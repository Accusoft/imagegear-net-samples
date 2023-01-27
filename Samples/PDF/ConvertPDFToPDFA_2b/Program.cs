using System.IO;
using ImageGear.Formats;
using ImageGear.Formats.PDF;

namespace ConvertPDFToPDFA_2b
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
            using (Stream stream = new FileStream(@"../../../../../../../Sample Input/multi-page.pdf", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (ImGearPDFDocument imGearPDFDocument = (ImGearPDFDocument)ImGearFileFormats.LoadDocument(stream, 0, -1))
                {

                    // Create PDF/A conversion options for PDF/A-2b with a 300 DPI fallback rasterization resolution.
                    using (ImGearPDFPreflightConversionOptions options = new ImGearPDFPreflightConversionOptions(ImGearPDFPreflightProfile.PDFA_2B_2011, 300))
                    {

                        // Attempt to create a new ImGearPDFDocument object that is PDF/A-2b compliant.
                        using (ImGearPDFPreflight imGearPDFPreflight = new ImGearPDFPreflight(imGearPDFDocument))
                        {
                            ImGearPDFPreflightConversionResult imGearPDFPreflightConversionResult = imGearPDFPreflight.Convert(options);
                            if (imGearPDFPreflightConversionResult.Succeeded)
                            {

                                // Save the PDF/A-2b compliant PDF document.
                                using (Stream outputStream = new FileStream(@"../../../../../../../Sample Output/ConvertPDFToPDFA_2b.pdf", FileMode.Create, FileAccess.Write))
                                    imGearPDFPreflightConversionResult.Document.Save(outputStream, ImGearSavingFormats.PDF, 0, 0, -1, ImGearSavingModes.OVERWRITE);
                            }
                            imGearPDFPreflightConversionResult.Document?.Dispose();
                        }
                    }
                }
            }

            // Terminate PDF support once for each call to Initialize PDF support.
            ImGearPDF.Terminate();
        }
    }
}
