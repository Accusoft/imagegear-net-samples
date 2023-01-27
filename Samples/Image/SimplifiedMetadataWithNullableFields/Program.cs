using System;
using System.IO;
using ImageGear.Core;
using ImageGear.Formats;
using ImageGear.Formats.TIF;

namespace SimplifiedMetadataWithNullableFields
{
    class Program
    {
        static void Main()
        {
            // Initialize common formats.
            ImGearCommonFormats.Initialize();

            // Load image page.
            ImGearPage imGearPage = null;
            using (FileStream inputStream = new FileStream(@"../../../../../../Sample Input/Bitonal-Cleanup-Sample.tif", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                imGearPage = ImGearFileFormats.LoadPage(inputStream);

                // If a particular item is not present in the metadata tree, the corresponding property returns null.
                // To allow this behavior for integers and other value types, corresponding properties, such as ImGearTIFFMetadata.Orientation, use Nullable types.
                ImGearTIFFOrientation igTIFFOrientation = (imGearPage.Metadata.TIFF as ImGearTIFFMetadata).Orientation.GetValueOrDefault();
                ushort yCbCrPositioning = (imGearPage.Metadata.TIFF as ImGearTIFFMetadata).YCbCrPositioning.GetValueOrDefault(1);
                uint tileLength = (imGearPage.Metadata.TIFF as ImGearTIFFMetadata).TileLength.GetValueOrDefault();

                // Display the orientation to the screen.
                Console.WriteLine("Image orientation is " + igTIFFOrientation.ToString());
                Console.WriteLine("YCbCrPositioning is " + yCbCrPositioning.ToString());
                Console.WriteLine("Tile Length is " + tileLength.ToString());
            }
        }
    }
}
