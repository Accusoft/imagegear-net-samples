# General PDF Samples

These samples demonstrate general PDF functionality in ImageGear .NET

## DeletePDFPage

Pages can be removed from the front (0), end (Pages.Count - 1), or specific page number from a PDF file.

## IntrinsicallyRotatePDFPage

In order to rotate a PDF page, each object on the page must be rotated about the origin and then translated back onto the page.

## LoadingAndSavingPDF

This sample demonstrates two of the most basic PDF functions of ImageGear, loading and saving a PDF file using the LoadDocument and SaveDocument functions.

## MergeTwoPDFFiles

Any PDF document can have pages from other PDF documents inserted into it. The InsertPages function can insert a range of pages.

## MultipageTIFFtoPDFFile

Since the pages are inserted one at a time into the PDF, the pages can be easily reordered and combinded with other images from other documents.

## PDFContentAddText

Text is added to a PDF page as a Text Element object using the Add function.

## PDFContentExtractText

Text is extracted from a range of PDF pages using the ExtractText function.

## PDFFileToMultipageTIFF

Converting a PDF file to a raster image format is very easy with ImageGear.

## SaveAllImagesOnPDFPage

After a PDF page is loaded, the objects on the page can be traversed.

## SetPDFSecurity

ImageGear can set the security on a PDF document using the SetNewCryptHandler and SetNewSecurityData functions.

## VerifyPDFSignature

After providing certificates to ImageGear, all the signatures in a PDF document can be verified at once with the VerifySignatures function.

## AddPDFSignature

After a PDF document is set to contain forms using the CreateForm function, a signature field can be created that will provide a digital signature.
