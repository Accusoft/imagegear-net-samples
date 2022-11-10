using System.IO;
using ImageGear.Formats;
using ImageGear.Formats.PDF;

namespace MergeTwoPDFFiles
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

            // Load the first PDF document.
            ImGearPDFDocument imGearPDFDocument1 = null;
            using (FileStream stream1 = new FileStream(@"../../../../../../../Sample Input/multi-page.pdf", FileMode.Open, FileAccess.Read, FileShare.Read))
                imGearPDFDocument1 = ImGearFileFormats.LoadDocument(stream1) as ImGearPDFDocument;

            // Load the second PDF document.
            ImGearPDFDocument imGearPDFDocument2 = null;
            using (FileStream stream2 = new FileStream(@"../../../../../../../Sample Input/samplepdf.pdf", FileMode.Open, FileAccess.Read, FileShare.Read))
                imGearPDFDocument2 = ImGearFileFormats.LoadDocument(stream2) as ImGearPDFDocument;

            // Copy all pages from the second document to the first one.
            imGearPDFDocument1.InsertPages((int)ImGearPDFPageNumber.LAST_PAGE, imGearPDFDocument2, 0, (int)ImGearPDFPageRange.ALL_PAGES, ImGearPDFInsertFlags.ALL);

            // Save the PDF document.
            using (FileStream outputStream = new FileStream(@"../../../../../../../Sample Output/MergeTwoPDFFiles.pdf", FileMode.Create))
                imGearPDFDocument1.Save(outputStream, ImGearSavingFormats.PDF, 0, 0, -1, ImGearSavingModes.OVERWRITE);

            // Dispose of the PDF documents
            imGearPDFDocument1?.Dispose();
            imGearPDFDocument2?.Dispose();

            // Terminate PDF support once for each call to Initialize PDF support.
            ImGearPDF.Terminate();
        }
    }
}
