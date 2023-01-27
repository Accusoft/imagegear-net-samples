using System.IO;
using ImageGear.Core;
using ImageGear.Formats;
using ImageGear.Formats.PDF;

namespace SplitPDFIntoMultiplePDFs
{
    class Program
    {
        static void Main()
        {
            const int FIRST_PAGE = 0;
            const int PAGE_COUNT = 1;

            // Initialize common formats.
            ImGearCommonFormats.Initialize();

            // Add PDF format to filters list.
            ImGearFileFormats.Filters.Add(ImGearPDF.CreatePDFFormat());

            // Initialize PDF support. Initialize for each process or thread.
            ImGearPDF.Initialize();

            // Load a PDF document.
            using (Stream stream = new FileStream(@"../../../../../../../Sample Input/multi-page.pdf", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (ImGearPDFDocument imGearPDFDocument = (ImGearPDFDocument)ImGearFileFormats.LoadDocument(stream, 0, -1))
                {
                    // For each page in document.
                    for (int i = 0; i < imGearPDFDocument.Pages.Count; i++)
                    {
                        // Construct the output filepath.
                        string outputFileName = string.Format("{0}_{1}.pdf", "SplitPDFIntoMultiplePDFs", i + 1);
                        string outputDirectory = @"../../../../../../../Sample Output";
                        string outputPath = System.IO.Path.Combine(outputDirectory, outputFileName);

                        // Create a new empty PDF document.
                        using (ImGearPDFDocument imGearTargetPDFDocument = new ImGearPDFDocument())
                        {
                            // Insert page into new PDF document.
                            imGearTargetPDFDocument.InsertPages((int)ImGearPDFPageNumber.BEFORE_FIRST_PAGE, imGearPDFDocument, i, PAGE_COUNT, ImGearPDFInsertFlags.DEFAULT);

                            // Save new PDF document to file.
                            imGearTargetPDFDocument.Save(outputPath, ImGearSavingFormats.PDF, FIRST_PAGE, FIRST_PAGE, PAGE_COUNT, ImGearSavingModes.OVERWRITE);
                        }
                    }
                }
            }

            // Terminate PDF support once for each call to Initialize PDF support.
            ImGearPDF.Terminate();
        }
    }
}
