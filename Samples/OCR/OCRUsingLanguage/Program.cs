using System;
using System.IO;
using ImageGear.Core;
using ImageGear.Evaluation;
using ImageGear.Formats;
using ImageGear.OCR;

namespace OCRUsingLanguage
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
            using (Stream stream = new FileStream(@"../../../../../../Sample Input/OCRLanguageSelectionDemo.png", FileMode.Open, FileAccess.Read, FileShare.Read))
                rasterPage = (ImGearRasterPage)ImGearFileFormats.LoadPage(stream);

            // Create OCR engine for recognition.
            using (ImGearOCR ocr = ImGearOCR.Create())
            {
                // Disable all languages.
                ocr.Settings.LanguageEnabled.DisableAll();

                // Enable only the Japanese language.
                ocr.Settings.LanguageEnabled[ImGearOCRLanguage.Japanese] = true;

                // Import raster page to the OCR engine.
                using (ImGearOCRPage ocrPage = ocr.ImportPage(rasterPage))
                {
                    // Prepare page for recognition.
                    ocrPage.Image.Preprocess();

                    // Run recognition process.
                    ocrPage.Recognize();

                    // Save recognition results to a text file.
                    File.WriteAllText(@"../../../../../../Sample Output/OCRUsingLanguage.txt", ocrPage.Text);
                }
            }
        }
    }
}
