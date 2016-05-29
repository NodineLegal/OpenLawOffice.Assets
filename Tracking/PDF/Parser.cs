
namespace OpenLawOffice.Assets.Tracking.PDF
{
    using System.Collections.Generic;
    using System.IO;

    public class Parser
    {
        public static List<ImageContainer> Parse(string filepath)
        {
            List<ImageContainer> imageContainers = new List<ImageContainer>();

            if (!File.Exists(filepath))
                throw new FileNotFoundException("File not found", filepath);

            // Only look at page 1 - serious memory saver for hundred page PDFs
            PdfImageExtractor.ExtractImagesFromPage1(filepath).ForEach(x =>
            {
                ImageContainer ic = new ImageContainer()
                {
                    Image = x,
                    QrCodes = new List<QRCode>()
                };

                // Of note: QRCodes of extreme size will NOT be detected by ZXing so we need to drop scale a few times, checking each

                // 1:1
                QRCode.ExtractFrom(ic.Image.DrawingImage).ForEach(y =>
                {
                    ic.QrCodes.Add(y);
                });

                // 5:1
                QRCode.ExtractFrom(ImageUtils.ResizeImage(ic.Image.DrawingImage, ic.Image.DrawingImage.Width/5, ic.Image.DrawingImage.Height/5)).ForEach(y =>
                {
                    if (!ic.QrCodes.Exists(model => model.Text == y.Text))
                        ic.QrCodes.Add(y);
                });

                // 10:1
                QRCode.ExtractFrom(ImageUtils.ResizeImage(ic.Image.DrawingImage, ic.Image.DrawingImage.Width / 10, ic.Image.DrawingImage.Height / 10)).ForEach(y =>
                {
                    if (!ic.QrCodes.Exists(model => model.Text == y.Text))
                        ic.QrCodes.Add(y);
                });
                
                imageContainers.Add(ic);
            });

            return imageContainers;
        }
    }
}
