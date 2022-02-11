using System.IO;
using ImageGear.Evaluation;
using ImageGear.Formats;
using ImageGear.Formats.PDF;

namespace SetPDFSecurity
{
    class Program
    {
        static void Main()
        {

            // Initialize evaluation license.
            ImGearEvaluationManager.Initialize();

            // Initialize common formats.
            ImGearCommonFormats.Initialize();

            // Add PDF format to filters list.
            ImGearFileFormats.Filters.Add(ImGearPDF.CreatePDFFormat());

            // Initialize PDF support. Initialize for each process or thread.
            ImGearPDF.Initialize();

            // Read PDF document from file.
            using (Stream stream = new FileStream(@"../../../../../../Sample Input/accusoft-brochure.pdf", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (ImGearPDFDocument pdfDocument = (ImGearPDFDocument)ImGearFileFormats.LoadDocument(stream))
                {

                    // Create new security data for injection to PDF document.
                    ImGearPDFSecurityData securityData = new ImGearPDFSecurityData()
                    {
                        // Set permissions for all operations in PDF file.
                        Perms = ImGearPDFPermsFlags.ALL,

                        // Add support for Acrobat 9.0 and up.
                        Revision = ImGearPDFRevision.REVISION_5,

                        // Set encryption key length in bytes.
                        KeyLength = 32,

                        // Use 256-bit AES algorithm for encryption.
                        EncryptMethod = ImGearPDFStdSecurityMethod.AES_V2,

                        // Set these values to encrypt metadata and attachments, if desired.
                        EncryptMetadata = true,

                        // Set the password.
                        NewUserPW = true,
                        UserPW = "password"
                    };

                    // Set up the encryption handler and the new security data.
                    ImGearPDFAtom cryptHandler = new ImGearPDFAtom("Standard");
                    pdfDocument.SetNewCryptHandler(cryptHandler);
                    pdfDocument.SetNewSecurityData(securityData);

                    // Save the PDF document with new permissions.
                    using (Stream outputStream = new FileStream(@"../../../../../../Sample Output/SetPDFSecurity.pdf", FileMode.Create))
                        pdfDocument.Save(outputStream, ImGearSavingFormats.PDF, 0, 0, (int)ImGearPDFPageRange.ALL_PAGES, ImGearSavingModes.OVERWRITE);
                }
            }

            // Terminate PDF support once for each call to Initialize PDF support.
            ImGearPDF.Terminate();
        }
    }
}
