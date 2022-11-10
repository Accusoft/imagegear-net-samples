using System.IO;
using ImageGear.Formats;
using ImageGear.Formats.PDF;

namespace DeletePDFPage
{
    class Program
    {
        static void Main()
        {
            // Initialize common formats.
            ImGearCommonFormats.Initialize();

            // Add PDF format to the format list.
            ImGearFileFormats.Filters.Add(ImGearPDF.CreatePDFFormat());

            // Initialize PDF format for each using thread.
            ImGearPDF.Initialize();

            // Load a PDF document.
            using (Stream stream = new FileStream(@"../../../../../../../Sample Input/multi-page.pdf", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (ImGearPDFDocument igPDFDocument = (ImGearPDFDocument)ImGearFileFormats.LoadDocument(stream, 0, -1))
                {
                    // Remove the first page from the PDF document.
                    igPDFDocument.Pages.RemoveAt(0);

                    // Save the PDF document.
                    using (Stream outputStream = new FileStream(@"../../../../../../../Sample Output/DeletePDFPage.pdf", FileMode.Create, FileAccess.Write))
                        igPDFDocument.Save(outputStream, ImGearSavingFormats.PDF, 0, 0, -1, ImGearSavingModes.OVERWRITE);
                }
            }

            // Terminate PDF support once for each call to Initialize PDF support.
            ImGearPDF.Terminate();
        }
    }
}
