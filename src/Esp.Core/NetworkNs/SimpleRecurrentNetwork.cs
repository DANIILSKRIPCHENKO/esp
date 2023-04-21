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
            AddLayer(inputLayer);
            AddLayer(hiddenLayer);
            AddLayer(outputLayer);
        }

        public Guid GetId() => _id;

        public IEnumerable<double> GetOutput()
        {
            throw new NotImplementedException();
        }

        public void PushExpectedValues(double[][] expectedOutputs)
        {
            _expectedResult = expectedOutputs;
        }

        public void PushInputValues(double[] inputs)
        {
            var neurons = _layers.First().Neurons.ToList();

            foreach(var neuron in neurons)
            {
                neuron.PushValueOnInput(inputs[neurons.IndexOf(neuron)]);
            }
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
