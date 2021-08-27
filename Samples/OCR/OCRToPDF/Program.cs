// Copyright Accusoft Corporation

using System.IO;
using ImageGear.Core;
using ImageGear.Evaluation;
using ImageGear.Formats;
using ImageGear.Formats.PDF;
using ImageGear.OCR;

namespace OCRToPDF
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

            // Load raster page from a file.
            ImGearRasterPage rasterPage;
            using (Stream stream = new FileStream(@"..\..\..\..\..\..\Sample Input\MRCExampleFlyer.png", FileMode.Open, FileAccess.Read, FileShare.Read))
                rasterPage = (ImGearRasterPage)ImGearFileFormats.LoadPage(stream);

            // Create OCR engine.
            using (ImGearOCR ocr = ImGearOCR.Create())
            {

                // Import raster page into OCR engine.
                using (ImGearOCRPage ocrPage = ocr.ImportPage(rasterPage))
                {

                    // Prepare page for recognition.
                    ocrPage.Image.Preprocess();

                    // Run recognition process.
                    ocrPage.Recognize();

                    // Create PDF document where recognized page should be placed.
                    using (ImGearPDFDocument pdfDocument = new ImGearPDFDocument())
                    {

                        // Create PDF output options with image over text.
                        ImGearOCRPDFOutputOptions options = new ImGearOCRPDFOutputOptions()
                        {
                            VisibleImage = true,
                            VisibleText = false
                        };

                        // Put recognition output to the PDF page in the document as image over text.
                        ocrPage.CreatePDFPage(pdfDocument, options);

                        // Save the PDF document.
                        using (Stream stream = new FileStream(@"..\..\..\..\..\..\Sample Output\OCRToPDF.pdf", FileMode.Create, FileAccess.Write, FileShare.None))
                            ImGearFileFormats.SaveDocument(pdfDocument, stream, 0, ImGearSavingModes.OVERWRITE, ImGearSavingFormats.PDF, new ImGearSaveOptions());
                    }
                }
            }

            // Terminate PDF support once for each call to Initialize PDF support.
            ImGearPDF.Terminate();
        }
    }
}
