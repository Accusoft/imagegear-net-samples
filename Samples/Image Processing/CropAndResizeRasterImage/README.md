# Crop And Resize Raster Image (CropAndResizeRasterImage)

When a raster image the wrong size or needs to be enlarged or shrank, ImageGear can crop or resize the image as needed.

Once an image is loaded into ImageGear, it can be cropped. ImageGear crops independently on each side of the image, giving you complete control over what portion of the image to remove. An image can also be resized, which consists of creating more or fewer pixels to represent the same portion of the image. There are a number of options for how pixels are created and removed when resizing. See the ImageGear documentation for a list of interpolation options.

For more information about the ImageGear .NET API, please refer to the [ImageGear .NET Online Documentation](https://help.accusoft.com/ImageGear/latest/webframe.html).

## System Requirements

For a list of the system and development software necessary to build and run these samples, please refer to the [ImageGear .NET Online Documentation](https://help.accusoft.com/ImageGear/latest/webframe.html#system-requirements.html).

## Building the Sample

Starting with ImageGear v26.0, ImageGear supports .NET Core.

All samples can be built using Microsoft Visual Studio 2022.

To build this sample:

1. Open the .sln file in the project directory using Visual Studio 2022.
2. Select a Solution Configuration (Debug or Release) and an available Solution Platform (x64 or Any CPU).
3. Build with Build Solution located in the Build menu.

To build this sample under Linux:

1. Install the Microsoft .NET SDK for your Linux distribution.
2. Run `dotnet build CropAndResizeRasterImage.sln`. By default this will build the Debug Solution Configuration.

## Running the Sample

When the sample is built, it produces a console application executable in the bin subdirectory. Run this application by double-clicking the application icon, or run it directly from Command Prompt (cmd.exe), PowerShell, or similar. Note that the working directory must be the same as the directory containing the sample executable in order to find the sample input image and the output directory. The input image(s) and output directory are specified relative to the location of the application in all of these samples.

To run this sample under Linux, run the sample from "bin/Debug/net6.0/" (depending on the solution platform) using `./CropAndResizeRasterImage`.

_**NOTE:** ImageGear .NET runs in evaluation mode if started without a license. In evaluation mode, documents and images will be watermarked when exported or displayed. If you would like to work with a full-featured evaluation of the product, [please contact Accusoft at info@accusoft.com](mailto:info@accusoft.com).

## Image Processing Support in ImageGear .NET

ImageGear .NET can perform a broad range of image cleanup, correction, and transformation functions. For more information on image processing support in ImageGear .NET, [please visit us at Accusoft.](https://www.accusoft.com/products/imagegear-collection/imagegear-dot-net/)
