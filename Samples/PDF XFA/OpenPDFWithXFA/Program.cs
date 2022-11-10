using System.IO;
using ImageGear.Evaluation;
using ImageGear.Core;
using ImageGear.Formats;
using ImageGear.Formats.PDF;
using ImageGear.Formats.PDF.Forms;

namespace OpenPDFWithXFA
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

            // Allow XGA support
            IImGearFormat format = ImGearFileFormats.Filters.Get(ImGearFormats.PDF);
            ImGearControlParameter parameter = format.Parameters.GetByName("XFAAllowed");
            parameter.Value = true;

            // Load PDF document from a file.
            using (Stream stream = new FileStream(@"../../../../../../../Sample Input/StaticXFA.pdf", FileMode.Open, FileAccess.Read, FileShare.None))
            {
                using (ImGearPDFDocument pdfDocument = (ImGearPDFDocument)ImGearFileFormats.LoadDocument(stream))
                {
                    // If the document has XFA content then remove it and then re-save the document
                    if(pdfDocument.XFAContentType != ImGearPDFXFAContentTypes.NONE)
                    {
                        pdfDocument.RemoveXFA();
                        // Save the PDF document.
                        using (Stream streamOut = new FileStream(@"../../../../../../../Sample Output/StaticXFARemoved.pdf", FileMode.Create, FileAccess.Write, FileShare.None))
                            ImGearFileFormats.SaveDocument(pdfDocument, streamOut, 0, ImGearSavingModes.OVERWRITE, ImGearSavingFormats.PDF, new ImGearSaveOptions());
                    }
                }
            }

            // Terminate PDF support once for each call to Initialize PDF support.
            ImGearPDF.Terminate();
        }
    }
}
