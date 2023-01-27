# Image Processing Samples

These samples demonstrate select image processing capabilities of ImageGear .NET.

## AdjustContrastForRasterImage

It is often necessary to manually adjust the contrast, brightness, and gamma of a raster image. Use the AdjustContrast method to do this.

## CreateThumbnailImage

After an image is loaded, use ImGearProcessing.CreateThumbnail() to create a thumbnail with specific dimensions. Interpolation options depend on the color space and bit depth of the image.

## CropAndResizeRasterImage

Once an image is loaded into ImageGear, it can be cropped. ImageGear crops independently on each side of the image, giving you complete control over what portion of the image to remove.

## DespeckleAndDeskewRasterImage

After an image is scanned, it is often slightly skewed, and can also contain "noise" in the background portion of the image. These can cause OCR to perform poorly. To correct these issues, load a raster image into ImageGear and use the Despeckle and Deskew functions to "fix" the image.

## EqualizeContrastForRasterImage

ImageGear can improve the contrast of a raster image using the EqualizeContrast method.

## ErodeAndDilateRasterImage

After a document is scanned, there can be some dot noise on the image. Sequential applying of morphological operations such as 'Erode' and 'Dilate' can significantly reduce or even completely remove this noise.

## FlipAndRotateRasterImage

After an image is loaded, use the ImGearProcessing.Flip() method to flip the image across either its horizontal or vertical axis. Use the ImGearProcessing.Rotate() method to rotate the image degrees clockwise using one of several interpolation techniques.

## ImageCleanup

After a document is scanned, there can be undesirable artifacts on the image. Use the various image cleanup operations, such as `CheckForNoise`, `RemoveDotLines`, `CleanBorders`, `TextAutoOrientation`, `RemoveLines` and `AutoCrop` to clean up the image to make it suitable for further processing. The sample also demonstrates how to invert black images and white text on a black background.

## ProcessUsingRegionOfInterest

Apply an image processing operation to a rectangluar Region of Interest (ROI) within a raster image instead of its entire area.

## ProcessUsingRegionOfInterestMask

Apply an image processing operation to a bitonal Region of Interest (ROI) mask within a raster image instead of its entire area.
