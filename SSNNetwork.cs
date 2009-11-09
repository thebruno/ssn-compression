using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SSNNetwork
{
    public class SSNNeuron
    {
        double Min = 0;
        double Max = 255;
        int[] inputs;
        public double[] weights;
        int matrixSize;
        Size matrix;
        public void SetInputs(int[] newInputs)
        {
            inputs = newInputs;
        }

        public double Error() {
            double temp = 0.0;
            for (int i = 0; i < matrixSize; i++)
                temp += Math.Abs(inputs[i] - weights[i]);
            temp /= (double) matrixSize;
            return temp;
        }
        
        public SSNNeuron(Size m, Random r)
        {
            matrix = m;
            matrixSize = matrix.Width * matrix.Height;
            inputs = new int[matrixSize];
            weights = new double[matrixSize];
            for (int i = 0; i < matrixSize; i++)
                weights[i] = 128;//(r.NextDouble() * (Max - Min)) + Min;
        }
        public void Improve(double factor)
        {
            for (int i = 0; i < matrixSize; i++)
                weights[i] += factor * (inputs[i] - weights[i]);
        }

    }

    public class SSNLayer
    {
        public ArrayList neurons;
        int matrixSize;
        int neuronCount;
        Size matrix;

        Random random = new Random((int)DateTime.Now.Millisecond);
        public SSNLayer(int count, Size m)
        {
            matrix = m;
            neuronCount = count;
            matrixSize = matrix.Width * matrix.Height;
            neurons = new ArrayList(matrixSize);

            for (int i = 0; i < neuronCount; i++)
                neurons.Add(new SSNNeuron(matrix, random));
        }

        public void Learn(double factor, int[] inputs)
        {
            int winner = GetWinnerForInputs(inputs);
            (neurons[winner] as SSNNeuron).Improve(factor);
            //for (int i = 0; i < 10; i++) {
            //    int temp = random.Next(255);
            //    (neurons[temp] as SSNNeuron).SetInputs(inputs);
            //    (neurons[temp] as SSNNeuron).Improve(factor/10.0); 
            //}
        }
        public int GetWinnerForInputs(int[] inputs)
        {
            int pos = 0;
            double min = double.MaxValue;
            for (int i = 0; i < neuronCount; i++)
            {
                SSNNeuron n = neurons[i] as SSNNeuron;
                n.SetInputs(inputs);
                if (min > n.Error())
                {
                    pos = i;
                    min = n.Error();
                }
            }
            return pos;
        }
        // get weights for particular neuron
        public double[] GetWeights(int neuron)
        {
            return (neurons[neuron] as SSNNeuron).weights;
        }
        
    }


}
