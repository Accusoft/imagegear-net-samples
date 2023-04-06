# ImageGear .NET Samples

These samples are intended to demonstrate how the various functions of ImageGear .NET can be used to perform imaging tasks. They are designed to be lightweight, concise, and show how ImageGear can be integrated into any application to perform these imaging tasks.

## How to Build these Samples

All samples can be built using Microsoft Visual Studio 2022. To build all the samples, use Visual Studio to open the samples.sln file in this directory. To build any individual sample, use Visual Studio to open the .sln file in the individual project directory. After opening the solution, select a Solution Configuration (Debug or Release) and an available Solution Platform (x64 or AnyCPU). Last, build the complete solution with Build Solution located in the Build menu.

For samples using the Accusoft.ImageGear.Office NuGet package, make sure to specify the path to your local LibreOffice installation as a parameter in the call to the ImGearOffice.Initialize() function.

## How to Run these Samples

When the solution is built, it produces one or more console application executables in 'bin' subdirectories in the individual project directories. Run the sample application by double-clicking the application icon, or running it directly from Command Prompt (cmd.exe), PowerShell, or similar. Note that the working directory must be the same as the directory containing the sample executable in order to find the sample input image and the output directory. The input image(s) and output directory are specified relative to the location of the application in all of these samples.

## System Requirements

