using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;

namespace BusinessLogic
{
    public class ImageFilter
    {
        public Image<Bgr, Byte> originalImage { get; set; }
        public Image<Bgr, Byte> editedImage { get; set; }
        public int rotationAngle { get; set; }
        public double scale { get; set; }

        public ImageFilter(Image<Bgr, Byte> image) {
            originalImage = image;
            rotationAngle = 0;
            editedImage = image;
            scale = 1;
        }
        public Image<Bgr, Byte> ApplyGammaCorrection(double value)
        {
            editedImage._GammaCorrect(value);
            return editedImage;
        }

        public Image<Bgr, Byte> ChangeBrightness(double gain, double bias)
        {
            for (int i = 0; i < originalImage.Width; i++)
            {
                for (int j = 0; j < originalImage.Height; j++)
                {
                    Bgr currentColor = originalImage[j, i];

                    currentColor.Blue = currentColor.Blue * gain + bias;
                    currentColor.Green = currentColor.Green * gain + bias;
                    currentColor.Red = currentColor.Red * gain + bias;

                    editedImage[j, i] = currentColor;
                }
            }
            return editedImage;
        }

        public void GenerateHistogram()
        {
            HistogramViewer v = new HistogramViewer();
            v.HistogramCtrl.GenerateHistograms(editedImage, 255);
            v.Show();
        }

        public Image<Bgr, Byte> RotateImage()
        {
            Bgr color = new Bgr(1,0,0);
            rotationAngle += rotationAngle % 360 + 10;
            editedImage = originalImage.Rotate(rotationAngle, color, false);
            return editedImage;
        }

        public Image<Bgr, Byte> ResizeImage()
        {
            scale += 0.25;
            editedImage = originalImage.Resize(scale, Emgu.CV.CvEnum.Inter.Cubic);
            return editedImage;
        }
    }
}
