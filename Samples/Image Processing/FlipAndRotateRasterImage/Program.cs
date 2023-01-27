using System.IO;
using ImageGear.Core;
using ImageGear.Formats;
using ImageGear.Processing;

namespace FlipAndRotateRasterImage
{
    internal class Program
    {
        static void Main()
        {
            // Initialize common formats.
            ImGearCommonFormats.Initialize();

            // Load image page.
            ImGearPage imGearPage = null;
            using (FileStream inputStream = new FileStream(@"../../../../../../Sample Input/water.jpg", FileMode.Open, FileAccess.Read, FileShare.Read))
                imGearPage = ImGearFileFormats.LoadPage(inputStream);

            // Flip the image horizontally.
            if (ImGearProcessing.Verifier.CanApplyFlip(imGearPage))
                ImGearProcessing.Flip(imGearPage, ImGearFlipModes.HORIZONTAL);

            // Create a cerulean blue background pixel for an RGB24 page.
            // The background pixel must have the same channel count and bit depth as the page.
            // A 24-bpp RGB pixel has 3 channels, 8 bits per channel.
            ImGearPixel backgroundPixel = new ImGearPixel(3, 8)
            {
                [0] = 0x2A,
                [1] = 0x52,
                [2] = 0xBE
            };

            // Rotate the image 15.0 degrees clockwise using bicubic interpolation, filling the expanded page dimensions with the background pixel.
            if (ImGearProcessing.Verifier.CanApplyRotate(imGearPage, ImGearInterpolations.BICUBIC))
                ImGearProcessing.Rotate(imGearPage, 15.0, ImGearRotationModes.CLIP, backgroundPixel, ImGearInterpolations.BICUBIC);

            // Save image page.
            using (FileStream outputStream = new FileStream(@"../../../../../../Sample Output/FlipAndRotateRasterImage.png", FileMode.Create))
                ImGearFileFormats.SavePage(imGearPage, outputStream, ImGearSavingFormats.PNG);
        }
    }
}
