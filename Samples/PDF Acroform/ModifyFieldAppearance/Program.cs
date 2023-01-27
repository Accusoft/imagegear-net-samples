using System.IO;
using System.Drawing;
using ImageGear.Core;
using ImageGear.Formats;
using ImageGear.Formats.PDF;
using ImageGear.Formats.PDF.Forms;

namespace ModifyFieldAppearance
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
            using (Stream stream = new FileStream(@"../../../../../../../Sample Input/BlankFormFields.pdf", FileMode.Open, FileAccess.Read, FileShare.None))
            {
                using (ImGearPDFDocument pdfDocument = (ImGearPDFDocument)ImGearFileFormats.LoadDocument(stream))
                {
                    // If the document does not have a form, create a form for the document.
                    if (pdfDocument.Form == null)
                        pdfDocument.CreateForm();

                    // Text Box items for decoration.
                    pdfDocument.Form.Fields[0].TextColor = Color.Red;
                    pdfDocument.Form.Fields[0].FontSize = 45;
                    pdfDocument.Form.Fields[0].FontName = "Helvetica";

                    // Cycle through each widget and change decorations.
                    foreach (Field field in pdfDocument.Form.Fields)
                    {
                        foreach (WidgetAnnotation widget in field.Widgets)
                        {
                            // Set fill and border colors.
                            widget.FillColor = Color.Red;
                            widget.BorderColor = Color.YellowGreen;

                            // Set a thick border width (size 3).
                            // Any integer can also be used.
                            widget.BorderWidth = AnnotationBorderWidth.THICK;

                            // Set a border style.
                            widget.BorderStyle = AnnotationBorderStyle.DASHED;
                        }
                    }

                    // Save the PDF document.
                    using (Stream streamOut = new FileStream(@"../../../../../../../Sample Output/ModifyFieldAppearance.pdf", FileMode.Create, FileAccess.Write, FileShare.None))
                   ImGearFileFormats.SaveDocument(pdfDocument, streamOut, 0, ImGearSavingModes.OVERWRITE, ImGearSavingFormats.PDF, new ImGearSaveOptions());
                }
            }

            // Terminate PDF support once for each call to Initialize PDF support.
            ImGearPDF.Terminate();
        }
    }
}
