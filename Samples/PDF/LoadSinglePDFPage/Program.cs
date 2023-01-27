using System;
using System.IO;
using ImageGear.Core;
using ImageGear.Formats;
using ImageGear.Formats.PDF;

namespace LoadSinglePDFPage
{
    class Program
    {
        static void Main()
        {
            // Initialize common formats.
            ImGearCommonFormats.Initialize();

            // Add PDF format to filters list.
            ImGearFileFormats.Filters.Add(ImGearPDF.CreatePDFFormat());

            // Initialize PDF support. Initialize for each process or thread.
            ImGearPDF.Initialize();

            // Load the first page of the PDF document.
            using (Stream stream = new FileStream(@"../../../../../../../Sample Input/multi-page.pdf", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (ImGearPDFPage pdfPage = (ImGearPDFPage)ImGearFileFormats.LoadPage(stream, 0))
                {

                    // Report the PDF page's physical size in default user space units, typically 72 DPI.
                    ImGearRectangle mediaRect = pdfPage.MediaRect;
                    Console.WriteLine($"MediaRect is {{Left={mediaRect.Left}, Top={mediaRect.Top}, Right={mediaRect.Right}, Bottom={mediaRect.Bottom}}}");
                }
            }

            // Terminate PDF support once for each call to Initialize PDF support.
            ImGearPDF.Terminate();
        }
    }
}
