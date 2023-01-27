using System.IO;
using ImageGear.Core;
using ImageGear.Formats;
using ImageGear.Processing;

namespace SpecifyColorProfilesGlobally
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

            // Enable color profiles and assign ImageGear's redistributed color profiles.
            ImGearColorProfileManager.EnableProfiles = true;
            ImGearColorProfileManager.RgbProfile = new ImGearColorProfile(File.OpenRead("ig_rgb_profile.icm"));
            ImGearColorProfileManager.CmykProfile = new ImGearColorProfile(File.OpenRead("ig_cmyk_profile.icm"));

            // Convert color space RGB to CMYK.
            ImGearRasterProcessing.ConvertColorSpace(imGearPage, new ImGearColorSpace(ImGearColorSpaceIDs.CMYK));

            // Save image page.
            using (FileStream outputStream = new FileStream(@"../../../../../../Sample Output/SpecifyColorProfilesGlobally.jpg", FileMode.Create))
                ImGearFileFormats.SavePage(imGearPage, outputStream, ImGearSavingFormats.JPG);
        }
    }
}
