# Convert Multi-page Raster Image to SVG (ConvertMultipageRasterImageToSVG)

Convert a multi-page raster image to an SVG file suitable for display on most web browsers.

Scalable Vector Graphics (SVG) is an XML-based format that describes two-dimensional vector (and raster) graphics. It is a popular format for expressing graphics on websites.

In order to convert a multi-page raster image (e.g. TIFF) to SVG, first the image must be loaded from the source image file using ImGearFileFormats.LoadDocument(). After an ImGearDocument has been retrieved, it can then be saved to SVG using ImGearFileFormats.SaveDocument().

For more information about the ImageGear .NET API, please refer to the [ImageGear .NET Online Documentation](https://help.accusoft.com/ImageGear-Net/latest/Windows/HTML/webframe.html).

## System Requirements

For a list of the system and development software necessary to build and run these samples, please refer to the [ImageGear .NET Online Documentation](https://help.accusoft.com/ImageGear-Net/latest/Windows/HTML/webframe.html#System_Requirements.html).

## Building the Sample

Starting with ImageGear v26.0, ImageGear supports .NET Core.

All samples can be built using Microsoft Visual Studio 2022. To build this sample, open the .sln file in the project directory using Visual Studio, select a Solution Configuration (Debug or Release) and an available Solution Platform (Any CPU), and build with Build Solution located in the Build menu.

To build this sample under Linux, first install the Microsoft .NET SDK for your Linux distribution. After that, run `dotnet build ConvertMultipageRasterImageToSVG.sln`. By default this will build the Debug Solution Configuration.

## Running the Sample

When the sample is built, it produces a console application executable in the bin subdirectory. Run this application by double-clicking the application icon, or running it directly from Command Prompt (cmd.exe), PowerShell, or similar. Note that the working directory must be the same as the directory containing the sample executable in order to find the sample input image and the output directory. The input image(s) and output directory are specified relative to the location of the application in all of these samples.

To run this sample under Linux, run the sample from "bin/Debug/net6.0/" using `./ConvertMultipageRasterImageToSVG`.

_**NOTE:** ImageGear .NET runs in evaluation mode if started without a license and will periodically display a dialog that requires interaction before proceeding. If you would like to work with a full-featured evaluation of the product, [please contact Accusoft at info@accusoft.com](mailto:info@accusoft.com)._

## Image Conversion Support in ImageGear .NET

ImageGear .NET supports conversion between the most commonly used image, graphic, and document formats. For more information on image conversion support in ImageGear .NET., [please visit us at Accusoft.](https://www.accusoft.com/products/imagegear-collection/imagegear-dot-net/)