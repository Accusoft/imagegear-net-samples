using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using ImageGear.Core;
using ImageGear.Formats;
using ImageGear.Formats.Office;
using ImageGear.Formats.Office.Metadata;
using ImageGear.Formats.PDF;

namespace ReadOfficeMetadata
{
    class Program
    {
        static void Main()
        {
            // Initialize common formats.
            ImGearCommonFormats.Initialize();

            // Add PDF format to filters list.
            ImGearFileFormats.Filters.Add(ImGearPDF.CreatePDFFormat());

            // Add Word format to filters list.
            ImGearFileFormats.Filters.Add(ImGearOffice.CreateWordFormat());

            // Initialize PDF support. Initialize for each process or thread.
            ImGearPDF.Initialize();

            // Initialize Office support for each process or thread by providing a path to your LibreOffice installation.
            // See ImageGear documentation for more information.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                ImGearOffice.Initialize(@"path/to/linux/libreoffice/bin/directory");
            else
                ImGearOffice.Initialize(@"path\to\windows\libreoffice\bin\directory");

            // Load image page.
            ImGearDocument imGearDocument;
            using (FileStream stream = new FileStream(@"../../../../../../../Sample Input/word-sample.docx", FileMode.Open, FileAccess.Read, FileShare.Read))
                imGearDocument = ImGearFileFormats.LoadDocument(stream);

            // Use a StringBuilder to print metadata.
            StringBuilder outputStringBuilder = new StringBuilder();
            ImGearOfficeMetadata metadata = (ImGearOfficeMetadata)imGearDocument.Metadata.Office;

            outputStringBuilder.AppendFormat("Creator: {0} \n", metadata.Creator);
            outputStringBuilder.AppendFormat("Created: {0} \n", metadata.Created);
            outputStringBuilder.AppendFormat("Last Modified: {0} \n", metadata.LastModifiedBy);
            outputStringBuilder.AppendFormat("Modified: {0} \n", metadata.Modified);
            outputStringBuilder.AppendFormat("Revision: {0} \n", metadata.Revision);
            outputStringBuilder.AppendFormat("Subject: {0} \n", metadata.Subject);
            outputStringBuilder.AppendFormat("Title: {0} \n", metadata.Title);

            // Output the resulting string.
            Console.Write(outputStringBuilder.ToString());

            // Dispose the document
            imGearDocument?.Dispose();

            // Terminate Office support once for each call to Initialize Office support.
            ImGearOffice.Terminate();

            // Terminate PDF support once for each call to Initialize PDF support.
            ImGearPDF.Terminate();
        }
    }
}
