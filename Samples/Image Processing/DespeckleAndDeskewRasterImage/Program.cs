using System.IO;
using ImageGear.Core;
using ImageGear.Formats;
using ImageGear.Processing;

namespace DespeckleAndDeskewRasterImage
{
    class Program
    {
        static void Main()
        {
            // Initialize common formats.
            ImGearCommonFormats.Initialize();

            // Load image page.
            ImGearPage imGearPage;
            using (FileStream stream = new FileStream(@"../../../../../../Sample Input/Bitonal-Cleanup-Sample.tif", FileMode.Open, FileAccess.Read, FileShare.Read))
                imGearPage = ImGearFileFormats.LoadPage(stream, 0);

            // Despeckle the image.
            ImGearRasterProcessing.Despeckle((ImGearRasterPage)imGearPage, 5, 5);

            // Deskew the image.
            ImGearRasterProcessing.Deskew((ImGearRasterPage)imGearPage, 0.1, ImGearRotationModes.CLIP, null);

            // Save image page.
            using (FileStream outputStream = new FileStream(@"../../../../../../Sample Output/DespeckleAndDeskewRasterImage.tif", FileMode.Create))
                ImGearFileFormats.SavePage(imGearPage, outputStream, 1, ImGearSavingModes.OVERWRITE, ImGearSavingFormats.TIF_G4, new ImGearSaveOptions());
        }
    }
}
