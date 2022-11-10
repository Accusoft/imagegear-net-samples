using System.IO;
using ImageGear.Evaluation;
using ImageGear.Core;
using ImageGear.Formats;
using System;
using ImageGear.Formats.JPG;
using System.Xml.Linq;

namespace CompressUsingOptions
{
    class Program
    {
        static void Main()
        {
            // Initialize common formats.
            ImGearCommonFormats.Initialize();

            // Specify options for saving.
            ImGearSaveOptions saveJPGOptions = new ImGearSaveOptions();

            // Set conversion mode.
            saveJPGOptions.ConversionMode = ImGearRasterConversionModes.ANY;

            // Set target color space.
            saveJPGOptions.ForceColorspace = ImGearColorSpaceIDs.NONE;

            // Create local control parameters for the filter corresponding to the format to be compressed (i.e. the global filter is not modified).
            saveJPGOptions.Filters = new ImGearFileFilters();

            // Get control parameters for the target format.
            ImGearFormatParameters formatParameters = saveJPGOptions.Filters.Get(ImGearFormats.JPG).Parameters;

            // Set output type for JPG format.
            formatParameters.GetByName("SaveType").Value = (int)(ImGearJpegSaveType.Lossy);

            // Set quality for Lossy compression ([1, 100]).
            formatParameters.GetByName("Quality").Value = 30;

            // Load PNG image file.
            ImGearPage imGearPage = null;
            using (FileStream inputStream = new FileStream(@"../../../../../../Sample Input/single-page.png", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                imGearPage = ImGearFileFormats.LoadPage(inputStream);

                // Save to JPG file with options.
                using (FileStream outputStream = new FileStream(@"../../../../../../Sample Output/single-page-out.jpg", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    ImGearFileFormats.SavePage(imGearPage, outputStream, 0, ImGearSavingModes.OVERWRITE, ImGearSavingFormats.JPG, saveJPGOptions);
                }
            }
        }
    }
}
