using System;
using System.Diagnostics;
using System.IO;
using ImageGear.Core;
using ImageGear.Formats;
using ImageGear.Formats.XMP;

namespace MetadataSimplifiedAPIUseArraysAndStructures
{
    internal class Program
    {
        static void Main()
        {
            // Initialize common formats.
            ImGearCommonFormats.Initialize();

            // Load image page.
            ImGearPage imGearPage = null;
            using (FileStream inputStream = new FileStream(@"../../../../../../Sample Input/water.jpg", FileMode.Open, FileAccess.Read, FileShare.Read))
                imGearPage = ImGearFileFormats.LoadPage(inputStream);

            // Reset the XMP metadata structure for the image.
            imGearPage.Metadata.XMP = new ImGearXMPMetadataRoot();

            // Note: Each time you access an array or structure in the Simplified Metadata API, ImageGear recreates the data structure from current values.
            // To modify values, populate a local object and then assign back to the property. Otherwise, edits are made to a discarded duplicate object.

            // For example, the XMP.DublinCore.Subject property remains unchanged because its getter returns a new object from existing data.
            ImGearXMPMetadataRoot xmpMetadataRoot = imGearPage.Metadata.XMP as ImGearXMPMetadataRoot;
            (imGearPage.Metadata.XMP as ImGearXMPMetadataRoot).DublinCore = new ImageGear.Formats.XMP.DublinCore.ImGearDublinCoreXMPMetadata();
            xmpMetadataRoot.DublinCore.Subject = new ImGearXMPBag<ImGearXMPString>();
            xmpMetadataRoot.DublinCore.Subject.Add(new ImGearXMPString("Subject1"));
            xmpMetadataRoot.DublinCore.Subject.Add(new ImGearXMPString("Subject2"));
            Console.WriteLine($"XMP.DublinCore.Subject has {xmpMetadataRoot.DublinCore.Subject.Count} values.");

            // Instead, add values to a local object and set the XMP.DublinCore.Subject property to change its data.
            ImGearXMPBag<ImGearXMPString> subject = new ImGearXMPBag<ImGearXMPString>();
            subject.Add(new ImGearXMPString("Subject3"));
            subject.Add(new ImGearXMPString("Subject4"));
            (imGearPage.Metadata.XMP as ImGearXMPMetadataRoot).DublinCore.Subject = subject;
            Console.WriteLine($"XMP.DublinCore.Subject has {xmpMetadataRoot.DublinCore.Subject.Count} values.");

            // All arrays and structures are recreated each time they are accessed when using the Simplified Metadata API.
            // Therefore access XMP.DublinCore.Subject once for best performance.
            subject = (imGearPage.Metadata.XMP as ImGearXMPMetadataRoot).DublinCore.Subject;
            // Now the array or structure contents can be efficiently accessed as much as needed.
            for (int i = 0; i < subject.Count; i++)
            {
                String value = subject[i].Value;
            }

            // Save image page.
            using (FileStream outputStream = new FileStream(@"../../../../../../Sample Output/MetadataSimplifiedAPIUseArraysAndStructures.png", FileMode.Create))
                ImGearFileFormats.SavePage(imGearPage, outputStream, ImGearSavingFormats.PNG);
        }
    }
}
