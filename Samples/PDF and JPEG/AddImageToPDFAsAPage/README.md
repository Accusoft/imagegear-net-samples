# Add an Image To a PDF As A Page (AddImageToPDFAsAPage)

When a JPEG raster image needs to be added to a PDF document as a page, ImageGear can add any raster image to a PDF document.

A single function, AddImage, is used to add a raster image to a PDF document as a page. The location on the PDF page can also be specified, in which case the image will be fit to that location ("fit" means that the aspect ratio of the raster image will not change). In this sample, the image is fit to the entire PDF page.

For more information about the ImageGear .NET API, please refer to the [ImageGear .NET Online Documentation](https://help.accusoft.com/ImageGear-Net/latest/Windows/HTML/webframe.html).

## System Requirements

For a list of the system and development software necessary to build and run these samples, please refer to the [ImageGear .NET Online Documentation](https://help.accusoft.com/ImageGear-Net/latest/Windows/HTML/webframe.html#System_Requirements.html).

## Building the Sample

All samples can be built using Microsoft Visual Studio 2017 or later. To build this sample, open the .sln file in the project directory using Visual Studio, select a Solution Configuration (Debug or Release) and a Solution Platform (x64 or x86), and build with Build Solution located in the Build menu.  

## Running the Sample

When the sample is built, it produces a console application executable in the bin subdirectory. Run this application by double-clicking the application icon, or running it directly from Command Prompt (cmd.exe), PowerShell, or similar. Note that the working directory must be the same as the directory containing the sample executable in order to find the sample input image and the output directory. The input image(s) and output directory are specified relative to the location of the application in all of these samples.

_**NOTE:** ImageGear .NET runs in evaluation mode if started without a license and will periodically display a dialog that requires interaction before proceeding. If you would like to work with a full-featured evaluation of the product, [please contact Accusoft at info@accusoft.com](mailto:info@accusoft.com)._

## PDF and JPEG Support in ImageGear .NET

ImageGear .NET seamlessly integrates PDF and JPEG functionality in a single toolkit. For more information on PDF and JPEG support in ImageGear .NET, [please visit us at Accusoft.](https://www.accusoft.com/products/imagegear-collection/imagegear-dot-net/)