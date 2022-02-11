# Place an Image On An Existing PDF Page (PlaceImageOnAnExistingPDFPage)

When a JPEG image needs to be added to an existing PDF page, ImageGear can place any raster image precisely on a PDF page.

A single function, AddImage, is used to add a raster image to a PDF document as a page. The location on the PDF page can also be specified, in which case the image will be fit to that location ("fit" means that the aspect ratio of the raster image will not change). In this sample, the image is placed in a rectangle on the PDF page.

For more information about the ImageGear .NET API, please refer to the [ImageGear .NET Online Documentation](https://help.accusoft.com/ImageGear-Net/latest/Windows/HTML/webframe.html).

## System Requirements

For a list of the system and development software necessary to build and run these samples, please refer to the [ImageGear .NET Online Documentation](https://help.accusoft.com/ImageGear-Net/latest/Windows/HTML/webframe.html#System_Requirements.html).

## Support for .NET core

Starting with ImageGear v26.0, ImageGear supports .NET Core. To build this sample using .NET Core, open the .dotnet.sln file in the project directory using Visual Studio 2017 or later. Follow the instructions below to build and run the sample.

## Building the Sample

All samples can be built using Microsoft Visual Studio 2017 or later. To build this sample, open the .sln file in the project directory using Visual Studio, select a Solution Configuration (Debug or Release) and a Solution Platform (x64 or x86), and build with Build Solution located in the Build menu.  

To build this sample under Linux, first install the Microsoft .NET SDK for your Linux distribution. After that, perform these steps:

- Build the sample using `dotnet build PlaceImageOnAnExistingPDFPage.dotnet.sln`. By default this will build the Debug Solution Configuration.
- For samples using the Accusoft.ImageGear.Pdf NuGet package, make sure the native portion of the ImageGear components is on the library search path. This additional search path can be temporarily defined by using the LD_LIBRARY_PATH environment variable. The native libraries can be found in the "bin/Debug/net5.0/runtimes/linux-x64/native" under the build configuration directory.

## Running the Sample

When the sample is built, it produces a console application executable in the bin subdirectory. Run this application by double-clicking the application icon, or running it directly from Command Prompt (cmd.exe), PowerShell, or similar. Note that the working directory must be the same as the directory containing the sample executable in order to find the sample input image and the output directory. The input image(s) and output directory are specified relative to the location of the application in all of these samples.

To run this sample under Linux, run the sample from "bin/Debug/net5.0/" using `./PlaceImageOnAnExistingPDFPage.dotnet`.

_**NOTE:** ImageGear .NET runs in evaluation mode if started without a license and will periodically display a dialog that requires interaction before proceeding. If you would like to work with a full-featured evaluation of the product, [please contact Accusoft at info@accusoft.com](mailto:info@accusoft.com)._

## PDF and JPEG Support in ImageGear .NET

ImageGear .NET seamlessly integrates PDF and JPEG functionality in a single toolkit. For more information on PDF and JPEG support in ImageGear .NET, [please visit us at Accusoft.](https://www.accusoft.com/products/imagegear-collection/imagegear-dot-net/)
