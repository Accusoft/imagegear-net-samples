using System.IO;
using ImageGear.Core;
using ImageGear.Formats;
using ImageGear.Processing;

namespace CreateThumbnailImage
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

            // Create a thumbnail with dimensions that are 1/5 the original image size and maximum color interpolation quality.
            int thumbnailWidth = imGearPage.DIB.Width / 5;
            int thumbnailHeight = imGearPage.DIB.Height / 5;
            ImGearInterpolationOptions interpolationOptions = new ImGearColorInterpolationOptions(100);
            if (ImGearProcessing.Verifier.CanApplyCreateThumbnail(imGearPage, thumbnailWidth, thumbnailHeight, interpolationOptions))
            {
                ImGearPage thumbnailImage = ImGearProcessing.CreateThumbnail(imGearPage, thumbnailWidth, thumbnailHeight, interpolationOptions);

                // Save the thumbnail as a PNG.
                using (FileStream outputStream = new FileStream(@"../../../../../../Sample Output/CreateThumbnailImage.png", FileMode.Create))
                    ImGearFileFormats.SavePage(thumbnailImage, outputStream, ImGearSavingFormats.PNG);
            }
        }
    }
}
