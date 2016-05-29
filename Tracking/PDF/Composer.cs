using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLawOffice.Assets.Tracking.PDF
{
    using iTextSharp.text;
    using iTextSharp.text.pdf;
    using System.Drawing;
    using System.IO;

    public class Composer
    {
        private const int MINIMUM_BORDER = 5;

        public static void Compose(
            Bitmap qrCode,
            double xPercent,
            double yPercent,
            SizeF size,
            string inputFilepath,
            string outputFilepath)
        {
            using (Stream inputPdfStream = new FileStream(inputFilepath, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (Stream outputPdfStream = new FileStream(outputFilepath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                var reader = new PdfReader(inputPdfStream);
                var stamper = new PdfStamper(reader, outputPdfStream);
                stamper.FormFlattening = true;
                var pdfContentByte = stamper.GetOverContent(1);
                var sizea = reader.GetPageSize(1);

                float x = (float)(sizea.Width * xPercent);
                float y = sizea.Height - (float)(sizea.Height * yPercent);
                x = x - (size.Width / 2);
                y = y - (size.Height / 2);

                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(
                    qrCode,
                    System.Drawing.Imaging.ImageFormat.Png);
                
                //image.Alignment = Element.ALIGN_CENTER | Element.ALIGN_MIDDLE;
                if (x < MINIMUM_BORDER + (size.Width / 2))
                {
                    x = MINIMUM_BORDER;
                    image.Alignment |= Element.ALIGN_LEFT;
                }
                else if (x > sizea.Width - size.Width - MINIMUM_BORDER)
                {
                    x = sizea.Width - size.Width - MINIMUM_BORDER;
                    image.Alignment |= Element.ALIGN_RIGHT;
                }
                else
                {
                    image.Alignment |= Element.ALIGN_CENTER;
                }

                if (y < MINIMUM_BORDER + (size.Height / 2))
                {
                    y = MINIMUM_BORDER;
                    image.Alignment |= Element.ALIGN_TOP;
                }
                else if (y > sizea.Height - size.Height - MINIMUM_BORDER)
                {
                    y = sizea.Height - size.Height - MINIMUM_BORDER;
                    image.Alignment |= Element.ALIGN_BOTTOM;
                }
                else
                {
                    image.Alignment |= Element.ALIGN_MIDDLE;
                }

                image.SetAbsolutePosition(x, y);
                image.ScaleToFit(size.Width, size.Height);
                pdfContentByte.AddImage(image);
                stamper.Close();
            }
        }
    }
}
