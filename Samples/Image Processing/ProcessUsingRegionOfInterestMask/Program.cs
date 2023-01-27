using System.IO;
using ImageGear.Core;
using ImageGear.Formats;
using ImageGear.Processing;

namespace ProcessUsingRegionOfInterestMask
{
    internal class Program
    {
        static void Main()
        {
            // Initialize common formats.
            ImGearCommonFormats.Initialize();

            // Load image page.
            ImGearRasterPage imGearPage = null;
            using (FileStream inputStream = new FileStream(@"../../../../../../Sample Input/water.jpg", FileMode.Open, FileAccess.Read, FileShare.Read))
                imGearPage = (ImGearRasterPage)ImGearFileFormats.LoadPage(inputStream);

            // Create an elliptical Region of Interest (ROI) mask, fit to the image's width and height.
            IImGearShape maskShape = new ImGearEllipse()
            {
                LeftTop = new ImGearPoint(0, 0),
                RightBottom = new ImGearPoint(imGearPage.DIB.Width, imGearPage.DIB.Height)
            };
            imGearPage.ROI = new ImGearROIMask(0, 0, maskShape);

            // Apply Gaussian blur within the Region of Interest.
            if (ImGearRasterProcessing.Verifier.CanApplyGaussianBlur(imGearPage))
                ImGearRasterProcessing.GaussianBlur(imGearPage, 5.0);

            // Save image page.
            using (FileStream outputStream = new FileStream(@"../../../../../../Sample Output/ProcessUsingRegionOfInterestMask.png", FileMode.Create))
                ImGearFileFormats.SavePage(imGearPage, outputStream, ImGearSavingFormats.PNG);
        }
    }
}
