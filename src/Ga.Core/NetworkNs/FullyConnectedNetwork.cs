using Ga.Core.LossFunction;
using Ga.Core.Models;
using Ga.Core.NeuralLayerNs.Hidden;
using Ga.Core.NeuralLayerNs.Input;
using Ga.Core.NeuralLayerNs.Output;
using Ga.Core.Task;
using Newtonsoft.Json;

namespace Ga.Core.NetworkNs
{
    /// <summary>
    /// Fully Connected Network implementation of INetwork interface
    /// </summary>
    public class FullyConnectedNetwork : INeuralNetwork
    {
        private readonly Guid _id = Guid.NewGuid();

        [JsonProperty]
        private readonly IInputLayer _inputLayer;
        [JsonProperty]
        private readonly IOutputLayer _outputLayer;
        [JsonProperty]
        private readonly IList<IHiddenLayer> _hiddenLayers = new List<IHiddenLayer>();
        [JsonProperty]
        private readonly ILossFunction _lossFunction;
        private IList<double> _expectedResult = new List<double>();


        public FullyConnectedNetwork(
            IInputLayer inputLayer,
            IList<IHiddenLayer> hiddenLayers,
            IOutputLayer outputLayer,
            ILossFunction lossFunction)
        {
            _inputLayer = inputLayer;
            _outputLayer = outputLayer;
            AddHiddenLayers(hiddenLayers);
            _lossFunction = lossFunction;
        }

        [JsonConstructor]
        public FullyConnectedNetwork()
        {

        }

        #region INetwork implementation

        public Guid GetId() => _id;

        public void PushInputValues(IList<double> inputs)
        {
            var inputNeurons = _inputLayer.InputNeurons;

            foreach (var neuron in inputNeurons.Select((neuron, i) => (neuron, i)))
            {
                neuron.neuron.PushValueOnInput(inputs[neuron.i]);
            }
        }

        public void BindLayers()
        {
            AddHiddenLayers(_hiddenLayers);
        }

        public IList<double> GetOutputs() => GetOutput();

        public EvaluationResult EvaluateOnDataset(ITask task)
        {
            double cumulativeFitness = 0;
            double cumulativeAccuracy = 0;
            foreach (var dataframe in task.GetDataset())
            {
                PushInputValues(dataframe.Inputs);
                PushExpectedValues(dataframe.ExpectedOutputs);

                cumulativeFitness += CalculateFitness();
                cumulativeAccuracy += CalculateAccuracy();
            }

            var fitness =  cumulativeFitness / task.GetDataset().Count;
            var error = 1 / fitness;
            var accuracy = cumulativeAccuracy / task.GetDataset().Count;

            return new EvaluationResult()
            {
                Loss = error,
                Fitness = fitness,
                Accuracy = accuracy
            };
        }

        public double ApplyFitness(double fitness)
        {
            var neurons = _hiddenLayers
                .SelectMany(x => x.HiddenNeurons)
                .ToList();

            foreach (var neuron in neurons)
            {
                neuron.AddFitness(fitness);
            }

            return fitness;
        }

        public void ResetConnection()
        {
            _hiddenLayers.Single().ResetConnections();
            _inputLayer.ResetConnections();
            _outputLayer.ResetConnections();
        }

        #endregion


        #region Private Methods

        private IList<double> GetOutput()
        {
            var outputLayerNeurons = _outputLayer.OutputNeurons;

            return outputLayerNeurons.Select(neuron => neuron.CalculateOutput()).ToList();
        }

        private void AddHiddenLayers(IList<IHiddenLayer> hiddenLayers)
        {
            // Now only one hidden layer supported
            var hiddenLayer = hiddenLayers.Single();

            hiddenLayer.ConnectInput(_inputLayer);
            hiddenLayer.ConnectOutput(_outputLayer);

            _hiddenLayers.Add(hiddenLayer);
        }

        // TODO: infinite when error is 0
        private double CalculateFitness()
        {
            var output = GetOutput();

            var error = _lossFunction.CalculateError(output, _expectedResult);

            return 1 / error;
        }

        private int CalculateAccuracy()
        {
            var output = GetOutput();
            var probabilities = GetProbabilities(output);
            var formattedOutPut = GetFormattedOutput(probabilities);

            return formattedOutPut.SequenceEqual(_expectedResult.Select(Convert.ToInt32)) ? 1 : 0;
        }

        private List<int> GetFormattedOutput(IList<double> predicted)
        {
            var probabilities = GetProbabilities(predicted);
            var result = probabilities.Select(Convert.ToInt32).ToList();
            return result;
        }

        private List<double> GetProbabilities(IList<double> values) => values
            .Select(value =>
                Math.Exp(value) / values.Select(Math.Exp).Sum()).ToList();

        private void PushExpectedValues(IList<double> expectedOutputs)
        {
            _expectedResult = expectedOutputs;
        }

        #endregion
    }
}
