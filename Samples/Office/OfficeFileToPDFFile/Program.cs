using System.IO;
using System.Runtime.InteropServices;
using ImageGear.Formats;
using ImageGear.Formats.PDF;
using ImageGear.Formats.Office;

namespace OfficeToPDF
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

            // Add Office formats to the filters list.
            ImGearFileFormats.Filters.Add(ImGearOffice.CreateWordFormat());
            ImGearFileFormats.Filters.Add(ImGearOffice.CreateExcelFormat());
            ImGearFileFormats.Filters.Add(ImGearOffice.CreatePowerPointFormat());

            // Initialize Office support for each process or thread. You must supply the path to your LibreOffice installation
            // via the IMAGEGEAR_LIBREOFFICE_PATH environment variable. See ImageGear documentation for more information.
            ImGearOffice.Initialize();

            // Load a Word document.
            using (Stream stream = new FileStream(@"../../../../../../../Sample Input/word-sample.docx", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (ImGearWordDocument imGearWordDocument = (ImGearWordDocument)ImGearFileFormats.LoadDocument(stream, 0, -1))
                {
                    // Initialize default save options
                    ImGearSaveOptions defaultSaveOptions = new ImGearSaveOptions();

                    // Convert and save as a PDF document.
                    using (Stream outputStream = new FileStream(@"../../../../../../../Sample Output/OfficeFileToPDFFile.pdf", FileMode.Create, FileAccess.Write))
                        ImGearFileFormats.SaveDocument(imGearWordDocument, outputStream, 0, ImGearSavingModes.OVERWRITE, ImGearSavingFormats.PDF, defaultSaveOptions);
                }
            }

            // Terminate Office support once for each call to Initialize Office support.
            ImGearOffice.Terminate();

            // Terminate PDF support once for each call to Initialize PDF support.
            ImGearPDF.Terminate();
        }
    }
}
