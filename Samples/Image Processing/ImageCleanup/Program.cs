using System.IO;
using ImageGear.Core;
using ImageGear.Formats;
using ImageGear.Processing.ImageClean;

namespace ImageCleanup
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

            // Remove dotted lines from the page.
            ImGearICDotLineOptions dotLineOptions = new ImGearICDotLineOptions()
            {
                MaxDiameter = 5,
                MinDiameter = 0,
                MinNumber = 0,
                Unit = ImGearICUnits.MM,
                Direction = ImGearICDirection.HORIZONTAL
            };
            ImGearIC.RemoveDotLines(imGearPage, dotLineOptions);

            // Remove black borders from the page.
            ImGearICBorderOptions borderOptions = new ImGearICBorderOptions()
            {
                nBottomBorderSize = 120,
                nLeftBorderSize = 120,
                nRightBorderSize = 120,
                nTopBorderSize = 120,
                nMinLinesNum = 10,
                nMinLineWidth = 10
            };
            ImGearIC.CleanBorders(imGearPage, borderOptions);

            // Orient the page content based on the text on the page.
            ImGearIC.TextAutoOrientation(imGearPage, ImGearICDocumentType.STANDARD);

            // Remove hole puncher marks from the page.
            ImGearICPunchHoleOptions punchHoleOptions = new ImGearICPunchHoleOptions()
            {
                MaxAspect = 1.05,
                MinAspect = 0.95,
                MaxDiameter = 10,
                MinDiameter = 5,
                Unit = ImGearICUnits.MM
            };
            ImGearIC.RemovePunchHoles(imGearPage, punchHoleOptions);

            // Remove horizontal lines from the page.
            ImGearICLineOptions lineOptions = new ImGearICLineOptions()
            {
                MinLength = 50,
                MaxWidth = 4,
                MaxBreak = 1,
                Unit = ImGearICUnits.MM,
                Direction = ImGearICDirection.HORIZONTAL
            };
            ImGearIC.RemoveLines(imGearPage, lineOptions);

            // Crop the page to the page content.
            ImGearIC.AutoCrop(imGearPage);

            // Save image page.
            using (FileStream outputStream = new FileStream(@"../../../../../../Sample Output/ImageCleanup.tif", FileMode.Create))
                ImGearFileFormats.SavePage(imGearPage, outputStream, 1, ImGearSavingModes.OVERWRITE, ImGearSavingFormats.TIF_LZW, new ImGearSaveOptions());
        }
    }
}
