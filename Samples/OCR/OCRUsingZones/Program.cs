using System;
using System.IO;
using ImageGear.Core;
using ImageGear.Evaluation;
using ImageGear.Formats;
using ImageGear.OCR;

namespace OCRUsingZones
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
            using (Stream stream = new FileStream(@"../../../../../../Sample Input/single-page.png", FileMode.Open, FileAccess.Read, FileShare.None))
                rasterPage = (ImGearRasterPage)ImGearFileFormats.LoadPage(stream);

            // Create OCR engine for recognition.
            using (ImGearOCR ocr = ImGearOCR.Create())
            {

                // Import raster page to the OCR engine.
                using (ImGearOCRPage ocrPage = ocr.ImportPage(rasterPage))
                {
                    // Create first zone of interest.
                    ImGearOCRZone zone = new ImGearOCRZone();
                    zone.Rect.CopyFrom(new ImGearRectangle(245, 1370, 1150, 200));

                    // Add first zone to the page's OCR zones.
                    ocrPage.Zones.Add(zone);

                    // Create second zone of interest.
                    zone = new ImGearOCRZone();
                    zone.Rect.CopyFrom(new ImGearRectangle(330, 1930, 1100, 385));

                    // Add second zone to the page's OCR zones.
                    ocrPage.Zones.Add(zone);

                    // Prepare image for recognition.
                    ocrPage.Image.Preprocess();

                    // Run recognition process.
                    ocrPage.Recognize();

                    // Write the recognition results.
                    Console.WriteLine(ocrPage.Text);
                }
            }
        }
    }
}
