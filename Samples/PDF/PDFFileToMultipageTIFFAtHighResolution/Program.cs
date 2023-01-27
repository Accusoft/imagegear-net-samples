using System.IO;
using ImageGear.Formats;
using ImageGear.Formats.PDF;

namespace PDFFileToMultipageTIFFAtHighResolution
{
    internal class Program
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
            using (Stream inputStream = new FileStream(@"../../../../../../../Sample Input/multi-page.pdf", FileMode.Open, FileAccess.Read, FileShare.Read))
                imGearPDFDocument = ImGearFileFormats.LoadDocument(inputStream) as ImGearPDFDocument;

            // Change global PDF filter control parameters for rasterization resolution.
            ImGearFileFormats.Filters.Get(ImGearFormats.PDF).Parameters.GetByName("ResolutionX").Value = 300;
            ImGearFileFormats.Filters.Get(ImGearFormats.PDF).Parameters.GetByName("ResolutionY").Value = 300;

            // Save the PDF document to a multi-page TIFF file.
            // Use null ImGearRasterSaveOptions to avoid overriding ResolutionX and ResolutionY with ScaleX and ScaleY defaults.
            using (Stream outputStream = new FileStream(@"../../../../../../../Sample Output/PDFFileToMultipageTIFFAtHighResolution-300DPI.tif", FileMode.Create))
                ImGearFileFormats.SaveDocument(imGearPDFDocument, outputStream, 0, ImGearSavingModes.OVERWRITE, ImGearSavingFormats.TIF_DEFLATE, null);

            // Set scaling factor for rasterized image. Scale = 1 (default) corresponds to 72 dpi, 2 to 144 dpi, etc.
            // The default BitDepth cannot be used.
            ImGearRasterSaveOptions rasterSaveOptions = new ImGearRasterSaveOptions()
            {
                BitDepth = 24,
                ScaleX = 4.0,
                ScaleY = 4.0
            };

            // Save the PDF document to a multi-page TIFF file.
            using (Stream outputStream = new FileStream(@"../../../../../../../Sample Output/PDFFileToMultipageTIFFAtHighResolution-ScaleFactor4.tif", FileMode.Create))
                ImGearFileFormats.SaveDocument(imGearPDFDocument, outputStream, 0, ImGearSavingModes.OVERWRITE, ImGearSavingFormats.TIF_DEFLATE, rasterSaveOptions);

            // Dispose the document.
            imGearPDFDocument.Dispose();

            // Terminate PDF support once for each call to Initialize PDF support.
            ImGearPDF.Terminate();
        }
    }
}
