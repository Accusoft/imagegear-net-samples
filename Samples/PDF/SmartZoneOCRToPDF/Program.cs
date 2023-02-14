using System.IO;
using ImageGear.Core;
using ImageGear.Formats;
using ImageGear.Formats.PDF;
using Accusoft.SmartZoneOCRSdk;

namespace SmartZoneOCRToPDF
{
    class Program
    {

        public static void AddHiddenTextBehindPDFPage(ref TextBlockResult textBlockResult, ref ImGearPDFPage imGearPDFPage,
            double pdfHeight, double widthPixelsToPoints, double heightPixelsToPoints)
        {
            // Create the font to use behind the image.
            int firstCharacterIndex = 0xFFFF;
            int lastCharacterIndex = 0;
            for (int lineIndex = 0; lineIndex < textBlockResult.NumberTextLines; lineIndex++)
            {
                TextLineResult textLineResult = textBlockResult.TextLine(lineIndex);
                for (int characterIndex = 0; characterIndex < textLineResult.NumberCharacters; characterIndex++)
                {
                    CharacterResult characterResult = textLineResult.Character(characterIndex);
                    if (characterResult.Text[0] < firstCharacterIndex)
                        firstCharacterIndex = characterResult.Text[0];
                    if (characterResult.Text[0] > lastCharacterIndex)
                        lastCharacterIndex = characterResult.Text[0];
                }
            }
            ImGearPDFAtom imGearPDFAtom = null;
            if ((textBlockResult.TextLine(0).Character(0).FontAttribute & FontAttributes.Proportional) != 0)
            {
                if ((textBlockResult.TextLine(0).Character(0).FontAttribute & FontAttributes.Serif) != 0)
                    imGearPDFAtom = new ImGearPDFAtom("Times-Roman");
                else
                    imGearPDFAtom = new ImGearPDFAtom("Helvetica");
            }
            else
            {
                imGearPDFAtom = new ImGearPDFAtom("Courier");
            }
            ImGearPDEFontAttrs imGearPDEFontAttrs = new ImGearPDEFontAttrs()
            {
                Name = imGearPDFAtom,
                Type = new ImGearPDFAtom("Type1")
            };
            ImGearPDEFont imGearPDEFont = new ImGearPDEFont(imGearPDEFontAttrs, firstCharacterIndex, lastCharacterIndex, null, null, null, null, 0, 0, 0);

            // Create a graphics state for new text elements.
            ImGearPDEGraphicState gState = new ImGearPDEGraphicState()
            {
                MiterLimit = (int)ImGearPDFFixedValues.TEN,
                Flatness = (int)ImGearPDFFixedValues.ONE,
                LineWidth = (int)ImGearPDFFixedValues.ONE,
            };
            ImGearPDEColorSpace colorSpace = new ImGearPDEColorSpace(new ImGearPDFAtom("DeviceRGB"));
            gState.StrokeColorSpec.Space = colorSpace;
            gState.FillColorSpec.Space = colorSpace;
            gState.FillColorSpec.Value.Color[0] = 255 * 255;
            gState.FillColorSpec.Value.Color[1] = 255 * 255;
            gState.FillColorSpec.Value.Color[2] = 255 * 255;

            // Process each recognized line. Since they will be inserted in reverse order, reverse the order of processing
            // so the final order will be correct.
            using (ImGearPDEContent imGearPDEContent = imGearPDFPage.GetContent())
            {
                for (int lineIndex = textBlockResult.NumberTextLines - 1; lineIndex >= 0; --lineIndex)
                {
                    TextLineResult textLineResult = textBlockResult.TextLine(lineIndex);

                    // Create a text element to draw the text behind the image on the page.
                    ImGearPDEText textElement = new ImGearPDEText();

                    // Create a transform matrix for the characters in the line of text. This will be modified 
                    // to scale and translate each character.
                    ImGearPDFFixedMatrix textMatrix = new ImGearPDFFixedMatrix();
                    textMatrix.Empty();

                    // Process each character in the recognized line.
                    for (int characterIndex = 0; characterIndex < textLineResult.NumberCharacters; characterIndex++)
                    {
                        CharacterResult characterResult = textLineResult.Character(characterIndex);

                        // Adjust the transform matrix for each character. If a valid font size is available (< 0), then it
                        // will provide the best placement and size. Otherwise, base the font size off the height of a
                        // capital letter, although this will usually not fit as well.
                        int fontSize = textLineResult.Character(0).FontSize;
                        if (fontSize > 0)
                        {
                            textMatrix.A = ImGearPDF.DoubleToFixed((double)characterResult.FontSize);
                            textMatrix.D = ImGearPDF.DoubleToFixed((double)characterResult.FontSize);
                        }
                        else
                        {
                            textMatrix.A = ImGearPDF.DoubleToFixed((double)characterResult.CapitalLetterHeight * 1.5 * heightPixelsToPoints);
                            textMatrix.D = ImGearPDF.DoubleToFixed((double)characterResult.CapitalLetterHeight * 1.5 * heightPixelsToPoints);
                        }
                        textMatrix.H = ImGearPDF.DoubleToFixed(characterResult.Area.X * widthPixelsToPoints);
                        textMatrix.V = ImGearPDF.DoubleToFixed(pdfHeight - characterResult.Baseline * heightPixelsToPoints);

                        // Add the character to the text element.
                        textElement.Add(ImGearPDETextFlags.CHAR, characterIndex, characterResult.Text, imGearPDEFont, gState, null, textMatrix, null);
                    }

                    // Insert the Text Element into the content behind the image.
                    imGearPDEContent.AddElement((int)ImGearPDEInsertElement.BEFORE_FIRST, textElement);
                    textElement.Dispose();
                }

                // Update the page content to include the added text elements.
                imGearPDFPage.SetContent();
            }
        }

