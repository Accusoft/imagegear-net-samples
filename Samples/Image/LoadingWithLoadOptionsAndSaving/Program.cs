using System.IO;
using ImageGear.Core;
using ImageGear.Formats;

namespace LoadingWithLoadOptionsAndSaving
{
    class Program
    {
        static void Main()
        {
            // Initialize common formats.
            ImGearCommonFormats.Initialize();

            // When image format is already known, specify image format explicitly to bypass auto-detection.
            ImGearLoadOptions imGearLoadOptions = new ImGearLoadOptions();
            imGearLoadOptions.Format = ImGearFormats.TIF;

            // Load image page using optional parameters.
            ImGearPage imGearPage = null;
            int pageIndex = 1;
            using (FileStream inputStream = new FileStream(@"../../../../../../Sample Input/multi-page-depth-24.tif", FileMode.Open, FileAccess.Read, FileShare.Read))
                imGearPage = ImGearFileFormats.LoadPage(inputStream, pageIndex, imGearLoadOptions);

            // Save image page.
            using (FileStream outputStream = new FileStream(@"../../../../../../Sample Output/LoadingWithLoadOptionsAndSaving.jpg", FileMode.Create))
                ImGearFileFormats.SavePage(imGearPage, outputStream, ImGearSavingFormats.JPG);
        }
    }
}
