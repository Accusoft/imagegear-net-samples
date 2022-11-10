using System.IO;
using ImageGear.Formats;
using ImageGear.Formats.PDF;
using ImageGear.Formats.PDF.Forms;

namespace SetPDFFormFields
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

            // Load a PDF document.
            using (Stream stream = new FileStream(@"../../../../../../../Sample Input/BlankFormFields.pdf", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (ImGearPDFDocument imGearPDFDocument = (ImGearPDFDocument)ImGearFileFormats.LoadDocument(stream, 0, -1))
                {

                    if (imGearPDFDocument.Form != null)
                    {

                        // This sample processes every field in the form
                        foreach (Field field in imGearPDFDocument.Form.Fields)
                        {

                            // Set a value for each type of field except a pushbutton field, which has no value, and a signature field
                            // Use field.FullName to determine exact fields by name
                            switch (field.Type)
                            {
                                case FieldType.TextField:
                                    var textField = field as TextField;
                                    string newValue = "This is a text field";
                                    if (textField.MaxLength < newValue.Length)
                                        textField.MaxLength = newValue.Length;
                                    textField.Value = newValue;
                                    break;
                                case FieldType.CheckboxField:
                                    var checkBox = field as CheckBox;
                                    checkBox.Checked = !checkBox.Checked;
                                    break;
                                case FieldType.RadioButtonField:
                                    var radioGroup = field as RadioGroup;
                                    radioGroup.SelectedIndex = 1;
                                    break;
                                case FieldType.ComboBoxField:
                                    var comboBox = field as ComboBox;
                                    comboBox.Edit = false;
                                    comboBox.SelectedIndex = 1;
                                    break;
                                case FieldType.ListBoxField:
                                    var listBox = field as ListBox;
                                    listBox.MultiSelect = true;
                                    listBox.SelectedIndices = new int[] { 1, 2 };
                                    break;
                                case FieldType.PushButtonField:
                                    break;
                                case FieldType.SignatureField:
                                    break;
                            }
                        }
                    }

                    // Save the PDF document, which will save the changed field values.
                    using (Stream outputStream = new FileStream(@"../../../../../../../Sample Output/SetPDFFormFields.pdf", FileMode.Create, FileAccess.Write))
                    imGearPDFDocument.Save(outputStream, ImGearSavingFormats.PDF, 0, 0, -1, ImGearSavingModes.OVERWRITE);
                }
            }

            // Terminate PDF support once for each call to Initialize PDF support.
            ImGearPDF.Terminate();
        }
    }
}
