using System.IO;
using ImageGear.Formats;
using ImageGear.Formats.PDF;
using ImageGear.Formats.PDF.Forms;

namespace FlattenPDFFormFields
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
            using (Stream stream = new FileStream(@"../../../../../../../Sample Input/FilledFormFields.pdf", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (ImGearPDFDocument imGearPDFDocument = (ImGearPDFDocument)ImGearFileFormats.LoadDocument(stream, 0, -1))
                {

                    // Flatten the PDF form fields
                    imGearPDFDocument.FlattenFormFields(false);

                    // Save the flattened PDF document.
                    using (Stream outputStream = new FileStream(@"../../../../../../../Sample Output/FlattenPDFFormFields.pdf", FileMode.Create, FileAccess.Write))
                        imGearPDFDocument.Save(outputStream, ImGearSavingFormats.PDF, 0, 0, -1, ImGearSavingModes.OVERWRITE);
                }
            }

            // Terminate PDF support once for each call to Initialize PDF support.
            ImGearPDF.Terminate();
        }
    }
}
