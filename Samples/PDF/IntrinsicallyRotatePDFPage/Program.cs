using System;
using System.IO;
using System.Collections.Generic;
using ImageGear.Core;
using ImageGear.Evaluation;
using ImageGear.Formats;
using ImageGear.Formats.PDF;

namespace IntrinsicallyRotatePDFPage
{
    class Program
    {

        private static ImGearPDFFixedMatrix Concat(ImGearPDFFixedMatrix matrix1, ImGearPDFFixedMatrix matrix2)
        {
            // Multiply two Affine transformation matrices together to produce one matrix
            // that will perform the same transformation as the two matrices performed in series
            double matrix1A = ImGearPDF.FixedToDouble(matrix1.A);
            double matrix1B = ImGearPDF.FixedToDouble(matrix1.B);
            double matrix1C = ImGearPDF.FixedToDouble(matrix1.C);
            double matrix1D = ImGearPDF.FixedToDouble(matrix1.D);
            double matrix1H = ImGearPDF.FixedToDouble(matrix1.H);
            double matrix1V = ImGearPDF.FixedToDouble(matrix1.V);
            double matrix2A = ImGearPDF.FixedToDouble(matrix2.A);
            double matrix2B = ImGearPDF.FixedToDouble(matrix2.B);
            double matrix2C = ImGearPDF.FixedToDouble(matrix2.C);
            double matrix2D = ImGearPDF.FixedToDouble(matrix2.D);
            double matrix2H = ImGearPDF.FixedToDouble(matrix2.H);
            double matrix2V = ImGearPDF.FixedToDouble(matrix2.V);
            ImGearPDFFixedMatrix result = new ImGearPDFFixedMatrix
            {
                A = ImGearPDF.DoubleToFixed(matrix1A * matrix2A + matrix1B * matrix2C),
                B = ImGearPDF.DoubleToFixed(matrix1A * matrix2B + matrix1B * matrix2D),
                C = ImGearPDF.DoubleToFixed(matrix1C * matrix2A + matrix1D * matrix2C),
                D = ImGearPDF.DoubleToFixed(matrix1C * matrix2B + matrix1D * matrix2D),
                H = ImGearPDF.DoubleToFixed(matrix1H * matrix2A + matrix1V * matrix2C + matrix2H),
                V = ImGearPDF.DoubleToFixed(matrix1H * matrix2B + matrix1V * matrix2D + matrix2V)
            };
            return result;
        }

        private static ImGearPDFFixedMatrix ConcatTranslation(ImGearPDFFixedMatrix matrix, double translateX, double translateY)
        {
            // Concatenate translation to the input matrix.
            ImGearPDFFixedMatrix translateMatrix = new ImGearPDFFixedMatrix
            {
                A = ImGearPDF.DoubleToFixed(1.0),
                D = ImGearPDF.DoubleToFixed(1.0),
                H = ImGearPDF.DoubleToFixed(translateX),
                V = ImGearPDF.DoubleToFixed(translateY)
            };
            return Concat(matrix, translateMatrix);
        }

        private static ImGearPDFFixedMatrix ConcatScaling(ImGearPDFFixedMatrix matrix, double scaleRatio)
        {
            // Concatenate scaling to the input matrix.
            ImGearPDFFixedMatrix scaleMatrix = new ImGearPDFFixedMatrix
            {
                A = ImGearPDF.DoubleToFixed(scaleRatio),
                D = ImGearPDF.DoubleToFixed(scaleRatio),
            };
            return Concat(matrix, scaleMatrix);
        }

        private static ImGearPDFFixedMatrix ConcatRotation(ImGearPDFFixedMatrix matrix, double rotateAngle)
        {
            // Concatenate clockwise rotation around the origin by rotateAngle radians to the input matrix.
            // The origin is usually the lower left corner of the page and is defined by the page's MediaBox.
            ImGearPDFFixedMatrix rotateMatrix = new ImGearPDFFixedMatrix
            {
                A = ImGearPDF.DoubleToFixed(Math.Cos(rotateAngle)),
                B = ImGearPDF.DoubleToFixed(-Math.Sin(rotateAngle)),
                C = ImGearPDF.DoubleToFixed(Math.Sin(rotateAngle)),
                D = ImGearPDF.DoubleToFixed(Math.Cos(rotateAngle)),
            };
            return Concat(matrix, rotateMatrix);
        }

