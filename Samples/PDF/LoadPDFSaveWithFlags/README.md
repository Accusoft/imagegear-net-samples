# Loading And Saving a PDF file with Flags (LoadPDFSaveWithFlags)

When using ImageGear to save PDF files, it may be useful to reduce the size to allow for more efficient transmission or storage.

This sample demonstrates saving a PDF file using various flags. There are instances with fonts used in the creation of the PDF that their encodings and descriptors are identical, taking up unnecessary space. Also, streams should be optimized, merging substreams and reducing the size of the outputted PDF. Unreferenced items can increase the space making the PDF larger than expected. Objects using the creation or editing of the PDF will use more space. Removing, or reducing the impact of these obstacles will also reduce the overall size, making transportation and storage easier.

For more information about the ImageGear .NET API, please refer to the [ImageGear .NET Online Documentation](https://help.accusoft.com/ImageGearMP/latest/webframe.html).

## System Requirements

For a list of the system and development software necessary to build and run these samples, please refer to the [ImageGear .NET Online Documentation](https://help.accusoft.com/ImageGearMP/latest/webframe.html#system-requirements.html).

## Building the Sample

Starting with ImageGear v26.0, ImageGear supports .NET Core.

All samples can be built using Microsoft Visual Studio 2022.

To build this sample:

1. Open the .sln file in the project directory using Visual Studio 2022.
2. Select a Solution Configuration (Debug or Release) and an available Solution Platform (x64 or Any CPU).
3. Build with Build Solution located in the Build menu.

To build this sample under Linux:

1. Install the Microsoft .NET SDK for your Linux distribution.
2. Run `dotnet build LoadPDFSaveWithFlags.sln`. By default this will build the Debug Solution Configuration.

## Running the Sample

When the sample is built, it produces a console application executable in the bin subdirectory. Run this application by double-clicking the application icon, or run it directly from Command Prompt (cmd.exe), PowerShell, or similar. Note that the working directory must be the same as the directory containing the sample executable in order to find the sample input image and the output directory. The input image(s) and output directory are specified relative to the location of the application in all of these samples.

To run this sample under Linux, run the sample from "bin/Debug/net6.0/" or "bin/x64/Debug/net6.0/" (depending on the solution platform) using `./LoadPDFSaveWithFlags`.

_**NOTE:** ImageGear .NET runs in evaluation mode if started without a license. In evaluation mode, documents and images will be watermarked when exported or displayed. If you would like to work with a full-featured evaluation of the product, [please contact Accusoft at info@accusoft.com](mailto:info@accusoft.com)._

## PDF Support in ImageGear .NET

ImageGear .NET is a robust, multi-platform, multi-language PDF solution. For more information on PDF support in ImageGear .NET, [please visit us at Accusoft.](https://www.accusoft.com/products/imagegear-collection/imagegear-dot-net/)
