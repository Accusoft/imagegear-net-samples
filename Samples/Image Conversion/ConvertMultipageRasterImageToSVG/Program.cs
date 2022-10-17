using System.IO;
using ImageGear.Core;
using ImageGear.Formats;

namespace ConvertMultipageRasterImageToSVG
{
    class Program
    {
        static void Main()
        {
            // Initialize common formats.
            ImGearCommonFormats.Initialize();

            using (Stream stream = new FileStream(@"../../../../../../Sample Input/multi-page-depth-24.tif", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                // Read all pages from the TIFF document.
                using (ImGearDocument imGearDocument = ImGearFileFormats.LoadDocument(stream))
                {
                    // Save the SVG file.
                    using (Stream outputStream = new FileStream(@"../../../../../../Sample Output/ConvertMultipageRasterImageToSVG.svg", FileMode.Create))
                    {
                        ImGearFileFormats.SaveDocument(imGearDocument, outputStream, 0, ImGearSavingModes.OVERWRITE, ImGearSavingFormats.SVG, null);
                    }
                }
            }
        }
    }
}
