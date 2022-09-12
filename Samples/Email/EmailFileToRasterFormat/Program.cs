using System.IO;
using ImageGear.Core;
using ImageGear.Formats;
using ImageGear.Formats.Email;

namespace EmailToImage
{
    class Program
    {
        static void Main()
        {
            // Initialize common formats.
            ImGearCommonFormats.Initialize();

            // Add Email formats to filters list.
            ImGearFileFormats.Filters.Insert(0, ImGearEmail.CreateMSGFormat());
            ImGearFileFormats.Filters.Insert(0, ImGearEmail.CreateEMLFormat());

            // Load a Email document as any other document.
            using (Stream stream = new FileStream(@"../../../../../../../Sample Input/Email.msg", FileMode.Open, FileAccess.Read, FileShare.Read))
                using (ImGearDocument emailDocument = ImGearFileFormats.LoadDocument(stream))
                {
                    // Save the Email document to a single-page TIFF image
                    using (Stream outputStream = new FileStream(@"../../../../../../../Sample Output/EmailFileToRasterFormat.tif", FileMode.Create, FileAccess.ReadWrite))
                        ImGearFileFormats.SaveDocument(emailDocument, outputStream, 0, ImGearSavingModes.OVERWRITE, ImGearSavingFormats.TIF_DEFLATE, null);

                    // Save the Email document to a PNG image
                    using (Stream outputStream = new FileStream(@"../../../../../../../Sample Output/EmailFileToRasterFormat.png", FileMode.Create, FileAccess.Write))
                        ImGearFileFormats.SaveDocument(emailDocument, outputStream, 0, ImGearSavingModes.OVERWRITE, ImGearSavingFormats.PNG, null);
                }
        }
    }
}
