// Credit: https://psycodedeveloper.wordpress.com/2013/01/10/how-to-extract-images-from-pdf-files-using-c-and-itextsharp/
namespace OpenLawOffice.Assets.Tracking.PDF
{
    using iTextSharp.text.pdf;
    using iTextSharp.text.pdf.parser;
    using System.Collections.Generic;

    public static class PdfImageExtractor
    {
        #region Methods
        #region Public Methods
        ///// <summary>Checks whether a specified page of a PDF file contains images.</summary>
        ///// <returns>True if the page contains at least one image; false otherwise.</returns>
        //public static bool PageContainsImages(string filename, int pageNumber)
        //{
        //    using (var reader = new PdfReader(filename))
        //    {
        //        var parser = new PdfReaderContentParser(reader);
        //        ImageRenderListener listener = null;
        //        parser.ProcessContent(pageNumber, (listener = new ImageRenderListener()));
        //        return listener.Images.Count > 0;
        //    }
        //}

        public static List<PdfImage> ExtractImagesFromPage1(string filename)
        {
            List<PdfImage> images = new List<PdfImage>();
            using (var reader = new PdfReader(filename))
            {
                var parser = new PdfReaderContentParser(reader);
                ImageRenderListener listener = null;
                
                parser.ProcessContent(1, (listener = new ImageRenderListener()));
                return listener.Images;
            }
        }
        ///// <summary>Extracts all images (of types that iTextSharp knows how to decode) from a PDF file.</summary>
        //public static List<System.Drawing.Image> ExtractImages(string filename)
        //{
        //    List<System.Drawing.Image> images = new List<System.Drawing.Image>();
        //    using (var reader = new PdfReader(filename))
        //    {
        //        var parser = new PdfReaderContentParser(reader);
        //        ImageRenderListener listener = null;
        //        for (var i = 1; i <= reader.NumberOfPages; i++)
        //        {
        //            parser.ProcessContent(i, (listener = new ImageRenderListener()));
        //            var index = 1;
        //            if (listener.Images.Count > 0)
        //            {
        //                foreach (var pair in listener.Images)
        //                {
        //                    images.Add(pair.Key);
        //                    index++;
        //                }
        //            }
        //        }
        //        return images;
        //    }
        //}
        ///// <summary>Extracts all images (of types that iTextSharp knows how to decode)
        ///// from a specified page of a PDF file.</summary>
        ///// <returns>Returns a generic <see cref="Dictionary&lt;string, System.Drawing.Image&gt;"/>,
        ///// where the key is a suggested file name, in the format: PDF filename without extension,
        ///// page number and image index in the page.</returns>
        //public static List<System.Drawing.Image> ExtractImages(string filename, int pageNumber)
        //{
        //    List<System.Drawing.Image> images = new List<System.Drawing.Image>();
        //    PdfReader reader = new PdfReader(filename);
        //    PdfReaderContentParser parser = new PdfReaderContentParser(reader);
        //    ImageRenderListener listener = null;
        //    parser.ProcessContent(pageNumber, (listener = new ImageRenderListener()));
        //    int index = 1;
        //    if (listener.Images.Count > 0)
        //    {
        //        foreach (KeyValuePair<System.Drawing.Image, string> pair in listener.Images)
        //        {
        //            images.Add(pair.Key);
        //            index++;
        //        }
        //    }
        //    return images;
        //}

        #endregion Public Methods
        #endregion Methods
    }
}
