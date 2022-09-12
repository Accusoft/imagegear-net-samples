# Extract email attachments from Email file (ExtractEmailAttachments)

When attachments from an Email document are needed, ImageGear can extract and save them as files.

This sample loads an Email document with ImageGear and demonstrates how to access attachments contained in the document. After the Email document is loaded, attachments can be directly accessed and saved as files.

For more information about the ImageGear .NET Email conversion API, please refer to the [ImageGear .NET Online Documentation](https://help.accusoft.com/ImageGear/latest/webframe.html).

## System Requirements

Email conversion API is only supported for 64 bit processes.

For a list of the system and development software necessary to build and run these samples, please refer to the [ImageGear .NET Online Documentation](https://help.accusoft.com/ImageGear/latest/webframe.html#system-requirements.html).

## Building the Sample

Starting with ImageGear v26.0, ImageGear supports .NET Core.

All samples can be built using Microsoft Visual Studio 2022 or later. To build this sample, open the .sln file in the project directory using Visual Studio, select a Solution Configuration (Debug or Release) and a Solution Platform (x64), and build with Build Solution located in the Build menu.  

To build this sample under Linux, first install the Microsoft .NET SDK for your Linux distribution. After that, perform these steps:

- Build the sample using `dotnet build ExtractEmailAttachments.sln`. By default this will build the Debug Solution Configuration.

- For samples using the Accusoft.ImageGear.Email NuGet package, make sure the native portion of the ImageGear components is on the library search path. This additional search path can be temporarily defined by using the LD_LIBRARY_PATH environment variable. The native libraries can be found in the "bin/Debug/net6.0/runtimes/linux-x64/native" under the build configuration directory.

## Running the Sample

When the sample is built, it produces a console application executable in the bin subdirectory. Run this application by double-clicking the application icon, or running it directly from Command Prompt (cmd.exe), PowerShell, or similar. Note that the working directory must be the same as the directory containing the sample executable in order to find the sample input image and the output directory. The input image(s) and output directory are specified relative to the location of the application in all of these samples.

To run this sample under Linux, run the sample from "bin/x64/Debug/net6.0/" using `./ExtractEmailAttachments`.

## Email Document Support in ImageGear .NET

ImageGear .NET supports reading MSG and EML Email document formats and converting them to PDF and Image Raster Formats. For more information on Email conversion support in ImageGear .NET, [please visit us at Accusoft.](https://www.accusoft.com/products/imagegear-collection/imagegear-dot-net/)
