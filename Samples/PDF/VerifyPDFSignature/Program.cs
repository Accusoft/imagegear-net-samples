using System;
using System.IO;
using ImageGear.Evaluation;
using ImageGear.Formats;
using ImageGear.Formats.PDF;
using ImageGear.Formats.PDF.Signatures;

namespace VerifyPDFSignature
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

            // Trust any user-specified certificates (DER or PEM format) to verify a signer's
            // identity when signing. If the certificate used to sign the PDF is not trusted,
            // an error will occur upon verification.
            ImGearPDF.TrustedCertificates.Import(@"../../../../../../Sample Input/SampleTrustedCertificate.der");

            // Load a signed PDF document.
            ImGearPDFDocument pdfDocument;
            using (Stream stream = new FileStream(@"../../../../../../Sample Input/accusoft-brochure-signed.pdf", FileMode.Open, FileAccess.Read))
                pdfDocument = (ImGearPDFDocument)ImGearFileFormats.LoadDocument(stream);

            // Verify all signatures in the document at once.
            SignatureVerificationResult result = pdfDocument.VerifySignatures();
            Console.WriteLine(result.ToString());

            // Dispose of the PDF document
            pdfDocument?.Dispose();

            // Terminate PDF support once for each call to Initialize PDF support.
            ImGearPDF.Terminate();
        }
    }
}
