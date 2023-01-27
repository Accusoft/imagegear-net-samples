using System.IO;
using ImageGear.Core;
using ImageGear.Formats;
using ImageGear.Processing;

namespace ErodeAndDilateRasterImage
{
    class Program
    {
        static void Main()
        {
            int[,] MorphologyMatrix = new int[,]
            {
                {0, 1, 1},
                {1, 1, 1},
                {0, 1, 0},
            };

            // Initialize common formats.
            ImGearCommonFormats.Initialize();

            // Load image page.
            ImGearRasterPage imGearPage;
            using (FileStream stream = new FileStream(@"../../../../../../Sample Input/Bitonal-Cleanup-Sample.tif", FileMode.Open, FileAccess.Read, FileShare.Read))
                imGearPage = ImGearFileFormats.LoadPage(stream, 0) as ImGearRasterPage;

            // Erode the image.
            if (ImGearRasterProcessing.Verifier.CanApplyErode(imGearPage))
                ImGearRasterProcessing.Erode(imGearPage, MorphologyMatrix);

            // Dilate the image.
            if (ImGearRasterProcessing.Verifier.CanApplyDilate(imGearPage))
                ImGearRasterProcessing.Dilate(imGearPage, MorphologyMatrix);

            // Save image page.
            using (FileStream outputStream = new FileStream(@"../../../../../../Sample Output/ErodeAndDilateRasterImage.png", FileMode.Create))
                ImGearFileFormats.SavePage(imGearPage, outputStream, 1, ImGearSavingModes.OVERWRITE, ImGearSavingFormats.PNG, new ImGearSaveOptions());
        }
    }
}
