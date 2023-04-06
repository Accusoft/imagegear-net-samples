using System.Net.Http;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;

namespace PDFXFARestAPI
{
    class Program
    {
        static string BaseUrl = "https://api.accusoft.com";
        static string processAPI = "/imageGear/api/v1/pdfXfaConverters";
        static string workerFileAPI = "/PCCIS/V1/WorkFile";

        // Create an HTTP Client
        // Note: http clients require different base Urls for local testing. Final implementation must use a single client. 
        private static HttpClient httpClient = new HttpClient { BaseAddress = new Uri(BaseUrl) };

        static async Task Main(string[] args)
        {
            // Set ACCUSOFT_CLOUD_KEY as environment variable or replace in string below.
            var apiKey = System.Environment.GetEnvironmentVariable("ACCUSOFT_CLOUD_KEY") ?? "YourAPIKeyHere...";
            httpClient.DefaultRequestHeaders.Add("acs-api-key", apiKey);

            // Upload pdf image
            var content = new StreamContent(System.IO.File.OpenRead(@"../../../../../../../Sample Input/StaticXFA.pdf"));
            content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("document/pdf");
            var workFileResponse = await httpClient.PostAsync(workerFileAPI + "?FileExtension=pdf", content);

            // Extract fileId from the upload response.
            string responseBody = await workFileResponse.Content.ReadAsStringAsync();
            var responseData = JsonNode.Parse(responseBody)!;
            string fileId = responseData["fileId"]!.ToString();

            // Exrtact Accusoft Affinity Token from response if it was included.
            // See https://help.accusoft.com/PrizmDoc/latest/HTML/affinity-tokens-and-cluster-mode.html?highlight=affinity%2C
            // for more information on Affinity Tokens.
            JsonNode? affinityToken = responseData["affinityToken"];
            if (affinityToken != null)
            {
                httpClient.DefaultRequestHeaders.Add("Accusoft-Affinity-Token", affinityToken.ToString());
            }

            // Build JSON for our process request
            var requestJson = new
            {
                input = new
                {
                    source = new
                    {
                        fileId
                    }
                }
            };

            // Create conversion process
            var processRequestContent = new StringContent(JsonSerializer.Serialize(requestJson), Encoding.UTF8, "application/json");
            var processRequest = await httpClient.PostAsync(processAPI, processRequestContent);
            string processRequestResponse = await processRequest.Content.ReadAsStringAsync();
            string processId = JsonNode.Parse(processRequestResponse)!["processId"]!.ToString();

            // Remove XFA
            string convertAPI = processAPI + "/" + processId;
            string processStatusResponse = await httpClient.GetStringAsync(convertAPI);

            // Wait for the process to complete
            while (JsonNode.Parse(processStatusResponse)!["state"]!.ToString() == "processing")
            {
                Thread.Sleep(1000);
                processStatusResponse = await httpClient.GetStringAsync(convertAPI);
            }

            // Get converted file ID and download it from the server
            var output = JsonNode.Parse(processStatusResponse)!["output"]!;
            var outputWorkFileId = output["fileId"]!.ToString();

            HttpResponseMessage downloadRequest = await httpClient.GetAsync(workerFileAPI + "/" + outputWorkFileId);
            using (var fileStream = new FileStream(@"../../../../../../../Sample Output/PDFXFARestAPI.pdf", FileMode.Create))
            {
                await downloadRequest.Content.CopyToAsync(fileStream);
            }
        }
    }
}
