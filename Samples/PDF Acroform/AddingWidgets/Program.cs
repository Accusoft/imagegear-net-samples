using System.IO;
using ImageGear.Core;
using ImageGear.Formats;
using ImageGear.Formats.PDF;
using ImageGear.Formats.PDF.Forms;

namespace AddingWidgets
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

            // Load PDF document from a file.
            using (Stream stream = new FileStream(@"../../../../../../../Sample Input/accusoft-brochure.pdf", FileMode.Open, FileAccess.Read, FileShare.None))
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

                    // Create new PDF page in the PDF document with the same dimensions as the first page.
                    using (ImGearPDFPage pdfPage = pdfDocument.CreateNewPage((int)ImGearPDFPageNumber.BEFORE_FIRST_PAGE, pageRectangle))
                    {
                        // Create new field rectangles for new fields on the newly created page.
                        ImGearPDFFixedRect radioButtonLocation1 = new ImGearPDFFixedRect
                        {
                            Left = ImGearPDF.IntToFixed(rectangle.Width / 3 - 200),
                            Top = ImGearPDF.IntToFixed(-rectangle.Height / 3 + 20),
                            Right = ImGearPDF.IntToFixed(rectangle.Width / 3 + 200),
                            Bottom = ImGearPDF.IntToFixed(-rectangle.Height / 3 - 20),
                        };

                        ImGearPDFFixedRect radioButtonLocation2 = new ImGearPDFFixedRect
                        {
                            Left = ImGearPDF.IntToFixed(rectangle.Width / 3 + 200),
                            Top = ImGearPDF.IntToFixed(-rectangle.Height / 3 + 20),
                            Right = ImGearPDF.IntToFixed(rectangle.Width / 3 + 600),
                            Bottom = ImGearPDF.IntToFixed(-rectangle.Height / 3 - 20),
                        };

                        ImGearPDFFixedRect textFieldLocation1 = new ImGearPDFFixedRect
                        {
                            Left = ImGearPDF.IntToFixed(rectangle.Width / 3 - 200),
                            Top = ImGearPDF.IntToFixed(-rectangle.Height / 6 + 40),
                            Right = ImGearPDF.IntToFixed(rectangle.Width / 3 + 200),
                            Bottom = ImGearPDF.IntToFixed(-rectangle.Height / 6 - 20),
                        };

                        ImGearPDFFixedRect textFieldLocation2 = new ImGearPDFFixedRect
                        {
                            Left = ImGearPDF.IntToFixed(rectangle.Width / 3 + 250),
                            Top = ImGearPDF.IntToFixed(-rectangle.Height / 6 + 40),
                            Right = ImGearPDF.IntToFixed(rectangle.Width / 3 + 700),
                            Bottom = ImGearPDF.IntToFixed(-rectangle.Height / 6 - 20),
                        };

                        // Create a radio group to hold the radio buttons.
                        RadioGroup radioGroup = pdfDocument.Form.CreateRadioGroup("Color");

                        // Set widget and put in locations.
                        RadioButtonWidgetAnnotation radioButton1 = new RadioButtonWidgetAnnotation(pdfPage, radioButtonLocation1);

                        RadioButtonWidgetAnnotation radioButton2 = new RadioButtonWidgetAnnotation(pdfPage, radioButtonLocation2);

                        // Link option and assign an export.
                        ChoiceOption option1 = radioGroup.AddOption(radioButton1, "Blue");
                        ChoiceOption option2 = radioGroup.AddOption(radioButton2, "Green");

                        // Create text boxes and assign locations.
                        TextBoxWidgetAnnotation textWidget1 = new TextBoxWidgetAnnotation(pdfPage, textFieldLocation1);
                        TextField textField = pdfDocument.Form.CreateTextField("Name", textWidget1);
                        TextBoxWidgetAnnotation textWidget2 = new TextBoxWidgetAnnotation(pdfPage, textFieldLocation2);
                        textField.AddWidget(textWidget2);

                        // Save the PDF document.
                        using (Stream streamOut = new FileStream(@"../../../../../../../Sample Output/AddingWidgets.pdf", FileMode.Create, FileAccess.Write, FileShare.None))
                            ImGearFileFormats.SaveDocument(pdfDocument, streamOut, 0, ImGearSavingModes.OVERWRITE, ImGearSavingFormats.PDF, new ImGearSaveOptions());
                    }
                }
            }

            // Terminate PDF support once for each call to Initialize PDF support.
            ImGearPDF.Terminate();
        }
    }
}
