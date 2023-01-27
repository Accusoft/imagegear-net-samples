using System.IO;
using ImageGear.Core;
using ImageGear.Formats;
using ImageGear.Processing;

namespace EqualizeContrastForRasterImage
{
    class Program
    {
        static void Main()
        {
            // Initialize common formats.
            ImGearCommonFormats.Initialize();

            // Load image page.
            ImGearRasterPage imGearPage;
            using (FileStream stream = new FileStream(@"../../../../../../Sample Input/water.jpg", FileMode.Open, FileAccess.Read, FileShare.Read))
                imGearPage = ImGearFileFormats.LoadPage(stream, 0) as ImGearRasterPage;

            // Equalize the image.
            if (ImGearRasterProcessing.Verifier.CanApplyEqualizeContrast(imGearPage))
                ImGearRasterProcessing.EqualizeContrast(
                    imGearPage, ImGearContrastModes.PIXEL, new ImGearChannelRange(0, imGearPage.DIB.ChannelCount));

            // Save image page.
            using (FileStream outputStream = new FileStream(@"../../../../../../Sample Output/EqualizeContrastForRasterImage.png", FileMode.Create))
                ImGearFileFormats.SavePage(imGearPage, outputStream, 1, ImGearSavingModes.OVERWRITE, ImGearSavingFormats.PNG, new ImGearSaveOptions());
        }
    }
}