        // Apply the transform matrix to each element in the page's (or other container's) content.
        private static void TransformElement(ImGearPDEElement pdeElement, ImGearPDFFixedMatrix transformMatrix, List<int> transformedIDs)
        {
            if (!transformedIDs.Contains(pdeElement.UniqueId))
            {
                transformedIDs.Add(pdeElement.UniqueId);
                switch (pdeElement.Type)
                {
                    case ImGearPDEType.CONTAINER:
                        ImGearPDEContainer pdeContainer = (ImGearPDEContainer)pdeElement;
                        using (ImGearPDEContent moreContent = pdeContainer.GetContent())
                            TransformPageContent(moreContent, transformMatrix, transformedIDs);
                        break;
                    case ImGearPDEType.CLIP:
                        ImGearPDEClip pdeClip = (ImGearPDEClip)pdeElement;
                        for (int i = pdeClip.ElementCount - 1; i >= 0; --i)
                            using (ImGearPDEElement anotherElement = pdeClip.GetElement(i))
                                TransformElement(anotherElement, transformMatrix, transformedIDs);
                        break;
                    case ImGearPDEType.GROUP:
                        ImGearPDEGroup pdeGroup = (ImGearPDEGroup)pdeElement;
                        using (ImGearPDEContent moreContent = pdeGroup.GetContent())
                            TransformPageContent(moreContent, transformMatrix, transformedIDs);
                        break;
                    case ImGearPDEType.TEXT:
                        ImGearPDEText pdeText = (ImGearPDEText)pdeElement;
                        for (int i = 0; i < pdeText.RunsCount; ++i)
                            pdeText.RunSetMatrix(i, Concat(pdeText.GetMatrix(ImGearPDETextFlags.RUN, i), transformMatrix));
                        break;
                    case ImGearPDEType.FORM:
                        ImGearPDEForm pdeForm = (ImGearPDEForm)pdeElement;
                        pdeForm.SetMatrix(Concat(pdeForm.GetMatrix(), transformMatrix));
                        using (ImGearPDEContent moreContent = pdeForm.GetContent())
                            TransformPageContent(moreContent, transformMatrix, transformedIDs);
                        break;
                    default:
                        pdeElement.SetMatrix(Concat(pdeElement.GetMatrix(), transformMatrix));
                        break;
                }
                if (pdeElement.Type != ImGearPDEType.CLIP)
                    using (ImGearPDEElement pdeClip = pdeElement.GetClip())
                        if (pdeClip != null && pdeClip.Type == ImGearPDEType.CLIP)
                            TransformElement(pdeClip, transformMatrix, transformedIDs);
            }
        }

        // Apply the transform matrix to the page's (or other container's) content.
        private static void TransformPageContent(ImGearPDEContent content, ImGearPDFFixedMatrix transformMatrix, List<int> transformedIDs)
        {
            ImGearPDEContentAttrs contentAttributes = content.GetAttributes();
            contentAttributes.Matrix = Concat(contentAttributes.Matrix, transformMatrix);
            for (int i = content.ElementCount - 1; i >= 0; i--)
                using (ImGearPDEElement pdeElement = content.GetElement(i))
                    TransformElement(pdeElement, transformMatrix, transformedIDs);
        }

