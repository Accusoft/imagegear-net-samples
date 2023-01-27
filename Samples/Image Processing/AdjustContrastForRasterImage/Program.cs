using System.IO;
using ImageGear.Core;
using ImageGear.Formats;
using ImageGear.Processing;

namespace AdjustContrastForRasterImage
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

            // Adjust image contrast, brightness and gamma.
            if (ImGearRasterProcessing.Verifier.CanApplyAdjustContrast(imGearPage))
                ImGearRasterProcessing.AdjustContrast(imGearPage, 1.5, 64, 1.5);

            // Save image page.
            using (FileStream outputStream = new FileStream(@"../../../../../../Sample Output/AdjustContrastForRasterImage.png", FileMode.Create))
                ImGearFileFormats.SavePage(imGearPage, outputStream, 1, ImGearSavingModes.OVERWRITE, ImGearSavingFormats.PNG, new ImGearSaveOptions());
        }
    }
}
