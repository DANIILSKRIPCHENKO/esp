using Esp.Core.NeuralLayerNs;

namespace Esp.Core.NetworkNs
{
    public class SimpleRecurrentNetwork : INetwork
    {
        private readonly Guid _id = Guid.NewGuid();

        private readonly List<NeuralLayer> _layers = new List<NeuralLayer>();

        private readonly int _geneSize;
        private double[][] _expectedResult;

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

        public IList<double> GetOutput()
        {
            var result = new List<double>();

            var outputLayerNeurons = _layers.Last().Neurons;

            foreach(var neuron in outputLayerNeurons)
            {
                result.Add(neuron.CalculateOutput());
            }

            return result;
        }

        public void PushExpectedValues(double[][] expectedOutputs)
        {
            _expectedResult = expectedOutputs;
        }

        public void PushInputValues(double[] inputs)
        {
            var neurons = _layers.First().Neurons;

            foreach(var neuron in neurons.Select((neuron, i) => (neuron, i)))
            {
                neuron.neuron.PushValueOnInput(inputs[neuron.i]);
            }
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
    }
}
