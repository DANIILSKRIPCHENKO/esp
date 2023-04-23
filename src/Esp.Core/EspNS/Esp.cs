using Esp.Core.Common;
using Esp.Core.Extensions;
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
        private readonly IList<IInputNeuron> _inputNeurons;
        private readonly IList<IOutputNeuron> _outputNeurons;
        private List<double> _fitnessHistory = new();

        public Esp(
            IList<IPopulation> populations,
            IList<IInputNeuron> inputNeurons, 
            IList<IOutputNeuron> outputNeurons)
        {
            _inputNeurons = inputNeurons;
            _populations = populations;
            _outputNeurons = outputNeurons;
        }

        public Guid GetId() => _id;

        public void Evaluate()
        {
            double bestFitness = 0;

            while (ShouldContinueTrials())
            {
                var randomNeuronsForHidden = _populations
                    .Select(population => population.GetRandomNeuron())
                    .ToList();

                CheckUniqueness(randomNeuronsForHidden);

                var inputLayer = new InputLayer(_inputNeurons);
                var hiddenLayer = new HiddenLayer(randomNeuronsForHidden);
                var outputLayer = new OutputLayer(_outputNeurons);

                var network = new SimpleRecurrentNetwork(
                    inputLayer, 
                    new List<IHiddenLayer>() { hiddenLayer }, 
                    outputLayer);

                network.PushExpectedValues(new List<double>(){ 1, 1, 1 });

                network.PushInputValues(new List<double> { 1054, 54, 234 });

                var fitness = network.ApplyFitness();

                if (fitness > bestFitness)
                    bestFitness = fitness;

                network.ResetConnection();
            }

            _fitnessHistory.Add(bestFitness);
        }

        public void CheckStagnation()
        {
            // hardcode
            var numberOfGenerationsToCheck = 3;

            var IsStagnate = _fitnessHistory
                .TakeLast(numberOfGenerationsToCheck)
                .ToList()
                .IsSorted();

            if (!IsStagnate)
                return;

            foreach (var population in _populations)
                population.BurstMutation();
        }

        public void Recombine()
        {
            foreach(var population in _populations)
            {
                population.Recombine();
            }
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

        private bool ShouldContinueTrials() => _populations
            .SelectMany(population => population.HiddenNeurons)
            .Any(neuron => neuron.Trials < 10);
    }
}
