using System.IO;
using ImageGear.Core;
using ImageGear.Formats;
using ImageGear.Processing;

namespace ProcessUsingRegionOfInterest
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

            // Create a rectangular Region of Interest (ROI) for the left half of the image.
            imGearPage.ROI = new ImGearROIRectangle(0, 0, imGearPage.DIB.Width / 2, imGearPage.DIB.Height);

            // Equalize constrast within the Region of Interest.
            if (ImGearRasterProcessing.Verifier.CanApplyEqualizeContrast(imGearPage))
                ImGearRasterProcessing.EqualizeContrast(
                    imGearPage, ImGearContrastModes.PIXEL, new ImGearChannelRange(0, imGearPage.DIB.ChannelCount));

            // Save image page.
            using (FileStream outputStream = new FileStream(@"../../../../../../Sample Output/ProcessUsingRegionOfInterest.png", FileMode.Create))
                ImGearFileFormats.SavePage(imGearPage, outputStream, ImGearSavingFormats.PNG);
        }
    }
}