For a list of the system and development software necessary to build and run these samples, please refer to the [ImageGear .NET Online Documentation](https://help.accusoft.com/ImageGear/latest/webframe.html#system-requirements.html).

## Imaging Support in ImageGear .NET

ImageGear .NET supports the most commonly used image, graphic, and document formats. For more information on imaging support in ImageGear .NET, [please visit us at Accusoft.](https://www.accusoft.com/products/imagegear-collection/imagegear-dot-net/)

## Sample Summary

| Category | Sample | Description |
| :-------------------------- | :--------------------------: | :---------------------------------------------------------------------------------------- |
| [***General Imaging Samples***](Samples/Image) | | These samples demonstrate general raster image functionality in ImageGear .NET. |
| | [CompressUsingOptions](Samples/Image/CompressUsingOptions) | Raster image is saved to a JPEG file using manually set conversion and compression options. |
| | [CreateMetadata](Samples/Image/CreateMetadata) | This sample uses ImageGear Simplified Metadata API to add IPTC Photo Metadata fields to an existing JPEG image. |
| | [DetectImageFormat](Samples/Image/DetectImageFormat) | After a stream is loaded, ImageGear provides functionality to automatically detect an image's format. |
| | [LoadingAndSaving](Samples/Image/LoadingAndSaving) | This sample demonstrates two of the most basic raster functions of ImageGear, loading and saving an image using the LoadPage and SavePage functions. |
| | [LoadingWithLoadOptionsAndSaving](Samples/Image/LoadingWithLoadOptionsAndSaving) | This sample demonstrates loading an image using basic LoadOptions and then saving the image using the SavePage functions. |
| | [LoadRawImage](Samples/Image/LoadRawImage) | When opening headerless image content, ImageGear can open images without headers as long as a complete description of the image data is provided. |
| | [MetadataGeneralAPI](Samples/Image/MetadataGeneralAPI) | After an image is loaded, ImageGear provides functionality to access both document and page level metadata. |
| | [MetadataSimplifiedAPI](Samples/Image/MetadataSimplifiedAPI) | After an image is loaded, ImageGear provides a simplified API to access and update both document and page level metadata. |
| | [MetadataSimplifiedAPIUseArraysAndStructures](Samples/Image/MetadataSimplifiedAPIUseArraysAndStructures) | This sample shows recommended techniques when working with the Simplified Metadata API, using XMP.DublinCore.Subject as an example. |
| | [SetFilterControlParametersGlobally](Samples/Image/SetFilterControlParametersGlobally) | This sample shows how to use global Filter Control Parameters to save a JPEG file with lossless compression instead of lossy compression. |
| | [SetFilterControlParametersLocally](Samples/Image/SetFilterControlParametersLocally) | This sample shows how to use local Filter Control Parameters to customize how ImageGear reads and writes supported file formats without affecting other threads. |
| | [SimplifiedMetadataWithNullableFields](Samples/Image/SimplifiedMetadataWithNullableFields) | After an image is loaded, ImageGear provides the ability to access the metadata without knowing the tree or if the information exists. |
| | [SpecifyColorProfilesGlobally](Samples/Image/SpecifyColorProfilesGlobally) | This sample shows how to implicitly use ICM color profiles to convert the color space in a JPEG image from RGB to CMYK. |
| | [SpecifyColorProfilesLocally](Samples/Image/SpecifyColorProfilesLocally) | This sample shows how to explicitly use ICM color profiles to convert the color space in a JPEG image from RGB to CMYK. |
| [***Image Conversion Samples***](Samples/Image%20Conversion) | | These samples demonstrate how to convert between raster image formats using ImageGear .NET. |
| | [ConvertRasterImageFormat](Samples/Image%20Conversion/ConvertRasterImageFormat) | After an ImGearPage has been retrieved, it can then be saved as any supported raster image format using ImGearFileFormats.SavePage(). |
| | [ConvertMultipageRasterImageToSVG](Samples/Image%20Conversion/ConvertMultipageRasterImageToSVG) | After a raster ImGearDocument has been retrieved, it can then be saved to SVG using ImGearFileFormats.SaveDocument(). |
| [***Image Processing Samples***](Samples/Image%20Processing) | | These samples demonstrate select image processing capabilities of ImageGear .NET. |
| | [AdjustContrastForRasterImage](Samples/Image%20Processing/AdjustContrastForRasterImage) | This sample demonstrates how to manually adjust the contrast, brightness, and gamma of a raster image. |
| | [CreateThumbnailImage](Samples/Image%20Processing/CreateThumbnailImage) | After an image is loaded, use ImGearProcessing.CreateThumbnail() to create a thumbnail with specific dimensions. Interpolation options depend on the color space and bit depth of the image. |
| | [CropAndResizeRasterImage](Samples/Image%20Processing/CropAndResizeRasterImage) | Once an image is loaded into ImageGear, it can be cropped. ImageGear crops independently on each side of the image, giving you complete control over what portion of the image to remove. |
| | [DespeckleAndDeskewRasterImage](Samples/Image%20Processing/DespeckleAndDeskewRasterImage) | After an image is scanned, it is often slightly skewed, and can also contain "noise" in the background portion of the image. These can cause OCR to perform poorly. To correct these issues, load a raster image into ImageGear and use the Despeckle and Deskew functions to "fix" the image. |
| | [EqualizeContrastForRasterImage](Samples/Image%20Processing/EqualizeContrastForRasterImage) | This sample demonstrates how to improve the contrast of the raster image using the EqualizeContrast method. |
| | [ErodeAndDilateRasterImage](Samples/Image%20Processing/ErodeAndDilateRasterImage) | The black-and-white and grayscale image can have some dot noise on it. To reduce or remove this noise, load a raster image into ImageGear and apply the Dilate and Erode operations sequentially. |
| | [FlipAndRotateRasterImage](Samples/Image%20Processing/FlipAndRotateRasterImage) | After an image is loaded, use the ImGearProcessing.Flip() method to flip the image across either its horizontal or vertical axis. Use the ImGearProcessing.Rotate() method to rotate the image degrees clockwise using one of several interpolation techniques. |
| | [ImageCleanup](Samples/Image%20Processing/ImageCleanup) | After a document is scanned, there can be undesirable artifacts on the image. Use the various image cleanup operations, such as `RemovePunchHoles` and `CleanBorders`, to clean up the image to make it suitable for further processing. |
| | [ProcessUsingRegionOfInterest](Samples/Image%20Processing/ProcessUsingRegionOfInterest) | Apply an image processing operation to a rectangluar Region of Interest (ROI) within a raster image instead of its entire area. |
| | [ProcessUsingRegionOfInterestMask](Samples/Image%20Processing/ProcessUsingRegionOfInterestMask) | Apply an image processing operation to a bitonal Region of Interest (ROI) mask within a raster image instead of its entire area. |
| [***Email Samples***](Samples/Email) | | These samples demonstrate general Email functionality in ImageGear .NET. |
| | [EmailFileToPDFFile](Samples/Email/EmailFileToPDFFile) | This sample demonstrates how to load an Email document into ImageGear and save the document as a PDF. |
| | [EmailFileToRasterFormat](Samples/Email/EmailFileToRasterFormat) | After an Email document is loaded, ImageGear can rasterize the entire document and save it as a single-page raster format. |
| | [ExtractEmailAttachments](Samples/Email/ExtractEmailAttachments) | ImageGear provides functionality to extract attachments from the Email document. |
| | [ExtractEmailMetadata](Samples/Email/ExtractEmailMetadata) | After an Email document is loaded, ImageGear provides functionality to access the document's metadata. |
| [***Office Samples***](Samples/Office) | | These samples demonstrate general Office functionality in ImageGear .NET. |
| | [OfficeFileToPDFFile](Samples/Office/OfficeFileToPDFFile) | This sample demonstrates how to load an Office document into ImageGear and save the document as a PDF. |
| | [OfficeFileToRasterFormat](Samples/Office/OfficeFileToRasterFormat) | After an Office document is loaded, ImageGear can rasterize individual pages or the entire document and save them to raster formats. |
| | [ReadOfficeMetadata](Samples/Office/ReadOfficeMetadata) | ImageGear provides functionality to access metadata from the Office document. |
| [***PDF AcroForm Samples***](Samples/PDF%20AcroForm) | | These samples demonstrate PDF AcroForm support in ImageGear .NET. |
| | [AddingWidgets](Samples/PDF%20AcroForm/AddingWidgets) | ImageGear allows adding widgets (radio button, checkboxes, listboxes, etc.) to PDF AcroForms. |
| | [CreatePDFFormField](Samples/PDF%20AcroForm/CreatePDFFormField) | Starting with a PDF document, new or existing, first the PDF document is set to contain forms. Then form controls can be added to any page in the PDF document. |
| | [FlattenPDFFormFields](Samples/PDF%20AcroForm/FlattenPDFFormFields) | This sample demonstrates how to flatten form fields. |
| | [ModifyFieldAppearance](Samples/PDF%20AcroForm/ModifyFieldAppearance) | This sample demonstrates how to add borders or change different attributes to widgets on PDF Forms. |
| | [ReadPDFFormFields](Samples/PDF%20AcroForm/ReadPDFFormFields) | This sample demonstrates how to read the values from form fields. |
| | [SetPDFFormFields](Samples/PDF%20AcroForm/SetPDFFormFields) | This sample demonstrates how to set the values of form fields and save those values to a PDF file. |
| [***PDF Compression Samples***](Samples/PDF%20Compression) | | These samples demonstrate how to compress PDF file content using ImageGear .NET. |
| | [CompressPDFFile](Samples/PDF%20Compression/CompressPDFFile) | To reduce the size of a PDF file, ImageGear provides the SaveCompressed function. |
| [***Combined PDF and JPEG Samples***](Samples/PDF%20and%20JPEG) | | These samples demonstrate general raster image functionality in ImageGear .NET. |
| | [AddImageToPDFAsAPage](Samples/PDF%20and%20JPEG/AddImageToPDFAsAPage) | This sample demonstrates how to add JPEG image as new page to the PDF Document. |
| | [PlaceImageOnAnExistingPDFPage](Samples/PDF%20and%20JPEG/PlaceImageOnAnExistingPDFPage) | This sample demonstrates how to add JPEG image to the existing page of the PDF Document. |
| [***PDF XFA Samples***](Samples/PDF) | | These samples demonstrate PDF with XFA support in ImageGear .NET. |
| | [OpenPDFWithXFA](Samples/PDF%20XFA/OpenPDFWithXFA) | ImageGear can open PDF with XFA, detect XFA type and remove XFA from the document. |
| [***General PDF Samples***](Samples/PDF) | | These samples demonstrate general PDF functionality in ImageGear .NET. |
| | [AddWatermarkToPDFPage](Samples/PDF/AddWatermarkToPDFPage) | This sample demonstrates how to watermark a page in a PDF document using PDE element operations with a raster image loaded into ImageGear. |
| | [ConvertPDFToPDFA_2b](Samples/PDF/ConvertPDFToPDFA_2b) | This sample demonstrates how to take a PDF document loaded into ImageGear, and convert that PDF document to be compliant with the PDF/A-2b specification. |
| | [DeletePDFPage](Samples/PDF/DeletePDFPage) | Pages can be removed from the front (0), end (Pages.Count - 1), or specific page number from a PDF file. |
| | [IntrinsicallyRotatePDFPage](Samples/PDF/IntrinsicallyRotatePDFPage) | In order to rotate a PDF page, each object on the page must be rotated about the origin and then translated back onto the page. |
| | [LoadEncryptedPDF](Samples/PDF/LoadEncryptedPDF) | This sample demonstrates how to decrypt and load a secure PDF. Additionally, it demonstrates how to remove encryption from a PDF once it has been loaded. |
| | [LoadingAndSavingPDF](Samples/PDF/LoadingAndSavingPDF) | This sample demonstrates two of the most basic PDF functions of ImageGear, loading and saving a PDF file using the LoadDocument and SaveDocument functions. |
| | [LoadPDFSaveWithFlags](Samples/PDF/LoadPDFSaveWithFlags) | This sample demonstrates reducing the size of a PDF using flags to reduce duplication of streams, cleaning up unreferenced objects, and optimizing fonts. |
| | [LoadSinglePDFPage](Samples/PDF/LoadSinglePDFPage) | Load a single PDF page and report its physical dimensions with the MediaRect property. |
| | [ManagePDFMetadata](Samples/PDF/ManagePDFMetadata) | Read and update PDF metadata using the GetInfo and SetInfo methods, respectively. |
| | [MergeTwoPDFFiles](Samples/PDF/MergeTwoPDFFiles) | Any PDF document can have pages from other PDF documents inserted into it. The InsertPages function can insert a range of pages. |
| | [MultipageTIFFtoPDFFile](Samples/PDF/MultipageTIFFtoPDFFile) | Since the pages are inserted one at a time into the PDF, the pages can be easily reordered and combinded with other images from other documents. |
| | [PDFContentAddText](Samples/PDF/PDFContentAddText) | Text is added to a PDF page as a Text Element object using the Add function. |
| | [PDFContentExtractText](Samples/PDF/PDFContentExtractText) | Text is extracted from a range of PDF pages using the ExtractText function. |
| | [PDFFileToMultipageTIFF](Samples/PDF/PDFFileToMultipageTIFF) | Converting a PDF file to a raster image format is very easy with ImageGear. |
| | [PDFFileToMultipageTIFFAtHighResolution](Samples/PDF/PDFFileToMultipageTIFFAtHighResolution) | Convert a PDF file to TIFF at high resolution to improve image quality. |
| | [SaveAllImagesOnPDFPage](Samples/PDF/SaveAllImagesOnPDFPage) | After a PDF page is loaded, the objects on the page can be traversed. |
| | [SetPDFSecurity](Samples/PDF/SetPDFSecurity) | ImageGear can set the security on a PDF document using the SetNewCryptHandler and SetNewSecurityData functions. |
| | [SmartZoneOCRToPDF](Samples/PDF/SmartZoneOCRToPDF) | ImageGear can take the recognition results from SmartZone and generate a searchable and copyable PDF page. |
| | [SplitPDFIntoMultiplePDFs](Samples/PDF/SplitPDFIntoMultiplePDFs) | This sample demonstrates how to create new separate PDF documents from a PDF that has been loaded by ImageGear. |
| [***REST API Samples***](Samples/REST%20API) | | These samples demonstrate making use of ImageGear document and image processing operations remotely using ImageGear's REST APIs |
| | [PDFtoPDFARestAPI](Samples/REST%20API/PDFtoPDFARestAPI) | Convert a PDF file to PDF/A remotely using REST API. |
| | [PDFXFARestAPI](Samples/REST%20API/PDFXFARestAPI) | Remove XFA (XML Forms) from PDF file remotely using REST API. |
| | [ImageEditorRestAPI](Samples/REST%20API/ImageEditorRestAPI) | Rotate, resize and crop raster images remotely using REST API. |
