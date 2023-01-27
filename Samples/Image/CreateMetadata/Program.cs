using System.IO;
using ImageGear.Core;
using ImageGear.Formats;
using ImageGear.Formats.XMP;
using ImageGear.Formats.XMP.DublinCore;
using ImageGear.Formats.XMP.Photoshop;
using ImageGear.Formats.XMP.RightsManagement;

namespace AddMetadata
{
    internal class Program
    {
        static void Main()
        {
            // Initialize common formats.
            ImGearCommonFormats.Initialize();

            // Load an image page.
            ImGearPage imGearPage = null;
            using (FileStream inputStream = new FileStream(@"../../../../../../Sample Input/water.jpg", FileMode.Open, FileAccess.Read, FileShare.Read))
                imGearPage = ImGearFileFormats.LoadPage(inputStream);

            // Add an XMP Metadata structure to the image page.
            imGearPage.Metadata.XMP = new ImGearXMPMetadataRoot();
            ImGearXMPMetadataRoot xmpMetadata = (ImGearXMPMetadataRoot)imGearPage.Metadata.XMP;

            // Create IPTC Photo Metadata for Creator field, XMP dc:Creator.
            string creator = "Accusoft Corporation";
            xmpMetadata.DublinCore = new ImGearDublinCoreXMPMetadata();
            xmpMetadata.DublinCore.Creator = new ImGearXMPSeq<ImGearXMPString>(new string[] { creator });

            // Create IPTC Photo Metadata for Credit Line field, XMP photoshop:Credit.
            string creditLine = "Accusoft Corporation";
            xmpMetadata.Photoshop = new ImGearPhotoshopXMPMetadata();
            xmpMetadata.Photoshop.Credit = new ImGearXMPString(creditLine);

            // Create IPTC Photo Metadata for Copyright Notice field, XMP dc:Rights.
            string copyrightNotice = "Copyright Accusoft Corporation";
            xmpMetadata.DublinCore.Rights = new ImGearXMPLangAlt();
            xmpMetadata.DublinCore.Rights.Default = copyrightNotice;

            // Create IPTC Photo Metadata for Web Statement of Rights, XMP xmpRights:WebStatement.
            string webStatementOfRights = "https://github.com/Accusoft/imagegear-net-samples/blob/master/LICENSE";
            xmpMetadata.RightsManagement = new ImGearRightsManagementXMPMetadata();
            xmpMetadata.RightsManagement.WebStatement = new ImGearXMPString(webStatementOfRights);

            // Save image page.
            using (FileStream outputStream = new FileStream(@"../../../../../../Sample Output/CreateMetadata.jpg", FileMode.Create))
                ImGearFileFormats.SavePage(imGearPage, outputStream, ImGearSavingFormats.JPG);
        }
    }
}