        static void Main()
        {
            // Initialize common formats.
            ImGearCommonFormats.Initialize();

            // Add PDF format to filters list.
            ImGearFileFormats.Filters.Add(ImGearPDF.CreatePDFFormat());

            // Initialize PDF support. Initialize for each process or thread.
            ImGearPDF.Initialize();

            // Load the first page from a TIFF document.
            ImGearRasterPage imGearRasterPage = null;
            using (FileStream stream = new FileStream(@"../../../../../../../Sample Input/SmartZoneOCR_English.tif", FileMode.Open, FileAccess.Read, FileShare.Read))
                imGearRasterPage = ImGearFileFormats.LoadPage(stream, 0) as ImGearRasterPage;

            // OCR the image using SmartZone.
            TextBlockResult textBlockResult = null;
            using (SmartZoneOCR smartZoneOCR = new SmartZoneOCR())
            {
                smartZoneOCR.Reader.CharacterSet = CharacterSet.AllCharacters;
                smartZoneOCR.Reader.CharacterSet.Language = Language.WesternEuropean;
                textBlockResult = smartZoneOCR.Reader.AnalyzeField(imGearRasterPage);
            }

            // Create a PDF document.
            ImGearPDFDocument pdfDocument = new ImGearPDFDocument();

            // Calculate the width and height of the converted PDF page.
            IImGearResolution iimGearResolution = imGearRasterPage.DIB.ImageResolution;
            iimGearResolution.ConvertUnits(ImGearResolutionUnits.INCHES);
            double widthPixelsToPoints = (double)iimGearResolution.XDenominator / (double)iimGearResolution.XNumerator * 72.0;
            double heightPixelsToPoints = (double)iimGearResolution.YDenominator / (double)iimGearResolution.YNumerator * 72.0;
            double pdfWidth = widthPixelsToPoints * (double)imGearRasterPage.DIB.Width;
            double pdfHeight = heightPixelsToPoints * (double)imGearRasterPage.DIB.Height;

            // Create a new page in the PDF document.
            ImGearPDFFixedRect imGearPDFFixedRect = new ImGearPDFFixedRect(0, ImGearPDF.DoubleToFixed(pdfHeight), ImGearPDF.DoubleToFixed(pdfWidth), 0);
            ImGearPDFPage imGearPDFPage = pdfDocument.CreateNewPage((int)ImGearPDFPageNumber.BEFORE_FIRST_PAGE, imGearPDFFixedRect);

            // Add the raster image to the PDF page.
            imGearPDFPage.AddImage(imGearRasterPage);

            // Add the recognized text as invisible text behind the existing contents of the PDF page.
            Program.AddHiddenTextBehindPDFPage(ref textBlockResult, ref imGearPDFPage, pdfHeight, widthPixelsToPoints, heightPixelsToPoints);

            // Save the PDF document.
            using (FileStream outputStream = new FileStream(@"../../../../../../../Sample Output/SmartZoneOCRToPDF.pdf", FileMode.Create))
                pdfDocument.Save(outputStream, ImGearSavingFormats.PDF, 0, 0, -1, ImGearSavingModes.OVERWRITE);

            // Dispose the document
            pdfDocument?.Dispose();

            // Terminate PDF support once for each call to Initialize PDF support.
            ImGearPDF.Terminate();
        }
    }
}
