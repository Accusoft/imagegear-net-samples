using System;
using System.IO;
using ImageGear.Formats;
using ImageGear.Formats.PDF;

namespace LoadEncryptedPDF
{
    class Program
    {
        // An application's PDFAuthProc() callback is expected to provide the password
        // using the ImGearPDFDocument.PermRequest() method.
        private static Boolean PDFAuthProc(ImGearPDFDocument pdfDocument, object ClientData)
        {
            ImGearPDFPermReqOpr targetOperation = (ImGearPDFPermReqOpr)ClientData;

            // Set or get from the user the password for the file.
            string password = "password";

            // Check the permissions associated with pdfDocument using the latest
            // permissions format and determine whether the targetOperation is
            // allowed for the specified object in the document.
            ImGearPDFPermReqStatus status = pdfDocument.PermRequest(ImGearPDFPermReqObj.DOC, targetOperation, password);

            // Return whether or not permission has been granted.    
            return (status == ImGearPDFPermReqStatus.GRANTED);
        }

        static void Main()
        {
            // Initialize common formats.
            ImGearCommonFormats.Initialize();

            // Add PDF format to filters list.
            ImGearFileFormats.Filters.Add(ImGearPDF.CreatePDFFormat());

            // Initialize PDF support. Initialize for each process or thread.
            ImGearPDF.Initialize();

            // Prior to opening any password-secured PDF documents, an authorization callback procedure
            // must be registered using the ImGearPDFDocument.RegisterAuthProc() method.
            ImGearPDFDocument.RegisterAuthProc(new ImGearPDFDocument.ImGearAuthProc(PDFAuthProc), ImGearPDFPermReqOpr.OPEN);

            // Load an encrypted PDF document.
            using (Stream stream = new FileStream(@"../../../../../../../Sample Input/samplepdf-encrypted.pdf", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (ImGearPDFDocument imGearPDFDocument = (ImGearPDFDocument)ImGearFileFormats.LoadDocument(stream, 0, -1))
                {
                    // Remove all security from encrypted PDF document.
                    imGearPDFDocument.SetNewCryptHandler(null);

                    // Save the PDF document.
                    using (Stream outputStream = new FileStream(@"../../../../../../../Sample Output/LoadEncryptedPDF.pdf", FileMode.Create, FileAccess.Write))
                        imGearPDFDocument.Save(outputStream, ImGearSavingFormats.PDF, 0, 0, -1, ImGearSavingModes.OVERWRITE);
                }
            }

            // Terminate PDF support once for each call to Initialize PDF support.
            ImGearPDF.Terminate();
        }
    }
}
