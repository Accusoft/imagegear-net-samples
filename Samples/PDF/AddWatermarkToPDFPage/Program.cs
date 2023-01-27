using System;
using System.IO;
using ImageGear.Core;
using ImageGear.Formats;
using ImageGear.Formats.PDF;

namespace AddWatermarkToPDFPage
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

            const Int32 BITS_PER_BYTE = 8;

            // Load a PDF document.
            using (Stream pdfStream = new FileStream(@"../../../../../../../Sample Input/multi-page.pdf", FileMode.Open, FileAccess.Read, FileShare.None))
            using (Stream imgStream = new FileStream(@"../../../../../../../Sample Input/water.jpg", FileMode.Open, FileAccess.Read, FileShare.None))
            {
                using (ImGearPDFDocument pdfDocument = (ImGearPDFDocument)ImGearFileFormats.LoadDocument(pdfStream))
                using (ImGearDocument watermarkDocument = ImGearFileFormats.LoadDocument(imgStream))
                {
                    ImGearPDFPage pdfPage = pdfDocument.Pages[1] as ImGearPDFPage;
                    ImGearPage watermarkPage = watermarkDocument.Pages[0];

                    // Acquire the PDF page content.
                    using (ImGearPDEContent content = pdfPage.GetContent())
                    {
                        // Extract pixel data from RGB24 image.
                        Int32 strideInBytes = (watermarkPage.DIB.Width * watermarkPage.DIB.BitDepth + BITS_PER_BYTE - 1)
                                                / BITS_PER_BYTE;
                        Byte[] pixelData = new Byte[strideInBytes * watermarkPage.DIB.Height];
                        for (int row = 0; row < watermarkPage.DIB.Height; row++)
                        {
                            ImGearArrayRef raster = watermarkPage.DIB.GetRaster(row);
                            Array.Copy((Byte[])raster.Content, raster.Offset, pixelData, row * strideInBytes,
                                strideInBytes);
                        }

                        // Create a DeviceRGB PDE Colorspace for RGB24 image.
                        ImGearPDEColorSpace igPDEColorSpace = new ImGearPDEColorSpace(
                            new ImGearPDFAtom("DeviceRGB"));

                        // Create a PDE Image attributes for image Form XObject.
                        ImGearPDEImageAttrs igPDEImageAttrs = new ImGearPDEImageAttrs();
                        igPDEImageAttrs.Width = watermarkPage.DIB.Width;
                        igPDEImageAttrs.Height = watermarkPage.DIB.Height;
                        igPDEImageAttrs.Flags = ImGearPDEImageAttrFlags.EXTERNAL;
                        igPDEImageAttrs.BitsPerComponent = 8;

                        // Create a transform matrix that places the image at the bottom-left corner
                        // of the PDF page.
                        ImGearPDFFixedMatrix igPDETransformMatrix = new ImGearPDFFixedMatrix();
                        igPDETransformMatrix.A = ImGearPDF.DoubleToFixed(((double)watermarkPage.DIB.Width));
                        igPDETransformMatrix.B = (int)ImGearPDFFixedValues.ZERO;
                        igPDETransformMatrix.C = (int)ImGearPDFFixedValues.ZERO;
                        igPDETransformMatrix.D = ImGearPDF.DoubleToFixed(((double)watermarkPage.DIB.Height));
                        igPDETransformMatrix.H = (int)ImGearPDFFixedValues.ZERO;
                        igPDETransformMatrix.V = (int)ImGearPDFFixedValues.ZERO;

                        // Create a filter to compress RGB24 pixel data using the discrete cosine
                        // transform (DCT) technique based on the JPEG standard.
                        ImGearPDEFilterArray igPDEFilterArray = new ImGearPDEFilterArray();
                        ImGearPDFBasDict igDict = new ImGearPDFBasDict((ImGearPDFDocument)pdfPage.Document,
                                                     false, 3);
                        igDict.PutInt(new ImGearPDFAtom("Columns"), false, watermarkPage.DIB.Width);
                        igDict.PutInt(new ImGearPDFAtom("Rows"), false, watermarkPage.DIB.Height);
                        igDict.PutInt(new ImGearPDFAtom("Colors"), false, 3);
                        ImGearPDEFilterSpec igPDEFilterSpec = new ImGearPDEFilterSpec();
                        igPDEFilterSpec.Name = new ImGearPDFAtom("DCTDecode");
                        igPDEFilterSpec.DecodeParams = igDict;
                        igPDEFilterSpec.EncodeParams = igDict;
                        igPDEFilterArray.AddSpec(igPDEFilterSpec);

                        // Create the PDE image.
                        using (ImGearPDEImage igPDEImage = new ImGearPDEImage(igPDEImageAttrs,
                                igPDETransformMatrix, ImGearPDEImageDataFlags.DECODED, igPDEColorSpace, null,
                                igPDEFilterArray, null, pixelData))
                        {
                            // Add the PDEImage to the PDF page graphics.
                            content.AddElement((int)ImGearPDEInsertElement.AFTER_LAST, igPDEImage);
                            pdfPage.SetContent();

                            // Force refresh of PDF page content.
                            pdfPage.ResetDisplayCache();
                        }

                        // Save the PDF document.
                        using (Stream streamOut = new FileStream(@"../../../../../../../Sample Output/AddWatermarkToPDFPage.pdf", FileMode.Create, FileAccess.Write, FileShare.None))
                            ImGearFileFormats.SaveDocument(pdfDocument, streamOut, 0, ImGearSavingModes.OVERWRITE, ImGearSavingFormats.PDF, new ImGearSaveOptions());
                    }

                    // Release the PDF page content.
                    pdfPage.ReleaseContent();
                }

            }

            // Terminate PDF support once for each call to Initialize PDF support.
            ImGearPDF.Terminate();
        }
    }
}
