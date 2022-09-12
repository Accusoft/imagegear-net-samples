using System.IO;
using ImageGear.Core;
using ImageGear.Formats;
using ImageGear.Formats.Email;
using ImageGear.Formats.PDF;

namespace EmailToPDF
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

            // Add Email formats to filters list.
            ImGearFileFormats.Filters.Add(ImGearEmail.CreateMSGFormat());
            ImGearFileFormats.Filters.Add(ImGearEmail.CreateEMLFormat());

            // Load an Email document.
            using (Stream stream = new FileStream(@"../../../../../../../Sample Input/Email.msg", FileMode.Open, FileAccess.Read, FileShare.Read))
                using (ImGearDocument emailDocument = ImGearFileFormats.LoadDocument(stream))
                    // Save email to a PDF Document.
                    using (Stream outputStream = new FileStream(@"../../../../../../../Sample Output/EmailFileToPDFFile.pdf", FileMode.Create, FileAccess.Write))
                        ImGearFileFormats.SaveDocument(emailDocument, outputStream, 0, ImGearSavingModes.OVERWRITE, ImGearSavingFormats.PDF, null);

            // Terminate PDF support once for each call to Initialize PDF support.
            ImGearPDF.Terminate();
        }
    }
}
