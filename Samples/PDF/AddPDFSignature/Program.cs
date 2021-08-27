// Copyright Accusoft Corporation

using System.IO;
using System.Drawing;
using ImageGear.Evaluation;
using ImageGear.Formats;
using ImageGear.Formats.PDF;
using ImageGear.Formats.PDF.Forms;
using ImageGear.Formats.PDF.Signatures;

namespace AddPDFSignature
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

            // Load PDF document from file.
            ImGearPDFDocument pdfDocument;
            using (Stream stream = new FileStream(@"..\..\..\..\..\..\Sample Input\accusoft-brochure.pdf", FileMode.Open, FileAccess.Read, FileShare.None))
                pdfDocument = (ImGearPDFDocument)ImGearFileFormats.LoadDocument(stream);

            // PDF document should be disposed in any case.
            using (pdfDocument)
            {
                // As far as signature is field we need to be sure the container for field exists in the document.
                if (pdfDocument.Form == null)
                    pdfDocument.CreateForm();

                // We will add the signature to the last document page. Get this page.
                ImGearPDFPage pdfPage = (ImGearPDFPage)pdfDocument.Pages[pdfDocument.Pages.Count - 1];

                // Define the signature bounding box in the left bottom corner of page.
                ImGearPDFFixedRect boundingBox = new ImGearPDFFixedRect
                {
                    Left = ImGearPDF.IntToFixed(40),
                    Top = ImGearPDF.IntToFixed(80),
                    Right = ImGearPDF.IntToFixed(340),
                    Bottom = ImGearPDF.IntToFixed(40)
                };

                // Create the signature field.
                SignatureField signatureField = pdfDocument.Form.CreateSignatureField("signature", pdfPage, boundingBox);

                // Override some default visual aspects of the field.
                signatureField.Widgets[0].BorderColor = Color.Gray;
                signatureField.Widgets[0].FillColor = Color.AliceBlue;

                // Define the signing properties.
                signatureField.Signature = new ApprovalSignature(pdfDocument)
                {
                    SignerName = "John Doe",
                    SigningReason = "I have read and agree to this document",
                    Handler = new PKCS7SignatureHandler(@"..\..\..\..\..\..\Sample Input\SampleUserCertificate.pfx", "password")
                };

                // Save the signed PDF document to a file. Document is signed during saving.
                pdfDocument.Save(@"..\..\..\..\..\..\Sample Output\AddPDFSignature.pdf", ImGearSavingFormats.PDF, 0, 0, pdfDocument.Pages.Count, ImGearSavingModes.OVERWRITE);
            }

            // Terminate PDF support once for each call to Initialize PDF support.
            ImGearPDF.Terminate();
        }
    }
}
