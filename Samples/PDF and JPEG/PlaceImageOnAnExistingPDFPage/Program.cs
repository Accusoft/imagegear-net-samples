using System.IO;
using ImageGear.Core;
using ImageGear.Formats;
using ImageGear.Formats.PDF;

namespace PlaceImageOnAnExistingPDFPage
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
            using (Stream stream = new FileStream(@"../../../../../../../Sample Input/multi-page.pdf", FileMode.Open, FileAccess.Read, FileShare.None))
            {
                using (ImGearPDFDocument pdfDocument = (ImGearPDFDocument)ImGearFileFormats.LoadDocument(stream))
                {
                    // Load a raster image.
                    using (Stream imageFileStream = new FileStream(@"../../../../../../../Sample Input/water.jpg", FileMode.Open, FileAccess.Read))
                    {
                        ImGearPage loadedIgRasterPage = ImGearFileFormats.LoadPage(imageFileStream);

                        // Add image to each page in the document.
                        foreach (ImGearPDFPage pdfPage in pdfDocument.Pages)
                        {

                            // Add the image to the top left corner.
                            ImGearDoubleRectangle areaOnPage = new ImGearDoubleRectangle(1.0, 1.0, 3.0, 3.0);
                            pdfPage.AddImage(loadedIgRasterPage, areaOnPage);
                        }
                    }

                    // Save the PDF document.
                    using (Stream streamOut = new FileStream(@"../../../../../../../Sample Output/PlaceImageOnAnExistingPDFPage.pdf", FileMode.Create, FileAccess.Write, FileShare.None))
                        ImGearFileFormats.SaveDocument(pdfDocument, streamOut, 0, ImGearSavingModes.OVERWRITE, ImGearSavingFormats.PDF, new ImGearSaveOptions());
                }
            }

            // Terminate PDF support once for each call to Initialize PDF support.
            ImGearPDF.Terminate();
        }
    }
}
