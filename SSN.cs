using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections;

namespace SSNCompressor
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
    public delegate double Function(double val);

    class Connection
    {
        public Neuron N{get; set;}
        public double Weight{get; set;}
        Connection(Neuron n, double weight)
        {
            N = n;
            Weight = weight;
        }
    }

    class Neuron
    {
        public Function ActivationFunction { get; set; }
        public List<Connection> Inputs { get; set; }        
        public double Error { get; set; }
        public double Output { get; set; }

        double Constant { get; set; }
        Random Rand = new Random (DateTime.Now.Millisecond);

        Neuron(double constantVal, Function func)
        {
            ActivationFunction = func;
            Inputs = new List<Connection>();
            Error = 0.0;
            Output = 0.0;
        }
        double Count()
        {
            Output = 0;
            foreach (Connection c in Inputs)
                Output += c.N.Output * c.Weight;
            Output += Constant;
            Output = ActivationFunction(Output);
            return Output;
        }
        public void Randomise(double Min, double Max)
        {            
            foreach (Connection c in Inputs)
            {
                c.Weight = (Rand.NextDouble() * (Max - Min)) + Min;
            }
            Constant = (Rand.NextDouble() * (Max - Min)) + Min;
        }
    }
    class Layer
    { }

    class Network
    { }

    public class SSNCompression
    {

        public void Compress()
        {}

        public void Decompress()
        { }

    }
}
