using System;
using System.IO;
using ImageGear.Core;
using ImageGear.Formats;

namespace DetectImageFormat
{
    class Program
    {
        static void Main()
        {
            // Initialize common formats.
            ImGearCommonFormats.Initialize();

            // Load data stream.
            using (FileStream inputStream = new FileStream(@"../../../../../../Sample Input/water.jpg", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                // Detect image format using a valid stream.
                IImGearFormat format = ImGearFileFormats.Detect(inputStream);

                // Output image format information.
                Console.WriteLine("Full Name of Image Format: " + format.FullName);
                Console.WriteLine("Name of Image Format: " + format.Name);
                Console.WriteLine("Default Extension for Image Format: " + format.DefaultExtension);
                Console.WriteLine("ID for Image Format: " + format.ID);
                Console.WriteLine("MIME Type for Image Format: " + format.MIMEType);
            }

        }
    }
}
