using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SSNNetwork;
using SSNUtilities;
using System.Drawing;
using System.IO;

namespace SSNCompression
{    
    public class SSNCompress
    {
        SSNLayer layer;
        BitmapEx bitmap;
        Size matrix;
        int matrixSize;
        double factor;
        int step;
        int neurons;
        public SSNCompress(int neuronsCount, Size matrixSizeVal)
        {
            neurons = neuronsCount;
            matrix = matrixSizeVal;
            matrixSize = matrixSizeVal.Height * matrixSizeVal.Width;
            layer = new SSNLayer(neuronsCount, matrix);            
        }
        double Factor
        {
            get { 
              //return factor * Math.Exp(((double) -step++) /10.0); 
                return factor * (-Math.Exp(((double)step++) / 15.0) + 2); 
            }
            set { step = 0;
                factor = value;
            }
        }
        public void Compress(string pathIn, string pathOut)
        {
            // open bitmap
            bitmap = new BitmapEx(pathIn, matrix, true);

            // create output file

            BinaryWriter binWriter = new BinaryWriter(File.Open(pathOut, FileMode.Create));
                                   

            // learn
            Factor = 1;
            double f  = 1;
            for (int i = 0; i < 10; i++)
            {
                f -= 1 / 10;
                foreach (int[] temp in bitmap)
                    layer.Learn(f, temp);
                
            }
            // save 
            // wymiary obrazka intxint
            // ilosc neuronow int
            // wymiary maski bytexbyte
            // po kolei wagi każdego neuronu doulbe * rozmiar maski
            binWriter.Write(bitmap.GetSize.Width);            
            binWriter.Write(bitmap.GetSize.Height);
            binWriter.Write((byte)matrix.Width);
            binWriter.Write((byte)matrix.Height);
            binWriter.Write(layer.neurons.Count);
            foreach (SSNNeuron n in layer.neurons)
                foreach (double d in n.weights)
                    binWriter.Write((byte)Math.Round(d));


            foreach (int[] temp in bitmap)
            {
                int neuron = layer.GetWinnerForInputs(temp);
                //double[] weights = layer.GetWeights(neuron);
                binWriter.Write((byte)neuron);
            }

            binWriter.Flush();
            binWriter.Close();           
        }

        public void DeCompress(string pathIn, string pathOut)
        {
            BinaryReader binReader = new BinaryReader(File.Open(pathIn, FileMode.Open));
            Size bitmapSize = new Size();
            bitmapSize.Width = binReader.ReadInt32();
            bitmapSize.Height = binReader.ReadInt32();
            matrix = new Size();
            matrix.Width = binReader.ReadByte();
            matrix.Height = binReader.ReadByte();
            matrixSize = matrix.Width * matrix.Height;
            neurons = binReader.ReadInt32();

            layer = new SSNLayer(neurons, matrix);

            int[] pixels = new Int32[bitmapSize.Width * bitmapSize.Height];


            // read dictionary
            foreach (SSNNeuron n in layer.neurons)
                for (int i = 0; i < matrixSize; ++i)
                    n.weights[i] = binReader.ReadByte();


            
            int temp;
            for (int i = 0; i < bitmapSize.Height; i += matrix.Height)
            {
                for (int j = 0; j < bitmapSize.Width; j += matrix.Width)
                {
                    int winner = binReader.ReadByte();
                    double[] weights = (layer.neurons[winner] as SSNNeuron).weights;
                    for (int k = 0; k < matrix.Height; k++)
                    {
                        for (int w = 0; w < matrix.Width; w++)
                        {
                            unchecked
                            {
                                temp = (int)0xff000000 | (byte)weights[k * matrix.Width + w] << 16 | (byte)weights[k * matrix.Width + w] << 8 | (byte)weights[k * matrix.Width + w];
                                pixels[(i + k) * bitmapSize.Width + j + w] = temp; 
                            }
                        }

                    }
                }
            }
            bitmap = new BitmapEx(pixels, bitmapSize, matrix);          
            // wczytaj słownik i parametry
            // utworz bitmape
            // numer neuronu wskazuje przyblizany fragment bitmapy
            

            bitmap.GetBitmap.Save(pathOut, System.Drawing.Imaging.ImageFormat.Bmp);
            binReader.Close();
        }
    }
}
