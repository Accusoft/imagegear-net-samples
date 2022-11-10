using System;
using System.IO;
using ImageGear.Formats;
using ImageGear.Formats.Email;

namespace ExtractEmailMetadata
{
    class Program
    {
        static void Main()
        {
            // Initialize common formats.
            ImGearCommonFormats.Initialize();

            // Add Email formats to filters list.
            ImGearFileFormats.Filters.Add(ImGearEmail.CreateMSGFormat());
            ImGearFileFormats.Filters.Add(ImGearEmail.CreateEMLFormat());

            // Load an Email document.
            using (Stream stream = new FileStream(@"../../../../../../../Sample Input/EmailWithMetadata.eml", FileMode.Open, FileAccess.Read, FileShare.Read))
            using (ImGearEmailDocument emailDocument = ImGearFileFormats.LoadDocument(stream) as ImGearEmailDocument)
            {
                // Output Email metadata
                Console.WriteLine("Subject: " + emailDocument.Subject);
                Console.WriteLine("Sender: " + emailDocument.Sender);
                Console.WriteLine("Sent on: " + emailDocument.SentOn);
                Console.WriteLine("Recipient: " + emailDocument.RecipientsTo[0]);
                Console.WriteLine("BCC: " + emailDocument.RecipientsBcc[0]);
                Console.WriteLine("CC: " + emailDocument.RecipientsCc[0]);
            }
        }
    }
}
