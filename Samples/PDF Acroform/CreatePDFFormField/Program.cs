using System.IO;
using ImageGear.Evaluation;
using ImageGear.Core;
using ImageGear.Formats;
using ImageGear.Formats.PDF;
using ImageGear.Formats.PDF.Forms;

namespace CreatePDFFormField
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

            // Load PDF document from a file.
            using (Stream stream = new FileStream(@"../../../../../../Sample Input/accusoft-brochure.pdf", FileMode.Open, FileAccess.Read, FileShare.None))
            {
                using (ImGearPDFDocument pdfDocument = (ImGearPDFDocument)ImGearFileFormats.LoadDocument(stream))
                {
                    // If the document does not have a form, create a form for the document.
                    if (pdfDocument.Form == null)
                        pdfDocument.CreateForm();

                    // Create the rectangle of new PDF page with the same size as visible area of first page.
                    ImGearRectangle rectangle = ((ImGearPDFPage)pdfDocument.Pages[0]).CropRectangle;
                    ImGearPDFFixedRect pageRectangle = new ImGearPDFFixedRect
                    {
                        Left = ImGearPDF.IntToFixed(0),
                        Top = ImGearPDF.IntToFixed(rectangle.Top),
                        Right = ImGearPDF.IntToFixed(rectangle.Right),
                        Bottom = ImGearPDF.IntToFixed(0)
                    };

                    // Create new PDF page in the PDF document with landscape Letter size.
                    using (ImGearPDFPage pdfPage = pdfDocument.CreateNewPage((int)ImGearPDFPageNumber.BEFORE_FIRST_PAGE, pageRectangle))
                    {

                        // Create new field rectangle for new field on the center of created page.
                        ImGearPDFFixedRect fieldBoundingBox = new ImGearPDFFixedRect
                        {
                            Left = ImGearPDF.IntToFixed(rectangle.Width / 2 - 100),
                            Top = ImGearPDF.IntToFixed(-rectangle.Height / 2 + 10),
                            Right = ImGearPDF.IntToFixed(rectangle.Width / 2 + 100),
                            Bottom = ImGearPDF.IntToFixed(-rectangle.Height / 2 - 10),
                        };

                        // Create the field.
                        TextField textField = pdfDocument.Form.CreateTextField("text field", pdfPage, fieldBoundingBox);

                        // Set the value of new field.
                        textField.Value = "This is text field.";
                    }

                    // Save the PDF document.
                    using (Stream streamOut = new FileStream(@"../../../../../../Sample Output/CreatePDFFormField.pdf", FileMode.Create, FileAccess.Write, FileShare.None))
                        ImGearFileFormats.SaveDocument(pdfDocument, streamOut, 0, ImGearSavingModes.OVERWRITE, ImGearSavingFormats.PDF, new ImGearSaveOptions());
                }
            }

            // Terminate PDF support once for each call to Initialize PDF support.
            ImGearPDF.Terminate();
        }
    }
}
