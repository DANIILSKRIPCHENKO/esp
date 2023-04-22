using Esp.Core.NeuralLayerNs;

namespace Esp.Core.NetworkNs
{
    public class SimpleRecurrentNetwork : INetwork
    {
        private readonly Guid _id = Guid.NewGuid();

        private readonly List<NeuralLayer> _layers = new List<NeuralLayer>();
        private IList<double> _expectedResult = new List<double>();

        private readonly int _geneSize;

        public SimpleRecurrentNetwork(
            NeuralLayer inputLayer,
            NeuralLayer hiddenLayer,
            NeuralLayer outputLayer)
        {
            AddFirstLayer(inputLayer);
            AddLayer(hiddenLayer);
            AddLayer(outputLayer);
        }

        public Guid GetId() => _id;


        public void PushExpectedValues(IList<double> expectedOutputs)
        {
            _expectedResult = expectedOutputs;
        }

        public void PushInputValues(IList<double> inputs)
        {
            var neurons = _layers.First().Neurons;

            foreach(var neuron in neurons.Select((neuron, i) => (neuron, i)))
            {
                neuron.neuron.PushValueOnInput(inputs[neuron.i]);
            }
        }

        public double ApplyFitness()
        {
            var fitness = CalculateFitness();

            var neurons = _layers
                .SelectMany(x => x.Neurons)
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

            var outputLayerNeurons = _layers.Last().Neurons;

            foreach (var neuron in outputLayerNeurons)
            {
                result.Add(neuron.CalculateOutput());
            }

            return result;
        }

        private void AddFirstLayer(NeuralLayer newLayer)
        {
            newLayer.Neurons
                .ToList()
                .ForEach(x => x.AddInputSynapse(0));

            _layers.Add(newLayer);
        }

        private void AddLayer(NeuralLayer newLayer)
        {
            if (_layers.Any())
            {
                var lastLayer = _layers.Last();
                newLayer.ConnectLayers(lastLayer);
            }

            _layers.Add(newLayer);
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
    }
}
