using System.IO;
using ImageGear.Evaluation;
using ImageGear.Formats;
using ImageGear.Formats.PDF;

namespace CompressPDFFile
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

            // Create compression options with default values.
            ImGearCompressOptions compressOptions = new ImGearCompressOptions();

            // Open file for PDF file reading.
            using (FileStream stream = new FileStream(@"../../../../../../Sample Input/compression-info.pdf", FileMode.Open, FileAccess.Read, FileShare.Read))

            // Load PDF document from a file.
            using (ImGearPDFDocument pdfDocument = (ImGearPDFDocument)ImGearFileFormats.LoadDocument(stream))
            {
                // Save the PDF document with compression.
                pdfDocument.SaveCompressed(@"../../../../../../Sample Output/CompressPDFFile.pdf");
            }

            // Terminate PDF support once for each call to Initialize PDF support.
            ImGearPDF.Terminate();
        }
    }
}