        // Apply scaling and rotation (about the center of the page) to a page.
        private static void TransformPage(ImGearPDFDocument imGearPDFDocument, int pageNumber, double scalingRatio, double clockwiseRotation)
        {

            // Get the page to scale and rotate.
            ImGearPDFPage imGearPDFPage = (ImGearPDFPage)imGearPDFDocument.Pages[pageNumber];
            
            // Get the object dictionary for the page.
            using (ImGearPDFBasDict pageDict = imGearPDFPage.GetDictionary())
            {
                // the MediaBox array is [lower left X, lower left Y, upper right X, upper right Y].
                ImGearPDFAtom mediaBoxKey = new ImGearPDFAtom("MediaBox");
                ImGearPDFBasArray mediaBox = (ImGearPDFBasArray)pageDict.Get(mediaBoxKey);
                double mediaBoxLowerLeftX = mediaBox.Get(0).Type == ImGearPDFBasicType.INTEGER ? ((ImGearPDFBasInt)(mediaBox.Get(0))).Value : ImGearPDF.FixedToDouble(((ImGearPDFBasFixed)(mediaBox.Get(0))).Value);
                double mediaBoxLowerLeftY = mediaBox.Get(1).Type == ImGearPDFBasicType.INTEGER ? ((ImGearPDFBasInt)(mediaBox.Get(1))).Value : ImGearPDF.FixedToDouble(((ImGearPDFBasFixed)(mediaBox.Get(1))).Value);
                double mediaBoxUpperRightX = mediaBox.Get(2).Type == ImGearPDFBasicType.INTEGER ? ((ImGearPDFBasInt)(mediaBox.Get(2))).Value : ImGearPDF.FixedToDouble(((ImGearPDFBasFixed)(mediaBox.Get(2))).Value);
                double mediaBoxUpperRightY = mediaBox.Get(3).Type == ImGearPDFBasicType.INTEGER ? ((ImGearPDFBasInt)(mediaBox.Get(3))).Value : ImGearPDF.FixedToDouble(((ImGearPDFBasFixed)(mediaBox.Get(3))).Value);

                // Calculate the adjusted page width and height so it will fit on the page after being scaled and rotated.
                double radiansRotation = Math.PI * clockwiseRotation / 180.0;
                double scaledWidth = (mediaBoxUpperRightX - mediaBoxLowerLeftX) * scalingRatio;
                double scaledHeight = (mediaBoxUpperRightY - mediaBoxLowerLeftY) * scalingRatio;
                double newWidth = scaledWidth * Math.Abs(Math.Cos(radiansRotation)) + scaledHeight * Math.Abs(Math.Sin(radiansRotation));
                double newHeight = scaledHeight * Math.Abs(Math.Cos(radiansRotation)) + scaledWidth * Math.Abs(Math.Sin(radiansRotation));

                // Calculate the transform matrix to perform the scaling and rotation. Translate to and from origin to facilitate rotation about the origin.
                ImGearPDFFixedMatrix transformMatrix = new ImGearPDFFixedMatrix 
                { 
                    A = ImGearPDF.DoubleToFixed(1.0), 
                    D = ImGearPDF.DoubleToFixed(1.0)
                };
                transformMatrix = ConcatTranslation(transformMatrix, -(mediaBoxUpperRightX - mediaBoxLowerLeftX) / 2.0, -(mediaBoxUpperRightY - mediaBoxLowerLeftY) / 2.0);
                transformMatrix = ConcatScaling(transformMatrix, scalingRatio);
                transformMatrix = ConcatRotation(transformMatrix, radiansRotation);
                transformMatrix = ConcatTranslation(transformMatrix, newWidth / 2.0, newHeight / 2.0);

                // Apply the transform to the content of the page.
                using (ImGearPDEContent content = imGearPDFPage.GetContent())
                {
                    List<int> transformedIDs = new List<int>();
                    TransformPageContent(content, transformMatrix, transformedIDs);
                    imGearPDFPage.SetContent();
                }
                imGearPDFPage.ReleaseContent();

                // Update the MediaBox to the new size.
                using (ImGearPDFBasArray newMediaBox = new ImGearPDFBasArray((ImGearPDFDocument)imGearPDFPage.Document, false, 4))
                {
                    newMediaBox.PutFixed(0, false, ImGearPDF.DoubleToFixed(0.0));
                    newMediaBox.PutFixed(1, false, ImGearPDF.DoubleToFixed(0.0));
                    newMediaBox.PutFixed(2, false, ImGearPDF.DoubleToFixed(newWidth));
                    newMediaBox.PutFixed(3, false, ImGearPDF.DoubleToFixed(newHeight));
                    pageDict.Put(mediaBoxKey, newMediaBox);

                    // Remove the CropBox, if any.
                    ImGearPDFAtom cropBoxKey = new ImGearPDFAtom("CropBox");
                    if (pageDict.Known(cropBoxKey))
                        pageDict.Remove(cropBoxKey);
                }
            }
        }

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

            // Load PDF document.
            using (Stream stream = new FileStream(@"../../../../../../Sample Input/samplepdf.pdf", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (ImGearDocument imGearDocument = ImGearFileFormats.LoadDocument(stream, 0, -1))
                {
                    // Intrinsically scale and rotate the first page. Make the first page 50% smaller and rotate it clockwise 60 degrees
                    ImGearPDFDocument imGearPDFDocument = (ImGearPDFDocument)imGearDocument;
                    TransformPage(imGearPDFDocument, 0, 0.5, 60.0);

                    // Save the PDF document.
                    using (Stream outputStream = new FileStream(@"../../../../../../Sample Output/IntrinsicallyRotatePDFPage.pdf", FileMode.Create, FileAccess.Write))
                        imGearPDFDocument.Save(outputStream, ImGearSavingFormats.PDF, 0, 0, -1, ImGearSavingModes.OVERWRITE);
                }
            }

            // Terminate PDF support once for each call to Initialize PDF support.
            ImGearPDF.Terminate();
        }
    }
}
