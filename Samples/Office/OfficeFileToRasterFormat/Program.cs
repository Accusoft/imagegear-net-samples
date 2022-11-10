using System.IO;
using ImageGear.Formats;
using ImageGear.Formats.PDF;
using ImageGear.Formats.Office;

namespace OfficeFileToRasterFormat
{
    class Program
    {
        static void Main()
        {
            // Initialize common formats.
            ImGearCommonFormats.Initialize();

            // Add PDF format to filters list.
            ImGearFileFormats.Filters.Add(ImGearPDF.CreatePDFFormat());

            // Add Word format to filters list.
            ImGearFileFormats.Filters.Add(ImGearOffice.CreateWordFormat());

            // Initialize PDF support. Initialize for each process or thread.
            ImGearPDF.Initialize();

            // Initialize Office support for each process or thread. You must supply the path to your LibreOffice installation
            // via the IMAGEGEAR_LIBREOFFICE_PATH environment variable. See ImageGear documentation for more information.
            ImGearOffice.Initialize();

            // Load a Word document.
            ImGearWordDocument imGearWordDocument = null;
            using (Stream stream1 = new FileStream(@"../../../../../../../Sample Input/word-sample.docx", FileMode.Open, FileAccess.Read, FileShare.Read))
                imGearWordDocument = ImGearFileFormats.LoadDocument(stream1) as ImGearWordDocument;

            // Save the Word document to a multi-page tiff file
            using (Stream tiffOutputStream = new FileStream(@"../../../../../../../Sample Output/OfficeFileToRasterFormat.tif", FileMode.Create))
                ImGearFileFormats.SaveDocument(imGearWordDocument, tiffOutputStream, 0, ImGearSavingModes.OVERWRITE, ImGearSavingFormats.TIF_DEFLATE, new ImGearSaveOptions());

            // Save the first page of the Word document to a PNG file
            using (Stream pngOutputStream = new FileStream(@"../../../../../../../Sample Output/OfficeFileToRasterFormat.png", FileMode.Create))
                ImGearFileFormats.SavePage(imGearWordDocument.Pages[0], pngOutputStream, ImGearSavingFormats.PNG);
            
            // Dispose the document
            imGearWordDocument?.Dispose();

            // Terminate Office support once for each call to Initialize Office support.
            ImGearOffice.Terminate();

            // Terminate PDF support once for each call to Initialize PDF support.
            ImGearPDF.Terminate();
        }
    }
}
