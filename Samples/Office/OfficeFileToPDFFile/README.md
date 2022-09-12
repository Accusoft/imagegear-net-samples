# Convert Office File To PDF File (OfficeFileToPDFFile)

When a PDF is needed instead of an Office file ImageGear can convert the pages of an Office file into a PDF document.

This sample demonstrates converting an Office file to a PDF with ImageGear. To convert an Office file, load the Office document and save as a PDF document.

For more information about the ImageGear .NET API, please refer to the [ImageGear .NET Online Documentation](https://help.accusoft.com/ImageGear/latest/webframe.html).

## System Requirements

For a list of the system and development software necessary to build and run these samples, please refer to the [ImageGear .NET Online Documentation](https://help.accusoft.com/ImageGear/latest/webframe.html#system-requirements.html).

## Building the Sample

Starting with ImageGear v26.0, ImageGear supports .NET Core.

All samples can be built using Microsoft Visual Studio 2022. To build this sample, open the .sln file in the project directory using Visual Studio, select a Solution Configuration (Debug or Release) and an available Solution Platform (x64 or x86), and build with Build Solution located in the Build menu.

Samples using the Accusoft.ImageGear.Office NuGet package must specify a full path to a local LibreOffice installation as a parameter to the ImGearOffice.Initialize() function. LibreOffice must be version 7.0 or later.

To build this sample under Linux, first install the Microsoft .NET SDK for your Linux distribution. After that, perform these steps:

- Build the sample using `dotnet build OfficeFileToPDFFile.sln`. By default this will build the Debug Solution Configuration.
- For samples using the Accusoft.ImageGear.Pdf NuGet package, make sure the native portion of the ImageGear components is on the library search path. This additional search path can be temporarily defined by using the LD_LIBRARY_PATH environment variable. The native libraries can be found in the "bin/x64/Debug/net6.0/runtimes/linux-x64/native" under the build configuration directory.

## Running the Sample

When the sample is built, it produces a console application executable in the bin subdirectory. Run this application by double-clicking the application icon, or running it directly from Command Prompt (cmd.exe), PowerShell, or similar. Note that the working directory must be the same as the directory containing the sample executable in order to find the sample input image and the output directory. The input image(s) and output directory are specified relative to the location of the application in all of these samples.

To run this sample under Linux, run the sample from "bin/x64/Debug/net6.0/" using `./OfficeFileToPDFFile`.

_**NOTE:** ImageGear .NET runs in evaluation mode if started without a license and may periodically display a dialog that requires interaction before proceeding. If you would like to work with a full-featured evaluation of the product, [please contact Accusoft at info@accusoft.com](mailto:info@accusoft.com)._

## Office Document Support in ImageGear .NET

ImageGear .NET supports reading all common Office document formats. For more information on Office support in ImageGear .NET, [please visit us at Accusoft.](https://www.accusoft.com/products/imagegear-collection/imagegear-dot-net/)