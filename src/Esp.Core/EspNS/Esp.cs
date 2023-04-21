using Esp.Core.NetworkNs;
using Esp.Core.NeuralLayerNs;
using Esp.Core.NeuronNs;
using Esp.Core.PopulationNs;

namespace Esp.Core.EspNS
{
    public class Esp : IEsp
    {
        private readonly Guid _id = Guid.NewGuid();
        private readonly IEnumerable<IPopulation> _populations;

        public Esp(IEnumerable<IPopulation> populations)
        {
            _populations = populations;
        }

        public Guid GetId() => _id;

        public void Evaluate()
        {
            var randomNeuronsForInput = _populations
                .Select(population => population.GetRandomNeuron());

            var randomNeuronsForHidden = _populations
                .Select(population => population.GetRandomNeuron());

            var randomNeuronsForOutput = _populations
                .Select(population => population.GetRandomNeuron());

            var inputLayer = new NeuralLayer(randomNeuronsForInput);
            var hiddenLayer = new NeuralLayer(randomNeuronsForHidden);
            var outputLayer = new NeuralLayer(randomNeuronsForOutput);

            var network = new SimpleRecurrentNetwork(inputLayer, hiddenLayer, outputLayer);

            network.PushExpectedValues(
                new double[][] {
                    new double[] { 0 },
                    new double[] { 1 },
                    new double[] { 1 },
                    new double[] { 0 },
                    new double[] { 1 },
                    new double[] { 0 },
                    new double[] { 0 },
                });

            network.PushInputValues(new double[] { 1054, 54, 1 });

            var outputs = network.GetOutput();
        }
    }
}
