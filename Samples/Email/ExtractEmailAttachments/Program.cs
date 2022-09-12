using System.IO;
using ImageGear.Formats;
using ImageGear.Formats.Email;

namespace EmailAttachments
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
            using (Stream stream = new FileStream(@"../../../../../../../Sample Input/EmailWithAttachments.msg", FileMode.Open, FileAccess.Read, FileShare.Read))
                using (ImGearEmailDocument emailDocument = ImGearFileFormats.LoadDocument(stream) as ImGearEmailDocument)
                    // Save all attachments to an existing folder.
                    if (emailDocument.Attachments.Count > 0)
                    {
                        emailDocument.SaveAllAttachments(@"../../../../../../../Sample Output");
                    }
        }
    }
}
