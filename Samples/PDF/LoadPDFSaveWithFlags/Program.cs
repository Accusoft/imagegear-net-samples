using System.IO;
using ImageGear.Formats;
using ImageGear.Formats.PDF;

namespace LoadPDFSaveWithFlags
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
            using (Stream stream = new FileStream(@"../../../../../../../Sample Input/samplepdf.pdf", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (ImGearPDFDocument imGearPDFDocument = (ImGearPDFDocument)ImGearFileFormats.LoadDocument(stream, 0, -1))
                {

                    // Choose PDF save flags that can reduce the size of a PDF document.
                    ImGearPDFSaveFlags saveFlags = ImGearPDFSaveFlags.ADD_FLATE | ImGearPDFSaveFlags.REPLACE_LZW | ImGearPDFSaveFlags.REMOVE_ASCII_FILTERS | ImGearPDFSaveFlags.COMPRESSED | ImGearPDFSaveFlags.FULL | ImGearPDFSaveFlags.OPTIMIZE_XOBJECTS | ImGearPDFSaveFlags.OPTIMIZE_CONTENT_STREAMS | ImGearPDFSaveFlags.OPTIMIZE_FONTS | ImGearPDFSaveFlags.COLLECT_GARBAGE;

                    ImGearControlParameter parameter = ImGearFileFormats.Filters.Get(ImGearFormats.PDF).Parameters.GetByName("SaveFlags");

                    // Update the PDF filter control parameter 'SaveFlags' used when writing PDF documents.
                    parameter.Value = saveFlags;

                    // Save the PDF document.
                    using (Stream outputStream = new FileStream(@"../../../../../../../Sample Output/LoadPDFSaveWithFlags.pdf", FileMode.Create, FileAccess.Write))
                        imGearPDFDocument.Save(outputStream, ImGearSavingFormats.PDF, 0, 0, -1, ImGearSavingModes.OVERWRITE);
                }
            }

            // Terminate PDF support once for each call to Initialize PDF support.
            ImGearPDF.Terminate();
        }
    }
}
