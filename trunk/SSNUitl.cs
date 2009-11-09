using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections;

namespace SSNUtilities
{
    class BitmapEx : IEnumerable
    {
        Bitmap bitmap;
        int[] pixels;
        Size matrix;

        public int[] GetPixels { get { return pixels; } }
        public Bitmap GetBitmap { get { return bitmap; } }
        public Size GetSize { get { return new Size(bitmap.Width, bitmap.Height); } }


        private Bitmap ToBitmap(int[] p, Size s)
        {
            Bitmap toReturn = new Bitmap(s.Width, s.Height);
            for (int i = 0; i < s.Width; i++)
                for (int j = 0; j < s.Height; j++)
                {
                    int temp;
                    unchecked
                    {
                        temp = (int) 0xff000000 | pixels[i + j * s.Width] << 16 | pixels[i + j * s.Width] << 8 | pixels[i + j * s.Width];
                    }
                    toReturn.SetPixel(i, j, Color.FromArgb(temp));
                }
            return toReturn;
        }
        private int[] ToPixels(Bitmap b)
        {
            int[] toReturn = new Int32[b.Width * b.Height];
            for (int i = 0; i < b.Width; i++)
                for (int j = 0; j < b.Height; j++)
                    toReturn[i + j * b.Width] = (byte) b.GetPixel(i, j).ToArgb();
            return toReturn;
        }

        public BitmapEx(Bitmap b, Size matrixVal)
        {
            matrix = matrixVal;
            bitmap = b;
            pixels = ToPixels(b);
        }
        public BitmapEx(Size s,Size matrixVal)
        {
            matrix = matrixVal;
            bitmap = new Bitmap(s.Width, s.Height);           
        }

        public BitmapEx(int[] p, Size s, Size matrixVal)
        {
            matrix = matrixVal;
            pixels = p;
            bitmap = ToBitmap(p, s);
        }

        public BitmapEx(string path, Size matrixVal, bool compression)
        {
            matrix = matrixVal;
            if (compression)
            {
                bitmap = new Bitmap(path);
                pixels = ToPixels(bitmap);
            }
        }

        public void SetMatrix(Size s)
        {
            matrix = s;
        }

        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < bitmap.Height ; i += matrix.Height)
            {
                for (int j = 0; j < bitmap.Width ; j += matrix.Width)
                {
                    int[] result = new int[matrix.Height * matrix.Width];
                    for (int k = 0; k < matrix.Height; k++)
                    {
                        for (int w = 0; w < matrix.Width; w++)
                        {
                            result[k * matrix.Width + w] = pixels[(i + k) * bitmap.Width + j + w];
                            
                        }

                    }
                    yield return result;
                }
            }
        }

        #endregion
    }
}
