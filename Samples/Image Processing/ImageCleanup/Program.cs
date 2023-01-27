using System;
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

            // Check for noise
            if (ImGearIC.Verifier.CanApplyCheckForNoise(imGearPage))
            {
                int noiseObjectNum = ImGearIC.CheckForNoise(imGearPage, 10);
                Console.WriteLine("Number of objects with size greater than 10 is " + noiseObjectNum.ToString());
            }

            // Remove dotted lines from the page.
            ImGearICDotLineOptions dotLineOptions = new ImGearICDotLineOptions()
            {
                MaxDiameter = 5,
                MinDiameter = 0,
                MinNumber = 0,
                Unit = ImGearICUnits.MM,
                Direction = ImGearICDirection.HORIZONTAL
            };

            if(ImGearIC.Verifier.CanApplyRemoveDotLines(imGearPage))
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
            if (ImGearIC.Verifier.CanApplyCleanBorders(imGearPage))
                ImGearIC.CleanBorders(imGearPage, borderOptions);

            // Orient the page content based on the text on the page.
            if (ImGearIC.Verifier.CanApplyAutoOrient(imGearPage))
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
            if (ImGearIC.Verifier.CanApplyRemovePunchHoles(imGearPage))
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
            if (ImGearIC.Verifier.CanApplyRemoveLines(imGearPage))
                ImGearIC.RemoveLines(imGearPage, lineOptions);

            // Crop the page to the page content.
            if (ImGearIC.Verifier.CanApplyAutoCrop(imGearPage))
                ImGearIC.AutoCrop(imGearPage);

            // Save image page.
            using (FileStream outputStream = new FileStream(@"../../../../../../Sample Output/ImageCleanup.tif", FileMode.Create))
                ImGearFileFormats.SavePage(imGearPage, outputStream, 1, ImGearSavingModes.OVERWRITE, ImGearSavingFormats.TIF_LZW, new ImGearSaveOptions());

            // Invert the page if the percentage of black pixels is higher than 5.
            if (ImGearIC.Verifier.CanApplyInvertBlackImage(imGearPage))
                ImGearIC.InvertBlackImage(imGearPage, 5);

            // Invert white text on black background using a minimum height of 40, width of 100, thickness of 10,
            // minimum letter size of 10, maximum letter size of 100, and a maximum border of 5.
            if (ImGearIC.Verifier.CanApplyInvertWhiteText(imGearPage))
                ImGearIC.InvertWhiteText(imGearPage, 40, 100, 10, 10, 100, 5);

            // Save image page.
            using (FileStream outputStream = new FileStream(@"../../../../../../Sample Output/ImageCleanupInverted.tif", FileMode.Create))
                ImGearFileFormats.SavePage(imGearPage, outputStream, 1, ImGearSavingModes.OVERWRITE, ImGearSavingFormats.TIF_LZW, new ImGearSaveOptions());
        }
    }
}
