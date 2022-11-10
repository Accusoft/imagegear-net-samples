using System;
using System.IO;
using System.Text;
using ImageGear.Formats;
using ImageGear.Formats.PDF;
using ImageGear.Formats.PDF.Forms;

namespace ReadPDFFormFields
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
            using (Stream stream = new FileStream(@"../../../../../../../Sample Input/FilledFormFields.pdf", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (ImGearPDFDocument imGearPDFDocument = (ImGearPDFDocument)ImGearFileFormats.LoadDocument(stream, 0, -1))
                {

                    if (imGearPDFDocument.Form != null)
                    {

                        // Write the form field values to a string.
                        StringBuilder outputStringBuilder = new StringBuilder();
                        foreach (Field field in imGearPDFDocument.Form.Fields)
                        {
                            // Write the name and type of the form field
                            outputStringBuilder.AppendFormat("{0} ({1}): ", field.FullName, field.Type.ToString());
                            switch (field.Type)
                            {
                                case FieldType.TextField:
                                    var textField = field as TextField;
                                    // Write text field value
                                    outputStringBuilder.AppendFormat("{0} (default: {1}){2}", textField.Value, textField.DefaultValue, Environment.NewLine);
                                    break;
                                case FieldType.CheckboxField:
                                    var checkBox = field as CheckBox;
                                    // Write checkbox field value and default
                                    outputStringBuilder.AppendFormat("{0} (default: {1}){2}", checkBox.Checked.ToString(), 
                                        checkBox.CheckedByDefault.ToString(), Environment.NewLine);
                                    break;
                                case FieldType.RadioButtonField:
                                    var radioGroup = field as RadioGroup;
                                    // Write the names of the radio buttons in the radio button group
                                    foreach (ChoiceOption option in radioGroup.Options)
                                        outputStringBuilder.AppendFormat("{0} ", option.ExportValue);
                                    // Write the name of the selected radio button
                                    outputStringBuilder.AppendFormat(" (current: {0})", radioGroup.Values.Length > 0 ? radioGroup.Values[0] : "<None>");
                                    // Write the index (zero based) of the selected radio button
                                    outputStringBuilder.AppendFormat(" (selected: {0}){1}", radioGroup.SelectedIndex, Environment.NewLine);
                                    break;
                                case FieldType.ComboBoxField:
                                    var comboBox = field as ComboBox;
                                    // Write the names and labels of the items in the combobox
                                    foreach (ChoiceOption option in comboBox.Options)
                                        outputStringBuilder.AppendFormat("'{0}';'{1}' ", option.ExportValue, option.Label);
                                    // Write the name of the selected combobox item
                                    outputStringBuilder.AppendFormat(" (current: '{0}')", comboBox.Value);
                                    // Write the index (zero based) of the selected combobox item
                                    outputStringBuilder.AppendFormat(" (selected: {0}){1}", comboBox.SelectedIndex, Environment.NewLine);
                                    break;
                                case FieldType.ListBoxField:
                                    var listBox = field as ListBox;
                                    // Write the names and labels of the items in the dropdown list
                                    foreach (ChoiceOption option in listBox.Options)
                                        outputStringBuilder.AppendFormat("'{0}';'{1}' ", option.ExportValue, option.Label);
                                    // Write the names of the selected items in the dropdown list
                                    outputStringBuilder.AppendFormat(" (current:");
                                    foreach (string value in listBox.Values)
                                        outputStringBuilder.AppendFormat(" '{0}'", value);
                                    outputStringBuilder.AppendFormat(")");
                                    // Write the indices (zero based) of the selected items in the dropdown list
                                    outputStringBuilder.AppendFormat(" (selected:");
                                    foreach (int index in listBox.SelectedIndices)
                                        outputStringBuilder.AppendFormat(" {0}", index);
                                    outputStringBuilder.AppendFormat("){0}", Environment.NewLine);
                                    break;
                                case FieldType.PushButtonField:
                                    break;
                                case FieldType.SignatureField:
                                    break;
                            }
                        }

                        // Output the resulting string.
                        Console.Write(outputStringBuilder.ToString());
                    }
                }
            }

            // Terminate PDF support once for each call to Initialize PDF support.
            ImGearPDF.Terminate();
        }
    }
}
