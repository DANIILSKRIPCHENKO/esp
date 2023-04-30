using Esp.Core.Common;
using Esp.Core.Extensions;
using Esp.Core.NetworkNs;
using Esp.Core.NeuralLayerNs.Hidden;
using Esp.Core.PopulationNs;

namespace Esp.Core.EspNS
{
    /// <summary>
    /// Enforces sub population implementation of GA
    /// </summary>
    public class Esp : IGeneticAlgorithm
    {
        private readonly Guid _id = Guid.NewGuid();
        private readonly IList<IPopulation> _populations;

        private readonly List<double> _actualFitnessHistory = new();
        private readonly List<double> _bestFitnessHistory = new();
        private double _bestFitnessEver { get => _bestFitnessHistory.LastOrDefault(); }

        private readonly List<double> _burstMutationHistory = new();
        private readonly INeuralNetworkBuilder _neuralNetworkBuilder;
        private readonly IHiddenLayerBuilder _hiddenLayerBuilder;

        public Esp(
            INeuralNetworkBuilder neuralNetworkBuilder,
            IHiddenLayerBuilder hiddenLayerBuilder,
            IPopulationBuilder populationBuilder)
        {
            _neuralNetworkBuilder = neuralNetworkBuilder;
            _hiddenLayerBuilder = hiddenLayerBuilder;
            _populations = populationBuilder.BuildInitialPopulations();
        }

        #region IGeneticAlgorith implementation

        public Guid GetId() => _id;

        public double Evaluate()
        {
            double bestFitness = 0;

            while (ShouldContinueTrials())
            {
                var randomNeuronsForHidden = _populations
                    .Select(population => population.GetRandomNeuron())
                    .ToList();

                CheckUniqueness(randomNeuronsForHidden);

                var hiddenLayer = _hiddenLayerBuilder.BuildHiddenLayer(randomNeuronsForHidden);    

                var network = _neuralNetworkBuilder
                    .BuildNeuralNetwork(new List<IHiddenLayer>(){ hiddenLayer });

                network.PushExpectedValues(new List<double>() { 0.3, 0.7, 0.9 });

                network.PushInputValues(new List<double> { 130, 120, 100 });
                
                var fitness = network.ApplyFitness();

                if (fitness > bestFitness)
                    bestFitness = fitness;

                network.ResetConnection();
            }

            RecordFitness(bestFitness);

            return bestFitness;
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

                var bestFitness = _actualFitnessHistory
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

        #endregion


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

        //TODO logic error
        private bool ShouldBurstMutate(int numberOfGenerationsToCheck)
        {
            if (numberOfGenerationsToCheck > _bestFitnessHistory.Count)
                return false;

            var lastFitnesses = _bestFitnessHistory
                .TakeLast(numberOfGenerationsToCheck)
                .ToList();

            var IsStagnate = !lastFitnesses
                .Any(x => x != lastFitnesses.First());

            return IsStagnate;
        }

        //TODO hide neurons
        private bool ShouldContinueTrials() => _populations
            .SelectMany(population => population.HiddenNeurons)
            .Any(neuron => neuron.Trials < 10);

        //TODO another way to check it
        private bool ShouldAdaptNetwork()
        {
            if (_burstMutationHistory.Count < 2)
                return false;

            //if (_burstMutationHistory.Count > 2)
                //throw new Exception("Invalid burst mutation history");

            return !_burstMutationHistory.IsAscending();
        }

        private void AdaptNetworkStructure()
        {
            _burstMutationHistory.Clear();
        }

        private void RecordFitness(double fitness)
        {
            _actualFitnessHistory.Add(fitness);

            if (fitness > _bestFitnessEver)
            {
                _bestFitnessHistory.Add(fitness);
                return;
            }
            
            _bestFitnessHistory.Add(_bestFitnessEver);
        }

        #endregion
    }
}
