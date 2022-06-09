# ImageGear .NET Samples

These samples are intended to demonstrate how the various functions of ImageGear .NET can be used to perform imaging tasks. They are designed to be lightweight, concise, and show how ImageGear can be integrated into any application to perform these imaging tasks.

## How to Build these Samples

All samples can be built using Microsoft Visual Studio 2017 or later. To build all the samples, use Visual Studio to open the samples.sln file in this directory. To build any individual sample, use Visual Studio to open the .sln file in the individual project directory. After opening the solution, select a Solution Configuration (Debug or Release) and a Solution Platform (x64 or x86). Last, build the complete solution with Build Solution located in the Build menu.

## How to Run these Samples

When the solution is built, it produces one or more console application executables in 'bin' subdirectories in the individual project directories. Run the sample application by double-clicking the application icon, or running it directly from Command Prompt (cmd.exe), PowerShell, or similar. Note that the working directory must be the same as the directory containing the sample executable in order to find the sample input image and the output directory. The input image(s) and output directory are specified relative to the location of the application in all of these samples.

## System Requirements

For a list of the system and development software necessary to build and run these samples, please refer to the [ImageGear .NET Online Documentation](https://help.accusoft.com/ImageGear-Net/latest/Windows/HTML/webframe.html#System_Requirements.html).

## Imaging Support in ImageGear .NET

ImageGear .NET supports the most commonly used image, graphic, and document formats. For more information on imaging support in ImageGear .NET, [please visit us at Accusoft.](https://www.accusoft.com/products/imagegear-collection/imagegear-dot-net/)

## Sample Summary

| Category | Sample | Description |
| :-------------------------- | :--------------------------: | :---------------------------------------------------------------------------------------- |
| [***General Imaging Samples***](Samples/Image) | | These samples demonstrate general raster image functionality in ImageGear .NET. |
| | [LoadingAndSaving](Samples/Image/LoadingAndSaving) | This sample demonstrates two of the most basic raster functions of ImageGear, loading and saving an image using the LoadPage and SavePage functions. |
| | [MetadataGeneralAPI](Samples/Image/MetadataGeneralAPI) | After an image is loaded, ImageGear provides functionality to access both document and page level metadata. |
|  [***Image Conversion Samples***](Samples/Image%20Conversion) | | These samples demonstrate how to convert between raster image formats using ImageGear .NET. |
|  | [ConvertRasterImageFormat](Samples/Image%20Conversion/ConvertRasterImageFormat) | After an ImGearPage has been retrieved, it can then be saved as any supported raster image format using ImGearFileFormats.SavePage(). |
| [***Image Processing Samples***](Samples/Image%20Processing) | | These samples demonstrate select image processing capabilities of ImageGear .NET. |
| | [CropAndResizeRasterImage](Samples/Image%20Processing/CropAndResizeRasterImage) | Once an image is loaded into ImageGear, it can be cropped. ImageGear crops independently on each side of the image, giving you complete control over what portion of the image to remove. |
| | [DespeckleAndDeskewRasterImage](Samples/Image%20Processing/DespeckleAndDeskewRasterImage) | After an image is scanned, it is often slightly skewed, and can also contain "noise" in the background portion of the image. These can cause OCR to perform poorly. To correct these issues, load a raster image into ImageGear and use the Despeckle and Deskew functions to "fix" the image. |
| [***OCR Samples***](Samples/OCR) | | These samples demonstrate the OCR functionality in ImageGear .NET. |
| | [OCRUsingDictionary](Samples/OCR/OCRUsingDictionary) | A dictionary can be initialized from a text file as shown by this sample. |
| | [OCRUsingLanguage](Samples/OCR/OCRUsingLanguage) | In order to best OCR the contents of a page, ImageGear can set the OCR language to improve recognition accuracy. |
| | [OCRUsingZones](Samples/OCR/OCRUsingZones) | Before recognizing text, zones may be specified. If zones are specified, then the text recognized in each zone is provided separately. |
| | [OCRToPDF](Samples/OCR/OCRToPDF) | After ImageGear has recognized an imported raster image, it can then be used to create a PDF page as well as getting the recognized text. |
| | [OCRToText](Samples/OCR/OCRToText) |By extracting the text, the contents of the image can be used to control workflow without modifing the source image. |
| [***PDF Acroform Samples***](Samples/PDF%20Acroform) | | These samples demonstrate PDF Acroform support in ImageGear .NET. |
| | [CreatePDFFormField](Samples/PDF%20Acroform/CreatePDFFormField) | Starting with a PDF document, new or existing, first the PDF document is set to contain forms. Then form controls can be added to any page in the PDF document. |
| [***PDF Compression Samples***](Samples/PDF%20Compression) | | These samples demonstrate how to compress PDF file content using ImageGear .NET. |
| | [CompressPDFFile](Samples/PDF%20Compression/CompressPDFFile) | To reduce the size of a PDF file, ImageGear provides the SaveCompressed function. |
| [***Combined PDF and JPEG Samples***](Samples/PDF%20and%20JPEG) | | These samples demonstrate general raster image functionality in ImageGear .NET. |
| | [AddImageToPDFAsAPage](Samples/PDF%20and%20JPEG/AddImageToPDFAsAPage) | This sample demonstrates how to add JPEG image as new page to the PDF Document. |
| | [PlaceImageOnAnExistingPDFPage](Samples/PDF%20and%20JPEG/PlaceImageOnAnExistingPDFPage) | This sample demonstrates how to add JPEG image to the existing page of the PDF Document. |
| [***General PDF Samples***](Samples/PDF) | | These samples demonstrate general PDF functionality in ImageGear .NET |
| | [DeletePDFPage](Samples/PDF/DeletePDFPage) | Pages can be removed from the front (0), end (Pages.Count - 1), or specific page number from a PDF file. |
| | [IntrinsicallyRotatePDFPage](Samples/PDF/IntrinsicallyRotatePDFPage) | In order to rotate a PDF page, each object on the page must be rotated about the origin and then translated back onto the page. |
| | [LoadingAndSavingPDF](Samples/PDF/LoadingAndSavingPDF) | This sample demonstrates two of the most basic PDF functions of ImageGear, loading and saving a PDF file using the LoadDocument and SaveDocument functions. |
| | [MergeTwoPDFFiles](Samples/PDF/MergeTwoPDFFiles) | Any PDF document can have pages from other PDF documents inserted into it. The InsertPages function can insert a range of pages. |
| | [MultipageTIFFtoPDFFile](Samples/PDF/MultipageTIFFtoPDFFile) | Since the pages are inserted one at a time into the PDF, the pages can be easily reordered and combinded with other images from other documents. |
| | [PDFContentAddText](Samples/PDF/PDFContentAddText) | Text is added to a PDF page as a Text Element object using the Add function. |
| | [PDFContentExtractText](Samples/PDF/PDFContentExtractText) | Text is extracted from a range of PDF pages using the ExtractText function. |
| | [PDFFileToMultipageTIFF](Samples/PDF/PDFFileToMultipageTIFF) | Converting a PDF file to a raster image format is very easy with ImageGear. |
| | [SaveAllImagesOnPDFPage](Samples/PDF/SaveAllImagesOnPDFPage) | After a PDF page is loaded, the objects on the page can be traversed. |
| | [SetPDFSecurity](Samples/PDF/SetPDFSecurity) | ImageGear can set the security on a PDF document using the SetNewCryptHandler and SetNewSecurityData functions. |
| | [VerifyPDFSignature](Samples/PDF/VerifyPDFSignature) | After providing certificates to ImageGear, all the signatures in a PDF document can be verified at once with the VerifySignatures function. |
| | [AddPDFSignature](Samples/PDF/AddPDFSignature) | After a PDF document is set to contain forms using the CreateForm function, a signature field can be created that will provide a digital signature. |
