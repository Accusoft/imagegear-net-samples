using System.IO;
using ImageGear.Core;
using ImageGear.Formats;

namespace SetFilterControlParametersGlobally
{
    internal class Program
    {
        static void Main()
        {
            // Initialize common formats.
            ImGearCommonFormats.Initialize();

            // Set JPEG Filter Control Parameter "SaveType" to 1 so that ImageGear saves JPEG using lossless compression instead of lossy compression.
            IImGearFormat jpgFormat = ImGearFileFormats.Filters.Get(ImGearFormats.JPG);
            ImGearControlParameter Param = jpgFormat.Parameters.GetByName("SaveType");
            Param.Value = 1;

            // Load image page.
            ImGearPage imGearPage = null;
            using (FileStream inputStream = new FileStream(@"../../../../../../Sample Input/water.jpg", FileMode.Open, FileAccess.Read, FileShare.Read))
                imGearPage = ImGearFileFormats.LoadPage(inputStream);

            // Save image page as lossless JPEG.
            using (FileStream outputStream = new FileStream(@"../../../../../../Sample Output/SetFilterControlParametersGlobally.jpg", FileMode.Create))
                ImGearFileFormats.SavePage(imGearPage, outputStream, ImGearSavingFormats.JPG);
        }
    }
}
