using Ga.Core.LossFunction;
using Ga.Core.NeuralLayerNs.Hidden;
using Ga.Core.NeuralLayerNs.Input;
using Ga.Core.NeuralLayerNs.Output;
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


        public void PushExpectedValues(IList<double> expectedOutputs)
        {
            _expectedResult = expectedOutputs;
        }

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

        public double GetFitness() => CalculateFitness();

        public double ApplyFitness()
        {
            var fitness = CalculateFitness();

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
            var result = new List<double>();

            var outputLayerNeurons = _outputLayer.OutputNeurons;

            foreach (var neuron in outputLayerNeurons)
            {
                result.Add(neuron.CalculateOutput());
            }

            return result;
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

        #endregion
    }
}
