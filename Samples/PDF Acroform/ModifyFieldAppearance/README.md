# Modify Field Appearance (ModifyFieldAppearance)

Sometimes Fields need to visually stand out for emphasis or to show importance. Fields and their widgets have various properties to control appearance when a PDF page is rendered or displayed.

This sample shows how to change appearance for a few preexisting AcroForm Fields and their Widgets. Read or update text style information for Fields that display text (which includes TextField, ListBox, ComboBox, CheckBox, SignatureField, and PushButton fields) using FontName, FontSize, and TextColor properties. Read or update Widget appearance using its BorderStyle, BorderColor, BorderWidth and FillColor properties.

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
2. Run `dotnet build ModifyFieldAppearance.sln`. By default this will build the Debug Solution Configuration.

## Running the Sample

When the sample is built, it produces a console application executable in the bin subdirectory. Run this application by double-clicking the application icon, or run it directly from Command Prompt (cmd.exe), PowerShell, or similar. Note that the working directory must be the same as the directory containing the sample executable in order to find the sample input image and the output directory. The input image(s) and output directory are specified relative to the location of the application in all of these samples.

To run this sample under Linux, run the sample from "bin/Debug/net6.0/" or "bin/x64/Debug/net6.0/" (depending on the solution platform) using `./ModifyFieldAppearance`.

_**NOTE:** ImageGear .NET runs in evaluation mode if started without a license. In evaluation mode, documents and images will be watermarked when exported or displayed. If you would like to work with a full-featured evaluation of the product, [please contact Accusoft at info@accusoft.com](mailto:info@accusoft.com)._

## PDF AcroForm Support in ImageGear .NET

ImageGear .NET supports adding, updating, and deleting AcroForm fields in new or existing PDFs. For more information on PDF AcroForm support in ImageGear .NET, [please visit us at Accusoft.](https://www.accusoft.com/products/imagegear-collection/imagegear-dot-net/)
