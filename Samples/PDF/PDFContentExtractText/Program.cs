using System;
using System.IO;
using ImageGear.Evaluation;
using ImageGear.Formats;
using ImageGear.Formats.PDF;

namespace PDFContentExtractText
{
    class Program
    {
        static void Main()
        {

            // Initialize evaluation license.
            ImGearEvaluationManager.Initialize();

            // Initialize common formats.
            ImGearCommonFormats.Initialize();

            // Add PDF format to filters list.
            ImGearFileFormats.Filters.Add(ImGearPDF.CreatePDFFormat());

            // Initialize PDF support. Initialize for each process or thread.
            ImGearPDF.Initialize();

            // Load a PDF document.
            using (Stream stream = new FileStream(@"../../../../../../Sample Input/multi-page.pdf", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (ImGearPDFDocument imGearPDFDocument = ImGearFileFormats.LoadDocument(stream) as ImGearPDFDocument)
                {

                    // Create a memory stream to hold extracted text.
                    using (MemoryStream textFromPDF = new MemoryStream())
                    {

                        // Extract text from all pages.
                        imGearPDFDocument.ExtractText(0, imGearPDFDocument.Pages.Count, ImGearPDFContextFlags.PDF_ORDER, textFromPDF);
                        Console.WriteLine(System.Text.Encoding.GetEncoding(0).GetString(textFromPDF.ToArray()));
                    }
                }
            }

            // Terminate PDF support once for each call to Initialize PDF support.
            ImGearPDF.Terminate();
        }
    }
}
