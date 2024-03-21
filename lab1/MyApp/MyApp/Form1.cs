using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using BusinessLogic;

namespace MyApp
{
    public partial class Form1 : Form
    {
        ImageFilter filter;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog Openfile = new OpenFileDialog();
            if (Openfile.ShowDialog() == DialogResult.OK)
            {
                Image<Bgr, Byte> myImage = new Image<Bgr, byte>(Openfile.FileName);
                filter = new ImageFilter(myImage);
                pictureBox1.Image = filter.originalImage.ToBitmap();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double bias, gain;
            if (Double.TryParse(textBox1.Text, out bias) && Double.TryParse(textBox2.Text, out gain))
            {
                filter.ChangeBrightness(gain, bias);
                pictureBox1.Image = filter.editedImage.ToBitmap();
            }
            else MessageBox.Show("Invalid input for bias or gain");

        }


        private void button3_Click(object sender, EventArgs e)
        {
            filter.GenerateHistogram();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            double gamma;
            if (Double.TryParse(textBox3.Text, out gamma))
            {
                filter.ApplyGammaCorrection(gamma);
                pictureBox1.Image = filter.editedImage.ToBitmap();
            }
            else MessageBox.Show("Invalid input for gamma");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = filter.RotateImage().ToBitmap();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = filter.ResizeImage().ToBitmap();
        }
    }
}
