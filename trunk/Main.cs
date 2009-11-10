using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSNUtilities;

namespace SSNCompression
{
    public partial class Main : Form
    {
        string path;
        
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (!checkBox1.Checked)
                {
                    path = openFileDialog1.FileName;
                    BitmapEx bitmap = new BitmapEx(path, new Size(4, 4),  true);
                    pictureBox1.Image = bitmap.GetBitmap;
                } 
                else
                    path = openFileDialog1.FileName;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SSNCompress compress = new SSNCompress(256, new Size(2, 2));
                if (!checkBox1.Checked)
                    compress.Compress(path, saveFileDialog1.FileName);
                else
                    compress.DeCompress(path, saveFileDialog1.FileName);

            }            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string p1 = @"d:\Programming\PolSl\ssn\SSNCompressor\64x64x8";
            string p2 = @"d:\Programming\PolSl\ssn\SSNCompressor\1024x768x8";
            string p = p2;
            SSNCompress compress = new SSNCompress(256, new Size(4, 4));
            compress.Compress(p+ ".bmp", p+".bin");
            compress.DeCompress(p +".bin", p +"_org.bmp");
            BitmapEx bitmap = new BitmapEx(p + "_org.bmp", new Size(4, 4), true);
            pictureBox1.Image = bitmap.GetBitmap;
        }
    }
}
