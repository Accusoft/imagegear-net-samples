using System.IO;
using ImageGear.Core;
using ImageGear.Formats;

namespace LoadRawImage
{
    class Program
    {
        static void Main()
        {
            // Initialize common formats.
            ImGearCommonFormats.Initialize();

            // Define the options for loading the raw image data.
            ImGearRawLoadOptions imGearRawLoadOptions = new ImGearRawLoadOptions()
            {
                Format = ImGearFormats.RAW,
                Compression = ImGearCompressions.CCITT_G4,
                BitsPerChannel = 1,
                Alignment = ImGearRawAlignment.Word,
                Offset = 0,
                Width = 2560,
                Height = 3500
            };

            // Load the raw image data.
            ImGearPage imGearPage = null;
            using (FileStream inputStream = new FileStream(@"../../../../../../Sample Input/G4ImageData.dat", FileMode.Open, FileAccess.Read, FileShare.Read))
                imGearPage = ImGearFileFormats.LoadPage(inputStream, 0, imGearRawLoadOptions);

            // Save the raw image data to a PNG image.
            using (FileStream outputStream = new FileStream(@"../../../../../../Sample Output/LoadRawImage.png", FileMode.Create))
                ImGearFileFormats.SavePage(imGearPage, outputStream, ImGearSavingFormats.PNG);
        }
    }
}
