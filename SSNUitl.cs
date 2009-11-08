using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections;

namespace SSNUtilities
{
    class BitmapEx
    {
        Bitmap bitmap;
        int[] pixels;

        public int[] GetPixels { get { return pixels; } }
        public Bitmap GetBitmap { get { return bitmap; } }
        public Size GetSize { get { return new Size(bitmap.Width, bitmap.Height); } }


        private Bitmap ToBitmap(int[] p, Size s)
        {
            Bitmap toReturn = new Bitmap(s.Width, s.Height);
            for (int i = 0; i < s.Width; i++)
                for (int j = 0; j < s.Height; j++)
                    toReturn.SetPixel(i, j, Color.FromArgb(pixels[i + j * s.Width]));
            return toReturn;
        }
        private int[] ToPixels(Bitmap b)
        {
            int[] toReturn = new Int32[b.Width * b.Height];
            for (int i = 0; i < b.Width; i++)
                for (int j = 0; j < b.Height; j++)
                    toReturn[i + j * b.Width] = b.GetPixel(i, j).ToArgb();
            return toReturn;
        }

        public BitmapEx(Bitmap b)
        {
            bitmap = b;
            pixels = ToPixels(b);            
        }

        public BitmapEx(int[] p, Size s)
        {
            pixels = p;
            bitmap = ToBitmap(p, s);
        }

        public BitmapEx(string path, bool compression)
        {
            if (compression)
            {
                bitmap = new Bitmap(path);
                pixels = ToPixels(bitmap);  
            }
        }


    }
    

    public class SSNCompression
    {

        public void Compress()
        {}

        public void Decompress()
        { }

    }
}
