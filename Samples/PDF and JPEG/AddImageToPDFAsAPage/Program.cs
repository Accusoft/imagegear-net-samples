using System.IO;
using ImageGear.Core;
using ImageGear.Formats;
using ImageGear.Formats.PDF;

namespace AddImageToPDFAsAPage
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

            // Load PDF document from a file.
            using (Stream stream = new FileStream(@"../../../../../../../Sample Input/multi-page.pdf", FileMode.Open, FileAccess.Read, FileShare.None))
            {
                using (ImGearPDFDocument pdfDocument = (ImGearPDFDocument)ImGearFileFormats.LoadDocument(stream))
                {
                    // Get rectangle of first page.
                    ImGearRectangle rectangle = ((ImGearPDFPage)pdfDocument.Pages[0]).CropRectangle;

                    // Create the rectangle of new PDF page with the same size as visible area of first page.
                    ImGearPDFFixedRect pageRectangle = new ImGearPDFFixedRect
                    {
                        Left = ImGearPDF.IntToFixed(0),
                        Top = ImGearPDF.IntToFixed(rectangle.Top),
                        Right = ImGearPDF.IntToFixed(rectangle.Right),
                        Bottom = ImGearPDF.IntToFixed(0)
                    };

                    // Calculate page index in the middle of the document for the new page
                    int newPageIndex = pdfDocument.Pages.Count / 2 - 1;

                    // Create new PDF page in the PDF document with landscape Letter size.
                    using (ImGearPDFPage pdfPage = pdfDocument.CreateNewPage(newPageIndex, pageRectangle))
                    {

                        // Load a raster image.
                        using (FileStream imageFileStream = new FileStream(@"../../../../../../../Sample Input/water.jpg", FileMode.Open, FileAccess.Read))
                        {
                            ImGearPage imGearPage = ImGearFileFormats.LoadPage(imageFileStream);

                            // Add image to the new page in the document.
                            pdfPage.AddImage(imGearPage);
                        }
                    }

                    // Save the PDF document.
                    using (Stream streamOut = new FileStream(@"../../../../../../../Sample Output/AddImageToPDFAsAPage.pdf", FileMode.Create, FileAccess.Write, FileShare.None))
                        ImGearFileFormats.SaveDocument(pdfDocument, streamOut, 0, ImGearSavingModes.OVERWRITE, ImGearSavingFormats.PDF, new ImGearSaveOptions());
                }
            }

            // Terminate PDF support once for each call to Initialize PDF support.
            ImGearPDF.Terminate();
        }
    }
}
