// Copyright Accusoft Corporation

using System.IO;
using ImageGear.Evaluation;
using ImageGear.Core;
using ImageGear.Formats;

namespace LoadingAndSaving
{
    class Program
    {
        static void Main()
        {

            // Initialize evaluation license.
            ImGearEvaluationManager.Initialize();

            // Initialize common formats.
            ImGearCommonFormats.Initialize();

            // Load image page.
            ImGearPage imGearPage = null;
            using (FileStream inputStream = new FileStream(@"..\..\..\..\..\..\Sample Input\water.jpg", FileMode.Open, FileAccess.Read, FileShare.Read))
                imGearPage = ImGearFileFormats.LoadPage(inputStream);

            // Save image page.
            using (FileStream outputStream = new FileStream(@"..\..\..\..\..\..\Sample Output\LoadingAndSaving.png", FileMode.Create))
                ImGearFileFormats.SavePage(imGearPage, outputStream, ImGearSavingFormats.PNG);
        }
    }
}
