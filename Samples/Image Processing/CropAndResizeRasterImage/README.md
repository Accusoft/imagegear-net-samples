# Crop And Resize Raster Image (CropAndResizeRasterImage)

When a raster image the wrong size or needs to be enlarged or shrank, ImageGear can crop or resize the image as needed.

Once an image is loaded into ImageGear, it can be cropped. ImageGear crops independently on each side of the image, giving you complete control over what portion of the image to remove. An image can also be resized, which consists of creating more or fewer pixels to represent the same portion of the image. There are a number of options for how pixels are created and removed when resizing. See the ImageGear documentation for a list of interpolation options.

For more information about the ImageGear .NET API, please refer to the [ImageGear .NET Online Documentation](https://help.accusoft.com/ImageGear-Net/latest/Windows/HTML/webframe.html).

## System Requirements

For a list of the system and development software necessary to build and run these samples, please refer to the [ImageGear .NET Online Documentation](https://help.accusoft.com/ImageGear-Net/latest/Windows/HTML/webframe.html#System_Requirements.html).

## Support for .NET core

Starting with ImageGear v26.0, ImageGear supports .NET Core. To build this sample using .NET Core, open the .dotnet.sln file in the project directory using Visual Studio 2017 or later. Follow the instructions below to build and run the sample.

## Building the Sample

All samples can be built using Microsoft Visual Studio 2017 or later. To build this sample, open the .sln file in the project directory using Visual Studio, select a Solution Configuration (Debug or Release) and a Solution Platform (x64 or x86), and build with Build Solution located in the Build menu.  

To build this sample under Linux, first install the Microsoft .NET SDK for your Linux distribution. After that, perform these steps:

- Build the sample using `dotnet build CropAndResizeRasterImage.dotnet.sln`. By default this will build the Debug Solution Configuration.
- For samples using the Accusoft.ImageGear.Pdf NuGet package, make sure the native portion of the ImageGear components is on the library search path. This additional search path can be temporarily defined by using the LD_LIBRARY_PATH environment variable. The native libraries can be found in the "bin/Debug/net5.0/runtimes/linux-x64/native" under the build configuration directory.

## Running the Sample

When the sample is built, it produces a console application executable in the bin subdirectory. Run this application by double-clicking the application icon, or running it directly from Command Prompt (cmd.exe), PowerShell, or similar. Note that the working directory must be the same as the directory containing the sample executable in order to find the sample input image and the output directory. The input image(s) and output directory are specified relative to the location of the application in all of these samples.

To run this sample under Linux, run the sample from "bin/Debug/net5.0/" using `./CropAndResizeRasterImage.dotnet`.

_**NOTE:** ImageGear .NET runs in evaluation mode if started without a license and will periodically display a dialog that requires interaction before proceeding. If you would like to work with a full-featured evaluation of the product, [please contact Accusoft at info@accusoft.com](mailto:info@accusoft.com)._

## Image Processing Support in ImageGear .NET

ImageGear .NET can perform a broad range of image cleanup, correction, and transformation functions. For more information on image processing support in ImageGear .NET, [please visit us at Accusoft.](https://www.accusoft.com/products/imagegear-collection/imagegear-dot-net/)
