using System.IO;
using ImageGear.Core;
using ImageGear.Formats;

namespace SetFilterControlParametersLocally
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

            // Create a FileFilters object.
            ImGearFileFilters localFilters = new ImGearFileFilters();

            // Set local JPEG Filter Control Parameter "SaveType" to 1 so that ImageGear saves JPEG using lossless compression instead of lossy compression.
            localFilters.Get(ImGearFormats.JPG).Parameters.GetByName("SaveType").Value = 1;

            // Create a SaveOptions object to use the local FileFilters object.
            ImGearSaveOptions saveOptions = new ImGearSaveOptions();
            saveOptions.Filters = localFilters;

            // Save image page as lossless JPEG.
            using (FileStream outputStream = new FileStream(@"../../../../../../Sample Output/SetFilterControlParametersLocally.jpg", FileMode.Create))
                ImGearFileFormats.SavePage(imGearPage, outputStream, 1, ImGearSavingModes.OVERWRITE, ImGearSavingFormats.JPG, saveOptions);
        }
    }
}
