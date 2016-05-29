namespace OpenLawOffice.Assets.Tracking.PDF
{
    using System.Collections.Generic;

    public class ImageContainer
    {
        public PdfImage Image { get; set; }
        public List<QRCode> QrCodes { get; set; }

        public ImageContainer()
        {
            QrCodes = new List<QRCode>();
        }
    }
}
