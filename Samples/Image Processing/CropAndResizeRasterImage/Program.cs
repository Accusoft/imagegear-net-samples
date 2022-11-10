using System.IO;
using ImageGear.Core;
using ImageGear.Formats;
using ImageGear.Processing;

namespace CropAndResizeRasterImage
{
    class Program
    {
        static void Main()
        {
            // Initialize common formats.
            ImGearCommonFormats.Initialize();

            // Load image page.
            ImGearPage imGearPage;
            using (FileStream stream = new FileStream(@"../../../../../../Sample Input/water.jpg", FileMode.Open, FileAccess.Read, FileShare.Read))
                imGearPage = ImGearFileFormats.LoadPage(stream, 0);

            // Crop the image.
            int width = imGearPage.DIB.Width;
            int height = imGearPage.DIB.Height;
            ImGearProcessing.Crop(imGearPage, width / 4, height / 4, width * 3 / 4, height * 3 / 4);

            // Enlarge the cropped image using bi-linear interpolation.
            ImGearProcessing.Resize(imGearPage, width * 2, height * 2, ImGearInterpolationOptions.GetDefault(ImGearInterpolations.BILINEAR));

            // Save image page.
            using (FileStream outputStream = new FileStream(@"../../../../../../Sample Output/CropAndResizeRasterImage.png", FileMode.Create))
                ImGearFileFormats.SavePage(imGearPage, outputStream, 1, ImGearSavingModes.OVERWRITE, ImGearSavingFormats.PNG, new ImGearSaveOptions());
        }
    }
}
