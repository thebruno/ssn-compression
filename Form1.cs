using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SSNCompressor
{
    public partial class Form1 : Form
    {
        string path;
        Image image;


        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog1.FileName;
                BitmapEx bitmap = new BitmapEx(path, true);
                pictureBox1.Image = bitmap.GetBitmap;

                int [] pixels = bitmap.GetPixels;
                Bitmap b = new BitmapEx(pixels, bitmap.GetSize).GetBitmap;


            }

        }
    }
}
