using Esp.Core.NeuralLayerNs;

namespace Esp.Core.NetworkNs
{
    public class SimpleRecurrentNetwork : INetwork
    {
        private readonly Guid _id = Guid.NewGuid();

        private readonly IInputLayer _inputLayer;
        private readonly IOutputLayer _outputLayer;
        private readonly IList<IHiddenLayer> _hiddenLayers = new List<IHiddenLayer>();

        private IList<double> _expectedResult = new List<double>();

        public SimpleRecurrentNetwork(
            IInputLayer inputLayer,
            IList<IHiddenLayer> hiddenLayers,
            IOutputLayer outputLayer)
        {
            _inputLayer = inputLayer;
            _outputLayer = outputLayer;
            AddHiddenLayers(hiddenLayers);
        }

        public Guid GetId() => _id;


        public void PushExpectedValues(IList<double> expectedOutputs)
        {
            _expectedResult = expectedOutputs;
        }

        public void PushInputValues(IList<double> inputs)
        {
            var inputNeurons = _inputLayer.InputNeurons;

            foreach(var neuron in inputNeurons.Select((neuron, i) => (neuron, i)))
            {
                neuron.neuron.PushValueOnInput(inputs[neuron.i]);
            }
        }

        public double ApplyFitness()
        {
            var fitness = CalculateFitness();

            var neurons = _hiddenLayers
                .SelectMany(x => x.HiddenNeurons)
                .ToList();

            foreach(var neuron in neurons)
            {
                neuron.AddFitness(fitness);
            }

            return fitness;
        }

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

            if (output.Count != _expectedResult.Count)
                throw new Exception("Failed to calculate fitness");

            var error = output
                .Select((output, index) => Math.Abs(output - _expectedResult[index]))
                .Sum();

            return 1 / error;
        }

        public void ResetConnection()
        {
            _hiddenLayers.Single().ResetConnections();
            _inputLayer.ResetConnections();
            _outputLayer.ResetConnections();
        }
    }
}
