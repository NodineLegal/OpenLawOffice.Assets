namespace OpenLawOffice.Assets.Tracking
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;

    public class QRCode
    {
        public string Text { get; set; }
        public PointF[] Points { get; set; }

        public QRCode(string text)
        {
            Text = text;
        }

        public static List<QRCode> ExtractFrom(Image image)
        {
            List<QRCode> output = new List<QRCode>();
            Bitmap bitmap = new Bitmap(image);
            ZXing.QrCode.QRCodeReader qr = new ZXing.QrCode.QRCodeReader();
            ZXing.LuminanceSource source = new ZXing.BitmapLuminanceSource(bitmap);
            ZXing.Common.HybridBinarizer hybrid = new ZXing.Common.HybridBinarizer(source);
            ZXing.BinaryBitmap binBitmap = new ZXing.BinaryBitmap(hybrid);

            ZXing.Result[] multiresults = new ZXing.Multi.QrCode.QRCodeMultiReader().decodeMultiple(binBitmap);

            if (multiresults != null && multiresults.Length > 0)
            {
                foreach (ZXing.Result result in multiresults)
                {
                    QRCode qrCode = new QRCode(result.Text);
                    qrCode.Points = new PointF[result.ResultPoints.Length];
                    for (int i=0; i<result.ResultPoints.Length; i++)
                        qrCode.Points[i] = new PointF(result.ResultPoints[i].X, result.ResultPoints[i].Y);
                    output.Add(qrCode);
                }
            }

            ZXing.Result singleresult = qr.decode(binBitmap);

            if (singleresult != null)
            {
                QRCode qrCode = new QRCode(singleresult.Text);
                qrCode.Points = new PointF[4];
                qrCode.Points[0] = new PointF(singleresult.ResultPoints[0].X, singleresult.ResultPoints[0].Y);
                qrCode.Points[1] = new PointF(singleresult.ResultPoints[1].X, singleresult.ResultPoints[1].Y);
                qrCode.Points[2] = new PointF(singleresult.ResultPoints[2].X, singleresult.ResultPoints[2].Y);
                if (singleresult.ResultPoints.Length > 2)
                    qrCode.Points[3] = new PointF(singleresult.ResultPoints[3].X, singleresult.ResultPoints[3].Y);
                output.Add(qrCode);
            }

            return output;
        }

        public static Bitmap Generate(string content)
        {
            ZXing.BarcodeWriter writer = new ZXing.BarcodeWriter()
            {
                Format = ZXing.BarcodeFormat.QR_CODE,
                Options = new ZXing.Common.EncodingOptions()
                {
                    Height = 100,
                    Width = 100,
                    PureBarcode = true
                }            
            };

            return writer.Write(content);
        }
    }
}
