using System;
using System.IO;
using System.Collections.Generic;
using ImageGear.Core;
using ImageGear.Evaluation;
using ImageGear.Formats;
using ImageGear.Formats.PDF;

namespace SaveAllImagesOnPDFPage
{
    class Program
    {
        private static int imageIndex = 1;

        // Traverse each element in the content. Process each image element.
        private static void SaveImagesInElement(ImGearPDEElement igPDEElement, List<int> transformedIDs)
        {
            if (!transformedIDs.Contains(igPDEElement.UniqueId))
            {
                transformedIDs.Add(igPDEElement.UniqueId);
                switch (igPDEElement.Type)
                {
                    case ImGearPDEType.CLIP:
                        ImGearPDEClip igPDEClip = (ImGearPDEClip)igPDEElement;
                        for (int index = igPDEClip.ElementCount - 1; index >= 0; index--)
                            using (ImGearPDEElement anotherElement = igPDEClip.GetElement(index))
                                SaveImagesInElement(anotherElement, transformedIDs);
                        break;
                    case ImGearPDEType.CONTAINER:
                        ImGearPDEContainer igPDEContainer = (ImGearPDEContainer)igPDEElement;
                        using (ImGearPDEContent moreContent = igPDEContainer.GetContent())
                            SaveImagesInContent(moreContent, transformedIDs);
                        break;
                    case ImGearPDEType.GROUP:
                        ImGearPDEGroup igPDEGroup = (ImGearPDEGroup)igPDEElement;
                        using (ImGearPDEContent moreContent = igPDEGroup.GetContent())
                            SaveImagesInContent(moreContent, transformedIDs);
                        break;
                    case ImGearPDEType.FORM:
                        ImGearPDEForm igPDEForm = (ImGearPDEForm)igPDEElement;
                        using (ImGearPDEContent moreContent = igPDEForm.GetContent())
                            SaveImagesInContent(moreContent, transformedIDs);
                        break;
                    case ImGearPDEType.IMAGE:

                        // Get the raster image from the image element.
                        ImGearPDEImage igPDEImage = (ImGearPDEImage)igPDEElement;
                        ImGearPage igPage = igPDEImage.ToImGearPage();

                        // Save image page.
                        using (Stream outputStream = new FileStream(String.Format(@"../../../../../../Sample Output/SaveAllImagesOnPDFPage_{0}.png", imageIndex), FileMode.Create))
                            ImGearFileFormats.SavePage(igPage, outputStream, 1, ImGearSavingModes.OVERWRITE, ImGearSavingFormats.PNG);
                        imageIndex++;
                        break;

                    default:
                        break;
                }
                if (igPDEElement.Type != ImGearPDEType.CLIP)
                    using (ImGearPDEElement igPDEClip = igPDEElement.GetClip())
                        if (igPDEClip != null && igPDEClip.Type == ImGearPDEType.CLIP)
                            SaveImagesInElement(igPDEClip, transformedIDs);
            }
        }

        // Process the content.
        private static void SaveImagesInContent(ImGearPDEContent igPDEContent, List<int> visitedElementIDs)
        {
            for (int index = igPDEContent.ElementCount - 1; index >= 0; index--)
                using (ImGearPDEElement igPDEElement = igPDEContent.GetElement(index))
                    SaveImagesInElement(igPDEElement, visitedElementIDs);
        }

        private static void SaveAllImagesOnPage(ImGearPDFDocument imGearPDFDocument, int pageNumber)
        {

            // Get the page for image extraction.
            using (ImGearPDFPage imGearPDFPage = (ImGearPDFPage)imGearPDFDocument.Pages[pageNumber])
            {

                // Get the object dictionary for the page.
                using (ImGearPDFBasDict pageDict = imGearPDFPage.GetDictionary())
                {

                    // Extract the images from the page's content.
                    using (ImGearPDEContent content = imGearPDFPage.GetContent())
                    {
                        List<int> visitedElementIDs = new List<int>();
                        SaveImagesInContent(content, visitedElementIDs);
                    }
                    imGearPDFPage.ReleaseContent();
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

            // Load a PDF document.
            using (Stream stream = new FileStream(@"../../../../../../Sample Input/samplepdf.pdf", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (ImGearPDFDocument igPDFDocument = (ImGearPDFDocument)ImGearFileFormats.LoadDocument(stream, 0, -1))
                {
                    // Save all images on the second page of the PDF document.
                    SaveAllImagesOnPage(igPDFDocument, 1);
                }
            }

            // Terminate PDF support once for each call to Initialize PDF support.
            ImGearPDF.Terminate();
        }
    }
}
