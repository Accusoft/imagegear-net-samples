using System.IO;
using ImageGear.Evaluation;
using ImageGear.Formats;
using ImageGear.Formats.PDF;

namespace PDFFileToMultipageTIFF
{
    class Program
    {
        static void Main()
        {
            // Initialize common formats.
            ImGearCommonFormats.Initialize();

            // Add PDF format to filters list.
            ImGearFileFormats.Filters.Add(ImGearPDF.CreatePDFFormat());

            // Initialize PDF support. Initialize for each process or thread.
            ImGearPDF.Initialize();

            // Load a PDF document.
            ImGearPDFDocument imGearPDFDocument = null;
            using (Stream stream1 = new FileStream(@"../../../../../../../Sample Input/multi-page.pdf", FileMode.Open, FileAccess.Read, FileShare.Read))
                imGearPDFDocument = ImGearFileFormats.LoadDocument(stream1) as ImGearPDFDocument;

            // Save the PDF document to a multi-page tiff file
            using (Stream outputStream = new FileStream(@"../../../../../../../Sample Output/PDFFileToMultipageTIFF.tif", FileMode.Create))
                ImGearFileFormats.SaveDocument(imGearPDFDocument, outputStream, 0, ImGearSavingModes.OVERWRITE, ImGearSavingFormats.TIF_DEFLATE, new ImGearSaveOptions());

            // Dispose the document
            imGearPDFDocument?.Dispose();

            // Terminate PDF support once for each call to Initialize PDF support.
            ImGearPDF.Terminate();
        }
    }
}
