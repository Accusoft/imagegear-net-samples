# Image Processing within a Region of Interest Mask (ProcessUsingRegionOfInterestMask)

ImageGear can apply image processing operations to a bitonal Region of Interest (ROI) mask within a raster image.

Use a Region of Interest mask when image processing operations should be limited to non-rectangular areas rather than the entire raster image.

After loading a raster image, create an ImGearROIMask from either a bitonal image, a shape (such as an ellipse, polygon, or freehand), or a mask color. Assign this ImGearROIMask object to the raster image ROI property. When image processing is applied to that raster image, only the area within its ROI is affected.

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
2. Run `dotnet build ProcessUsingRegionOfInterestMask.sln`. By default this will build the Debug Solution Configuration.

## Running the Sample

When the sample is built, it produces a console application executable in the bin subdirectory. Run this application by double-clicking the application icon, or run it directly from Command Prompt (cmd.exe), PowerShell, or similar. Note that the working directory must be the same as the directory containing the sample executable in order to find the sample input image and the output directory. The input image(s) and output directory are specified relative to the location of the application in all of these samples.

To run this sample under Linux, run the sample from "bin/Debug/net6.0/" (depending on the solution platform) using `./ProcessUsingRegionOfInterestMask`.

_**NOTE:** ImageGear .NET runs in evaluation mode if started without a license. In evaluation mode, documents and images will be watermarked when exported or displayed. If you would like to work with a full-featured evaluation of the product, [please contact Accusoft at info@accusoft.com](mailto:info@accusoft.com)._

## Image Processing Support in ImageGear .NET

ImageGear .NET can perform a broad range of image cleanup, correction, and transformation functions. For more information on image processing support in ImageGear .NET, [please visit us at Accusoft.](https://www.accusoft.com/products/imagegear-collection/imagegear-dot-net/)
