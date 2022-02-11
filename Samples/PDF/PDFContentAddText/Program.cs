using System.IO;
using ImageGear.Evaluation;
using ImageGear.Formats;
using ImageGear.Formats.PDF;

namespace PDFContentAddText
{
    class Program
    {
        static void Main()
        {

            // Initialize evaluation license.
            ImGearEvaluationManager.Initialize();

            // Initialize common formats.
            ImGearCommonFormats.Initialize();

            // Add PDF format to filters list.
            ImGearFileFormats.Filters.Add(ImGearPDF.CreatePDFFormat());

            // Initialize PDF support. Initialize for each process or thread.
            ImGearPDF.Initialize();

            // Create a ImGearPDEFontAttrs to get the attributes for ImGearPDEFont and ImGearPDFSysFont.
            ImGearPDEFontAttrs findAttrs = new ImGearPDEFontAttrs()
            {
                Name = new ImGearPDFAtom("Times-Roman"),
                Type = new ImGearPDFAtom("Type1")
            };

            // Create the font.
            ImGearPDEFont font = new ImGearPDEFont(findAttrs, 0, 255, null, null, null, null, 0, 0, 0);

            // Set the color space to DeviceRGB.
            ImGearPDEColorSpace colorSpace = new ImGearPDEColorSpace(new ImGearPDFAtom("DeviceRGB"));

            // Set the attributes for the graphic state.
            ImGearPDEGraphicState gState = new ImGearPDEGraphicState()
            {
                MiterLimit = (int)ImGearPDFFixedValues.TEN,
                Flatness = (int)ImGearPDFFixedValues.ONE,
                LineWidth = (int)ImGearPDFFixedValues.ONE
            };

            // Set the red, green and blue color values in that order.
            gState.FillColorSpec.Value.Color[0] = 0;
            gState.FillColorSpec.Value.Color[1] = ImGearPDF.IntToFixed(128);
            gState.FillColorSpec.Value.Color[2] = ImGearPDF.IntToFixed(256);
            gState.FillColorSpec.Space = new ImGearPDEColorSpace(new ImGearPDFAtom("DeviceRGB"));

            // Create a transformation matrix of fixed numbers.
            ImGearPDFFixedMatrix textMatrix = new ImGearPDFFixedMatrix()
            {
                A = ImGearPDF.IntToFixed(40),
                D = ImGearPDF.IntToFixed(40),
                H = ImGearPDF.IntToFixed(50),
                V = ImGearPDF.IntToFixed(750)
            };

            // Create a string of text.
            string textToAdd = "This is the text that was added";

            // Create a new text element.
            ImGearPDEText textElement = new ImGearPDEText();

            // Add the textToAdd into the element using the settings from above.
            textElement.Add(ImGearPDETextFlags.RUN, 0, textToAdd, font, gState, null, textMatrix, null);

            // Load a PDF document.
            using (Stream stream = new FileStream(@"../../../../../../Sample Input/samplepdf.pdf", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (ImGearPDFDocument imGearPDFDocument = ImGearFileFormats.LoadDocument(stream) as ImGearPDFDocument)
                {

                    // Select the first page of the current document.
                    ImGearPDFPage page = (ImGearPDFPage)imGearPDFDocument.Pages[0];

                    // Get the content of the first page of the PDF document.
                    using (ImGearPDEContent content = page.GetContent())
                    {

                        // Insert a Text Element into the content.
                        content.AddElement((int)ImGearPDEInsertElement.AFTER_LAST, textElement);

                        // Set the page's PDE content back into the page object.
                        page.SetContent();
                    }

                    // Save the PDF document.
                    using (Stream outputStream = new FileStream(@"../../../../../../Sample Output/PDFContentAddText.pdf", FileMode.Create))
                        ImGearFileFormats.SaveDocument(imGearPDFDocument, outputStream, 1, ImGearSavingModes.OVERWRITE, ImGearSavingFormats.PDF, new ImGearSaveOptions());
                }
            }

            // Terminate PDF support once for each call to Initialize PDF support.
            ImGearPDF.Terminate();
        }
    }
}
