// Copyright Accusoft Corporation

using System.IO;
using ImageGear.Core;
using ImageGear.Evaluation;
using ImageGear.Formats;
using ImageGear.OCR;

namespace OCRToText
{
    class Program
    {
        static void Main()
        {

            // Initialize evaluation license.
            ImGearEvaluationManager.Initialize();

            // Initialize common formats.
            ImGearCommonFormats.Initialize();

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

                    // Save recognition results as a text file
                    File.WriteAllText(@"..\..\..\..\..\..\Sample Output\OCRToText.txt", ocrPage.Text);
                }
            }
        }
    }
}
