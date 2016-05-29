namespace OpenLawOffice.Assets.Tracking.PDF
{
    using System.Drawing;
    using System.Windows.Media.Imaging;

    public class PdfImage
    {
        private BitmapImage _bitmapImage;

        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public Image DrawingImage { get; set; }
        public BitmapImage BitmapImage
        {
            get
            {
                if (_bitmapImage == null)
                    return _bitmapImage = ImageUtils.ImageToBitmapImage(DrawingImage);
                return _bitmapImage;
            }
            set { _bitmapImage = value; }
        }
        public string Extension { get; set; }

        public PdfImage()
        {
        }
    }
}
