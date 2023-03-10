# Adding Widgets (AddingWidgets)

When a PDF form needs to have widgets such as check boxes, combo boxes, list boxes, and radio buttons, ImageGear allows you to create the objects on the forms.

Widget Annotations are the visual elements of a form field. A field can have zero or more widgets attached to it: a TextField, ListBox, ComboBox, and CheckBox usually have exactly one, while the RadioGroup has multiple widgets, one for each radio button.
In some cases, you may want a field to appear on more than one page, repeating the same value. In that case, fields that normally have just one widget may have multiple widgets attached. Someone filling out the form may use any of those widgets to update the field's value, and this is reflected in all the other widgets as well.

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
2. Run `dotnet build AddingWidgets.sln`. By default this will build the Debug Solution Configuration.

## Running the Sample

When the sample is built, it produces a console application executable in the bin subdirectory. Run this application by double-clicking the application icon, or run it directly from Command Prompt (cmd.exe), PowerShell, or similar. Note that the working directory must be the same as the directory containing the sample executable in order to find the sample input image and the output directory. The input image(s) and output directory are specified relative to the location of the application in all of these samples.

To run this sample under Linux, run the sample from "bin/Debug/net6.0/" or "bin/x64/Debug/net6.0/" (depending on the solution platform) using `./AddingWidgets`.

_**NOTE:** ImageGear .NET runs in evaluation mode if started without a license. In evaluation mode, documents and images will be watermarked when exported or displayed. If you would like to work with a full-featured evaluation of the product, [please contact Accusoft at info@accusoft.com](mailto:info@accusoft.com)._

## PDF AcroForm Support in ImageGear .NET

ImageGear .NET supports adding, updating, and deleting AcroForm fields in new or existing PDFs. For more information on PDF AcroForm support in ImageGear .NET, [please visit us at Accusoft.](https://www.accusoft.com/products/imagegear-collection/imagegear-dot-net/)
