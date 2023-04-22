using Esp.Core.Common;
using Esp.Core.NetworkNs;
using Esp.Core.NeuralLayerNs;
using Esp.Core.NeuronNs;
using Esp.Core.PopulationNs;

namespace Esp.Core.EspNS
{
    public class Esp : IEsp
    {
        private readonly Guid _id = Guid.NewGuid();
        private readonly IList<IPopulation> _populations;

        public Esp(IList<IPopulation> populations)
        {
            _populations = populations;
        }

        public Guid GetId() => _id;

        public void Evaluate()
        {
            while (ShouldContinueEvolution())
            {
                var neuronsInUsed = new List<INeuron>();

                var randomNeuronsForInput = _populations
                    .Select(population => population.GetRandomNeuronNotIn(neuronsInUsed))
                    .ToList();

                neuronsInUsed.AddRange(randomNeuronsForInput);

                var randomNeuronsForHidden = _populations
                    .Select(population => population.GetRandomNeuronNotIn(neuronsInUsed))
                    .ToList();

                neuronsInUsed.AddRange(randomNeuronsForHidden);

                var randomNeuronsForOutput = _populations
                    .Select(population => population.GetRandomNeuronNotIn(neuronsInUsed))
                    .ToList();

                neuronsInUsed.AddRange(randomNeuronsForOutput);

                CheckUniqueness(neuronsInUsed);

                var inputLayer = new NeuralLayer(randomNeuronsForInput);
                var hiddenLayer = new NeuralLayer(randomNeuronsForHidden);
                var outputLayer = new NeuralLayer(randomNeuronsForOutput);

                var network = new SimpleRecurrentNetwork(inputLayer, hiddenLayer, outputLayer);

                network.PushExpectedValues(new List<double>(){ 0.3, 0.5, 0.5, 0.5, 0.5 });

                network.PushInputValues(new List<double> { 1054, 54, 234, 763, 21 });

                network.ApplyFitness();

                int t = 5;
            }

            int p = 5;
        }

        private void CheckUniqueness(IEnumerable<IId> idCollection)
        {
            var duplicatedElements = idCollection
                .GroupBy(x => x.GetId())
                .Where(x => x.Count() > 1);

            if (!duplicatedElements.Any())
                return;

            throw new Exception("Duplicate elements found");
        }

        private bool ShouldContinueEvolution() => _populations
            .SelectMany(population => population.GetNeurons())
            .Any(neuron => neuron.Trials < 10);
    }
}
