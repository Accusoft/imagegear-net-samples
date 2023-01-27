using System;
using System.IO;
using ImageGear.Core;
using ImageGear.Formats;
using ImageGear.Formats.EXIF;

namespace MetadataSimplifiedAPI
{
    class Program
    {
        static void Main()
        {
            // Initialize common formats.
            ImGearCommonFormats.Initialize();

            // Load image page.
            ImGearMetadataHead imGearMetadataHead = null;
            using (FileStream inputStream = new FileStream(@"../../../../../../Sample Input/Road.jpg", FileMode.Open, FileAccess.Read, FileShare.Read))
                imGearMetadataHead = ImGearFileFormats.LoadPageMetadata(inputStream, 0);

            // Get simplified access to EXIF and EXIF GPS metadata.
            ImGearEXIFMetadata imGearEXIFMetadata = imGearMetadataHead.EXIF as ImGearEXIFMetadata;
            if (imGearEXIFMetadata != null)
            {
                ImGearEXIFGPSMetadata imGearEXIFGPSMetadata = imGearEXIFMetadata.GPS;

                // Read the EXIF Model and EXIF GPS ImgDirection metadata tags.
                Console.WriteLine("imGearEXIFMetadata.Model: " + imGearEXIFMetadata.Model);
                if (imGearEXIFGPSMetadata != null)
                    Console.WriteLine("imGearEXIFGPSMetadata.ImgDirection: " + imGearEXIFGPSMetadata.ImgDirection);

                // Create a new EXIF Software metadata tag.
                imGearEXIFMetadata.Software = "Updated by MetadataSimplifiedAPI sample";

                // Update the EXIF Make metadata tag.
                imGearEXIFMetadata.Make = imGearEXIFMetadata.Make + " (MetadataSimplifiedAPI)";

                // Delete the EXIF DateTimeDigitized metadata tag.
                imGearEXIFMetadata.DateTimeDigitized = null;

                // Save image with the updated EXIF metadata tags.
                using (FileStream inputStream = new FileStream(@"../../../../../../Sample Input/Road.jpg", FileMode.Open, FileAccess.Read, FileShare.Read))
                    using (FileStream outputStream = new FileStream(@"../../../../../../Sample Output/MetadataSimplifiedAPI.jpg", FileMode.Create))
                        ImGearFileFormats.UpdatePageMetadata(inputStream, outputStream, imGearMetadataHead, 0);
            }
        }
    }
}
