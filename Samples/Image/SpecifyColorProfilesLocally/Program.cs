using System.IO;
using ImageGear.Core;
using ImageGear.Formats;
using ImageGear.Processing;

namespace SpecifyColorProfilesLocally
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

            // Create local ColorProfile objects from ImageGear's redistributed color profiles.
            ImGearColorProfile rgbProfile = new ImGearColorProfile(File.OpenRead("ig_rgb_profile.icm"));
            ImGearColorProfile cmykProfile = new ImGearColorProfile(File.OpenRead("ig_cmyk_profile.icm"));

            // Convert color space RGB to CMYK.
            ImGearRasterProcessing.ConvertColorSpace(imGearPage, new ImGearColorSpace(ImGearColorSpaceIDs.CMYK),
                rgbProfile, cmykProfile, ImGearRenderingIntents.PERCEPTUAL);

            // Save image page.
            using (FileStream outputStream = new FileStream(@"../../../../../../Sample Output/SpecifyColorProfilesLocally.jpg", FileMode.Create))
                ImGearFileFormats.SavePage(imGearPage, outputStream, ImGearSavingFormats.JPG);
        }
    }
}
