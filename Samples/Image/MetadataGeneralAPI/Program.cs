using System;
using System.IO;
using System.Text;
using ImageGear.Core;
using ImageGear.Evaluation;
using ImageGear.Formats;

namespace MetadataGeneralAPI
{
    class Program
    {
        private static void RecursivelyWriteMetadata(ImGearMetadataTree tree, StringBuilder outputStringBuilder)
        {
            foreach (ImGearMetadataNode child in tree.Children)
            {
                // If the child is a leaf, then parse its data. 
                if (child is ImGearMetadataLeaf leaf)
                {
                    // If the data is an array, record its elements as a comma-separated list.
                    if (leaf.Data is Array items)
                    {
                        outputStringBuilder.AppendFormat("{0}: [", leaf.Name);
                        foreach (object item in items)
                        {
                            outputStringBuilder.AppendFormat("{0},", item);
                        }
                        outputStringBuilder.Append("]\r\n");
                    }

                    // Otherwise simply record the data as a string.
                    else
                    {
                        outputStringBuilder.AppendFormat("{0}: {1}\r\n", leaf.Name, leaf.Data);
                    }
                }

                // If the child is itself a tree, then recursively call this function.
                else if (child is ImGearMetadataTree subtree)
                {
                    outputStringBuilder.AppendFormat("begin {0}\r\n", subtree.Name);
                    RecursivelyWriteMetadata(subtree, outputStringBuilder);
                    outputStringBuilder.AppendFormat("end {0}\r\n", subtree.Name);
                }
            }
        }

        static void Main()
        {
            // Initialize evaluation license.
            ImGearEvaluationManager.Initialize();

            // Initialize common formats.
            ImGearCommonFormats.Initialize();

            // Load image page.
            ImGearPage imGearPage;
            using (FileStream stream = new FileStream(@"../../../../../../Sample Input/Bitonal-Cleanup-Sample.tif", FileMode.Open, FileAccess.Read, FileShare.Read))
                imGearPage = ImGearFileFormats.LoadPage(stream, 0);

            // Collect image metadata into a StringBuilder.
            StringBuilder outputStringBuilder = new StringBuilder();
            ImGearMetadataTree metadata = (ImGearMetadataTree)imGearPage.Metadata.Child;
            RecursivelyWriteMetadata(metadata, outputStringBuilder);

            // Output the resulting string.
            Console.Write(outputStringBuilder.ToString());
        }
    }
}
