using System;
using System.IO;
using ImageGear.Formats;
using ImageGear.Formats.PDF;

namespace ManagePDFMetadata
{
    internal class Program
    {
        static void Main()
        {
            // Initialize common formats.
            ImGearCommonFormats.Initialize();

            // Add PDF format to the format list.
            ImGearFileFormats.Filters.Add(ImGearPDF.CreatePDFFormat());

            // Initialize PDF format for each using thread.
            ImGearPDF.Initialize();

            // Load a PDF document.
            ImGearPDFDocument igPDFDocument = null;
            using (Stream stream = new FileStream(@"../../../../../../../Sample Input/multi-page.pdf", FileMode.Open, FileAccess.Read, FileShare.Read))
                igPDFDocument = (ImGearPDFDocument)ImGearFileFormats.LoadDocument(stream, 0, -1);

            // Read and update Author.
            string author = igPDFDocument.GetInfo("Author");
            string newAuthor = "Accusoft Corporation";
            igPDFDocument.SetInfo("Author", newAuthor);
            Console.WriteLine($"Author: \"{author}\" -> \"{newAuthor}\"");

            // Read Created. (ImageGear will overwrite its value on document save.)
            string created = igPDFDocument.GetInfo("Created");
            Console.WriteLine($"Created: \"{created}\"");

            // Read and update Creator.
            string creator = igPDFDocument.GetInfo("Creator");
            string newCreator = "ManagePDFMetadata Sample";
            igPDFDocument.SetInfo("Creator", newCreator);
            Console.WriteLine($"Creator: \"{creator}\" -> \"{newCreator}\"");

            // Read and update Keywords.
            string keywords = igPDFDocument.GetInfo("Keywords");
            string newKeywords = "brochure document photo imaging SDK";
            igPDFDocument.SetInfo("Keywords", newKeywords);
            Console.WriteLine($"Keywords: \"{keywords}\" -> \"{newKeywords}\"");

            // Read Modified. (ImageGear will overwrite its value on document save.)
            string modified = igPDFDocument.GetInfo("Modified");
            Console.WriteLine($"Modified: \"{modified}\"");

            // Read Producer. (ImageGear will overwrite its value on document save.)
            string producer = igPDFDocument.GetInfo("Producer");
            Console.WriteLine($"Producer: \"{producer}\"");

            // Read and update Subject.
            string subject = igPDFDocument.GetInfo("Subject");
            string newSubject = "ImageGear";
            igPDFDocument.SetInfo("Subject", newSubject);
            Console.WriteLine($"Subject: \"{subject}\" -> \"{newSubject}\"");

            // Read and update Title.
            string title = igPDFDocument.GetInfo("Title");
            string newTitle = "ImageGear Product Brochure (2015)";
            igPDFDocument.SetInfo("Title", newTitle);
            Console.WriteLine($"Title: \"{title}\" -> \"{newTitle}\"");

            // Save the PDF document.
            using (Stream outputStream = new FileStream(@"../../../../../../../Sample Output/ManagePDFMetadata.pdf", FileMode.Create, FileAccess.Write))
                igPDFDocument.Save(outputStream, ImGearSavingFormats.PDF, 0, 0, -1, ImGearSavingModes.OVERWRITE);

            // Terminate PDF support once for each call to Initialize PDF support.
            ImGearPDF.Terminate();
        }
    }
}
