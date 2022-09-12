using System.IO;
using ImageGear.Core;
using ImageGear.Evaluation;
using ImageGear.Formats;
using ImageGear.Formats.PDF;

namespace MultipageTIFFtoPDFFile
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

            // Load a multi-page TIFF document.
            ImGearDocument imGearDocument = null;
            using (FileStream stream = new FileStream(@"../../../../../../../Sample Input/multi-page-depth-24.tif", FileMode.Open, FileAccess.Read, FileShare.Read))
                imGearDocument = ImGearFileFormats.LoadDocument(stream, 0, -1);

            // Create a PDF document.
            ImGearPDFDocument pdfDocument = new ImGearPDFDocument();

            // Add raster pages to a PDF document.
            foreach (ImGearPage rasterPage in imGearDocument.Pages)
            {
                ImGearPDFFixedRect pageRectangle = new ImGearPDFFixedRect
                {
                    Left = ImGearPDF.IntToFixed(0),
                    Top = ImGearPDF.IntToFixed(rasterPage.DIB.Height - 1),
                    Right = ImGearPDF.IntToFixed(rasterPage.DIB.Width - 1),
                    Bottom = ImGearPDF.IntToFixed(0)
                };
                ImGearPDFPage pdfPage = pdfDocument.CreateNewPage((int)ImGearPDFPageNumber.BEFORE_FIRST_PAGE, pageRectangle);
                pdfPage.AddImage(rasterPage);
            }

            // Save the PDF document.
            using (FileStream outputStream = new FileStream(@"../../../../../../../Sample Output/MultipageTIFFtoPDFFile.pdf", FileMode.Create))
                pdfDocument.Save(outputStream, ImGearSavingFormats.PDF, 0, 0, -1, ImGearSavingModes.OVERWRITE);
                
            // Dispose the document
            pdfDocument?.Dispose();

            // Terminate PDF support once for each call to Initialize PDF support.
            ImGearPDF.Terminate();
        }
    }
}
