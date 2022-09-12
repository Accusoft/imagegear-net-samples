# Convert a Multipage TIFF to a PDF File (MultipageTIFFtoPDFFile)

When a PDF is needed in place of a multipage TIFF file, ImageGear can easily convert the pages of the TIFF file into a PDF document.

Since the pages are inserted one at a time into the PDF, the pages can be easily reordered and combinded with other images from other documents. This is also an opportunity to easily process the images before they are added to a PDF.

For more information about the ImageGear .NET API, please refer to the [ImageGear .NET Online Documentation](https://help.accusoft.com/ImageGear/latest/webframe.html).

## System Requirements

For a list of the system and development software necessary to build and run these samples, please refer to the [ImageGear .NET Online Documentation](https://help.accusoft.com/ImageGear/latest/webframe.html#system-requirements.html).

## Building the Sample

Starting with ImageGear v26.0, ImageGear supports .NET Core.

All samples can be built using Microsoft Visual Studio 2022. To build this sample, open the .sln file in the project directory using Visual Studio, select a Solution Configuration (Debug or Release) and an available Solution Platform (x64 or x86), and build with Build Solution located in the Build menu.  

To build this sample under Linux, first install the Microsoft .NET SDK for your Linux distribution. After that, perform these steps:

- Build the sample using `dotnet build MultipageTIFFtoPDFFile.dotnet.sln`. By default this will build the Debug Solution Configuration.
- For samples using the Accusoft.ImageGear.Pdf NuGet package, make sure the native portion of the ImageGear components is on the library search path. This additional search path can be temporarily defined by using the LD_LIBRARY_PATH environment variable. The native libraries can be found in the "bin/x64/Debug/net6.0/runtimes/linux-x64/native" under the build configuration directory.

## Running the Sample

When the sample is built, it produces a console application executable in the bin subdirectory. Run this application by double-clicking the application icon, or running it directly from Command Prompt (cmd.exe), PowerShell, or similar. Note that the working directory must be the same as the directory containing the sample executable in order to find the sample input image and the output directory. The input image(s) and output directory are specified relative to the location of the application in all of these samples.

To run this sample under Linux, run the sample from "bin/x64/Debug/net6.0/" using `./MultipageTIFFtoPDFFile.dotnet`.

_**NOTE:** ImageGear .NET runs in evaluation mode if started without a license and will periodically display a dialog that requires interaction before proceeding. If you would like to work with a full-featured evaluation of the product, [please contact Accusoft at info@accusoft.com](mailto:info@accusoft.com)._

## PDF Support in ImageGear .NET

ImageGear .NET is a robust, multi-platform, multi-language PDF solution. For more information on PDF support in ImageGear .NET, [please visit us at Accusoft.](https://www.accusoft.com/products/imagegear-collection/imagegear-dot-net/)
