using System.IO;
using ImageGear.Core;
using ImageGear.Formats;

namespace ConvertRasterImageFormat
{
    class Program
    {
        static void Main()
        {
            // Initialize common formats.
            ImGearCommonFormats.Initialize();

            // Load PNG image.
            ImGearPage imGearPage;
            using (FileStream stream = new FileStream(@"../../../../../../Sample Input/single-page.png", FileMode.Open, FileAccess.Read, FileShare.Read))
                imGearPage = ImGearFileFormats.LoadPage(stream, 0);

            // Save image as JPG.
            using (FileStream outputStream = new FileStream(@"../../../../../../Sample Output/ConvertRasterImageFormat.jpg", FileMode.Create))
                ImGearFileFormats.SavePage(imGearPage, outputStream, 1, ImGearSavingModes.OVERWRITE, ImGearSavingFormats.JPG, new ImGearSaveOptions());
        }
    }
}
