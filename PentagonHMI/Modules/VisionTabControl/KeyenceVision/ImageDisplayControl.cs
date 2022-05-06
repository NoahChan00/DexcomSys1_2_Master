using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Keyence.IV.Sdk.Sample_CSharp.Parts
{
    public partial class ImageDisplayControl : UserControl
    {
        private static Size ImageSize = new Size(320, 240);
        private byte activeToolNo;
        private IVisionSensor sensor;

        public ImageDisplayControl()
        {
            InitializeComponent();
        }

        public void Initialize(IVisionSensor visionSensor)
        {
            CleanUp();
            sensor = visionSensor;
            sensor.ImageAcquired += SensorImageAcquired;
        }

        public void RefreshState()
        {
            var image = new Bitmap(ImageSize.Width, ImageSize.Height, PixelFormat.Format24bppRgb);
            using (Graphics graphics = Graphics.FromImage(image))
            {
                graphics.FillRectangle(new SolidBrush(Color.FromArgb(192, 192, 192)),
                                       new Rectangle(Point.Empty, image.Size));
            }
            if (pictureBox.Image != null)
            {
                pictureBox.Image.Dispose();
            }
            pictureBox.Image = image;
        }

        public void CleanUp()
        {
            if (sensor != null)
            {
                sensor.ImageAcquired -= SensorImageAcquired;
            }
            pictureBox.Image = null;
            activeToolNo = 0;
        }

        public void ChangeActiveTool(byte toolNo)
        {
            activeToolNo = toolNo;
        }

        private void SensorImageAcquired(object sender, ImageAcquiredEventArgs e)
        {
            var image = new Bitmap(ImageSize.Width, ImageSize.Height, PixelFormat.Format24bppRgb);
            BitmapData lockData = image.LockBits(new Rectangle(Point.Empty, ImageSize), ImageLockMode.WriteOnly,
                                                 PixelFormat.Format24bppRgb);
            Marshal.Copy(e.LiveImage.ByteData, 0, lockData.Scan0, e.LiveImage.ByteData.Length);
            image.UnlockBits(lockData);
            using (Graphics graphics = Graphics.FromImage(image))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                sensor.DrawWindow(graphics, Color.Green, Color.Red, activeToolNo);
            }
            if (pictureBox.Image != null)
            {
                pictureBox.Image.Dispose();
            }
            pictureBox.Image = image;
        }
    }
}