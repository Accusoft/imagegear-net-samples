# Convert Raster Image Format (ConvertRasterImageFormat)

When a raster image is not in a desired format, ImageGear can convert the format of the image by saving the image in a new format.

In order to convert the format of a raster image, first the image must be loaded from the source image file using either ImGearFileFormats.LoadPage() or ImGearFileFormats.LoadDocument() and then the Pages property from the returned ImGearDocument object. After an ImGearPage has been retrieved, it can then be saved as any supported raster image format using ImGearFileFormats.SavePage().

For more information about the ImageGear .NET API, please refer to the [ImageGear .NET Online Documentation](https://help.accusoft.com/ImageGear-Net/latest/Windows/HTML/webframe.html).

## System Requirements

For a list of the system and development software necessary to build and run these samples, please refer to the [ImageGear .NET Online Documentation](https://help.accusoft.com/ImageGear-Net/latest/Windows/HTML/webframe.html#System_Requirements.html).

## Building the Sample

All samples can be built using Microsoft Visual Studio 2017 or later. To build this sample, open the .sln file in the project directory using Visual Studio, select a Solution Configuration (Debug or Release) and a Solution Platform (x64 or x86), and build with Build Solution located in the Build menu.  

## Running the Sample

When the sample is built, it produces a console application executable in the bin subdirectory. Run this application by double-clicking the application icon, or running it directly from Command Prompt (cmd.exe), PowerShell, or similar. Note that the working directory must be the same as the directory containing the sample executable in order to find the sample input image and the output directory. The input image(s) and output directory are specified relative to the location of the application in all of these samples.

_**NOTE:** ImageGear .NET runs in evaluation mode if started without a license and will periodically display a dialog that requires interaction before proceeding. If you would like to work with a full-featured evaluation of the product, [please contact Accusoft at info@accusoft.com](mailto:info@accusoft.com)._

## Image Conversion Support in ImageGear .NET

ImageGear .NET supports conversion between the most commonly used image, graphic, and document formats. For more information on image conversion support in ImageGear .NET., [please visit us at Accusoft.](https://www.accusoft.com/products/imagegear-collection/imagegear-dot-net/)