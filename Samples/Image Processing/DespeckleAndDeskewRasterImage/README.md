# Despeckle And Deskew Raster Image (DespeckleAndDeskewRasterImage)

When a scanned raster image is skewed or speckled, ImageGear can straighten and clean the image for further processing or OCR.

After an image is scanned, it is often slightly skewed, and can also contain "noise" in the background portion of the image. These can cause OCR to perform poorly. To correct these issues, load a raster image into ImageGear and use the Despeckle and Deskew functions to "fix" the image. Besides skew and specks, there may be other issues with the image that need to be addressed before OCR can be accurately performed. Look at the image processing functions listed in the ImageGear documentation for more options.

For more information about the ImageGear .NET API, please refer to the [ImageGear .NET Online Documentation](https://help.accusoft.com/ImageGear-Net/latest/Windows/HTML/webframe.html).

## System Requirements

For a list of the system and development software necessary to build and run these samples, please refer to the [ImageGear .NET Online Documentation](https://help.accusoft.com/ImageGear-Net/latest/Windows/HTML/webframe.html#System_Requirements.html).

## Building the Sample

All samples can be built using Microsoft Visual Studio 2017 or later. To build this sample, open the .sln file in the project directory using Visual Studio, select a Solution Configuration (Debug or Release) and a Solution Platform (x64 or x86), and build with Build Solution located in the Build menu.  

## Running the Sample

When the sample is built, it produces a console application executable in the bin subdirectory. Run this application by double-clicking the application icon, or running it directly from Command Prompt (cmd.exe), PowerShell, or similar. Note that the working directory must be the same as the directory containing the sample executable in order to find the sample input image and the output directory. The input image(s) and output directory are specified relative to the location of the application in all of these samples.

_**NOTE:** ImageGear .NET runs in evaluation mode if started without a license and will periodically display a dialog that requires interaction before proceeding. If you would like to work with a full-featured evaluation of the product, [please contact Accusoft at info@accusoft.com](mailto:info@accusoft.com)._

## Image Processing Support in ImageGear .NET

ImageGear .NET can perform a broad range of image cleanup, correction, and transformation functions. For more information on image processing support in ImageGear .NET, [please visit us at Accusoft.](https://www.accusoft.com/products/imagegear-collection/imagegear-dot-net/)
