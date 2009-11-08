using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSNNetwork
{
    public delegate double Function(double val);


    //TODO: add error checking
    public class SSNConnection
    {
        public SSNNeuron N { get; set; }
        public double Weight { get; set; }
        public SSNConnection(SSNNeuron n, double weight)
        {
            N = n;
            Weight = weight;
        }
    }

    public class SSNNeuron
    {
        public Function ActivationFunction { get; set; }
        public List<SSNConnection> Inputs { get; set; }
        public double Error { get; set; }
        public double Output { get; set; }

        double Constant { get; set; }
        Random Rand = new Random(DateTime.Now.Millisecond);

        public SSNNeuron( Function activation)
        {
            ActivationFunction = activation;
            Inputs = new List<SSNConnection>();
            Error = 0.0;
            Output = 0.0;
            Constant = 0.0;
        }
        #region public Methods
        public void Randomise(double Min, double Max)
        {
            foreach (SSNConnection c in Inputs)
            {
                c.Weight = (Rand.NextDouble() * (Max - Min)) + Min;
            }
            Constant = (Rand.NextDouble() * (Max - Min)) + Min;
        }

        public void AddInput(SSNNeuron n, double weight)
        {
            Inputs.Add(new SSNConnection(n, weight));
        }
        public void AddInputs(SSNLayer layer, double weight)
        {
            foreach (SSNNeuron n in layer.Neurons)
                AddInput(n, weight);
        }
        public void AddInput(SSNNeuron n)
        {
            AddInput(n, 1.0);
        }
        public void AddInputs(SSNLayer layer)
        {
            AddInputs(layer, 1.0);
        }

        double Count()
        {
            Output = 0;
            foreach (SSNConnection c in Inputs)
                Output += c.N.Output * c.Weight;
            Output += Constant * 1.0;
            Output = ActivationFunction(Output);
            return Output;
        }

        public double CountError(double correctVal)
        {
            Error = correctVal - Output;
            return Error;
        }

        public void Learn(double factor)
        {
            foreach (SSNConnection c in Inputs)
            {
                //TODO:
                //c.Weight += factor * Error * LeariningFunction(Output) * c.N.Output;
            }
            //TODO:
            //Constant += factor * Error * LeariningFunction(Output) * 1.0;
        }
        #endregion
    }


    public class SSNLayer
    {
        public List<SSNNeuron> Neurons { get; set; }
        public SSNLayer(int count, Function activation)
        {
            Neurons = new List<SSNNeuron>();
            for (int i = 0; i < count; ++i)
                Neurons.Add(new SSNNeuron(activation));
        }
        public void ConnectToLayer(SSNLayer previousLayer)
        {
            foreach (SSNNeuron n in Neurons)
            {
                n.AddInputs(previousLayer);
            }
        }
        public void RandomiseAllNeurons(double min, double max){
            foreach (SSNNeuron n in Neurons)
            {
                n.Randomise(min, max);
            }
        }

    }

    public class SSNNetwork
    { }
}
