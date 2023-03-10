using System.Net.Http;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;

namespace PDFtoPDFARestAPI
{
    class Program
    {
        static string processManagerPublicUrl = "https://api.accusoft.com";
        static string workerFileUrl = "https://api.accusoft.com";
        static string processAPI = "/imageGear/api/v1/pdfToPdfaConverters";
        static string workerFileAPI = "/PCCIS/V1/WorkFile";

        // Create an HTTP Client
        // Note: http clients require different base Urls for local testing. Final implementation must use a single client. 
        private static HttpClient workFileClient = new HttpClient { BaseAddress = new Uri(workerFileUrl) };
        private static HttpClient conversionClient = new HttpClient() { BaseAddress = new Uri(processManagerPublicUrl) };

        static async Task Main(string[] args)
        {
            // Set ACCUSOFT_CLOUD_KEY as environment variable or replace in string below.
            var apiKey = System.Environment.GetEnvironmentVariable("ACCUSOFT_CLOUD_KEY") ?? "YourAPIKeyHere...";
            workFileClient.DefaultRequestHeaders.Add("acs-api-key", apiKey);
            conversionClient.DefaultRequestHeaders.Add("acs-api-key", apiKey);

            // Upload pdf image
            var content = new StreamContent(System.IO.File.OpenRead(@"../../../../../../../Sample Input/1-page-image-only.pdf"));
            content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("document/pdf");
            var workFileResponse = await workFileClient.PostAsync(workerFileAPI + "?FileExtension=pdf", content);

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
                workFileClient.DefaultRequestHeaders.Add("Accusoft-Affinity-Token", affinityToken.ToString());
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
            var processRequest = await conversionClient.PostAsync(processAPI, processRequestContent);
            string processRequestResponse = await processRequest.Content.ReadAsStringAsync();
            string processId = JsonNode.Parse(processRequestResponse)!["processId"]!.ToString();

            // Convert PDF to PDF/A
            string convertAPI = processAPI + "/" + processId;
            string processStatusResponse = await conversionClient.GetStringAsync(convertAPI);

            // Wait for the process to complete
            while (JsonNode.Parse(processStatusResponse)!["state"]!.ToString() == "processing")
            {
                Thread.Sleep(1000);
                processStatusResponse = await conversionClient.GetStringAsync(convertAPI);
            }

            // Get converted file ID and download it from the server
            var output = JsonNode.Parse(processStatusResponse)!["output"]!;
            var outputWorkFileId = output["fileId"]!.ToString();

            HttpResponseMessage downloadRequest = await workFileClient.GetAsync(workerFileAPI + "/" + outputWorkFileId);
            using (var fileStream = new FileStream(@"../../../../../../../Sample Output/PDFtoPDFARestAPI.pdf", FileMode.Create))
            {
                await downloadRequest.Content.CopyToAsync(fileStream);
            }
        }
    }
}
