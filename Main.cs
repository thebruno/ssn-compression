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
                    BitmapEx bitmap = new BitmapEx(path, new Size(16, 16),  true);
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
                SSNCompress compress = new SSNCompress(512, new Size(8, 8));
                if (!checkBox1.Checked)
                    compress.Compress(path, saveFileDialog1.FileName);
                else
                    compress.DeCompress(path, saveFileDialog1.FileName);

            }            
        }
    }
}
