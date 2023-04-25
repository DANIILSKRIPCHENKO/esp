using Esp.Core.Common;
using Esp.Core.Extensions;
using Esp.Core.NetworkNs;
using Esp.Core.NeuralLayerNs;
using Esp.Core.NeuronNs;
using Esp.Core.PopulationNs;

namespace Esp.Core.EspNS
{
    /// <summary>
    /// Enforces sub population implementation of GA
    /// </summary>
    public class Esp : IEsp
    {
        private readonly Guid _id = Guid.NewGuid();
        private readonly IList<IPopulation> _populations;
        private readonly IList<IInputNeuron> _inputNeurons;
        private readonly IList<IOutputNeuron> _outputNeurons;
        private List<double> _fitnessHistory = new();
        private List<double> _burstMutationHistory = new();

        public Esp(
            IList<IPopulation> populations,
            IList<IInputNeuron> inputNeurons, 
            IList<IOutputNeuron> outputNeurons)
        {
            _inputNeurons = inputNeurons;
            _populations = populations;
            _outputNeurons = outputNeurons;
        }

        #region IEsp implementation

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

                var network = new FullyConnectedNetwork(
                    inputLayer, 
                    new List<IHiddenLayer>() { hiddenLayer }, 
                    outputLayer);

                network.PushExpectedValues(new List<double>(){ 1, 1, 0 });

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
            if (ShouldAdaptNetwork())
                AdaptNetworkStructure();

            // hardcode  number of generations to check
            if (ShouldBurstMutate(3))
            {
                foreach (var population in _populations)
                    population.BurstMutation();

                var bestFitness = _fitnessHistory
                    .OrderByDescending(fitness => fitness)
                    .First();

                _burstMutationHistory.Add(bestFitness);
            }
        }

        public void Recombine()
        {
            foreach(var population in _populations)
            {
                population.Recombine();
            }
        }

        #endregion IEsp implementation


        #region Private methods 
        private void CheckUniqueness(IEnumerable<IId> idCollection)
        {
            var duplicatedElements = idCollection
                .GroupBy(x => x.GetId())
                .Where(x => x.Count() > 1);

            if (!duplicatedElements.Any())
                return;

            throw new Exception("Duplicate elements found");
        }

        private bool ShouldBurstMutate(int numberOfGenerationsToCheck)
        {
            if (numberOfGenerationsToCheck > _fitnessHistory.Count)
                return false;

            var IsStagnate = !_fitnessHistory
                .TakeLast(numberOfGenerationsToCheck)
                .ToList()
                .IsAscending();

            return IsStagnate;
        }

        private bool ShouldContinueTrials() => _populations
            .SelectMany(population => population.HiddenNeurons)
            .Any(neuron => neuron.Trials < 10);

        private bool ShouldAdaptNetwork()
        {
            if (_burstMutationHistory.Count < 2)
                return false;

            if (_burstMutationHistory.Count > 2)
                throw new Exception("Invalid burst mutation history");

            return !_burstMutationHistory.IsAscending();
        }

        private void AdaptNetworkStructure()
        {
            _burstMutationHistory.Clear();
        }

        #endregion
    }
}
