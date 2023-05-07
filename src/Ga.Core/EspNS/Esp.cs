using Ga.Core.Common;
using Ga.Core.NetworkNs;
using Ga.Core.NeuralLayerNs.Hidden;
using Ga.Core.PopulationNs;

namespace Ga.Core.EspNS
{
    /// <summary>
    /// Enforces sub population implementation of GA
    /// </summary>
    public class Esp : IGeneticAlgorithm, IReportableGeneticAlgorithm
    {
        private readonly Guid _id = Guid.NewGuid();
        private readonly IList<IPopulation> _populations;

        private readonly List<double> _actualFitnessHistory = new();
        private readonly List<double> _bestFitnessHistory = new();
        private readonly List<int> _populationHistory = new();

        private double _bestFitnessEver { get => _bestFitnessHistory.LastOrDefault(); }

        private int _burstMutationCounter = 0;

        private readonly INeuralNetworkBuilder _neuralNetworkBuilder;
        private readonly IHiddenLayerBuilder _hiddenLayerBuilder;
        private readonly IPopulationBuilder _populationBuilder;
        private IList<double> _inputs;
        private IList<double> _expectedOutputs;

        public Esp(
            INeuralNetworkBuilder neuralNetworkBuilder,
            IHiddenLayerBuilder hiddenLayerBuilder,
            IPopulationBuilder populationBuilder)
        {
            _neuralNetworkBuilder = neuralNetworkBuilder;
            _hiddenLayerBuilder = hiddenLayerBuilder;
            _populationBuilder = populationBuilder;
            _populations = populationBuilder.BuildInitialPopulations();
        }

        #region IGeneticAlgorith implementation

        public Guid GetId() => _id;

        public double Evaluate()
        {
            return Evaluate(isTracking: true);
        }

        public void CheckStagnation()
        {
            if (ShouldAdaptNetwork())
            {
                AdaptNetworkStructure();
                _burstMutationCounter = 0;

                return;
            }

            // hardcode  number of generations to check
            if (ShouldBurstMutate(3))
            {
                foreach (var population in _populations.Where(population => population.IsTurnedOff == false))
                    population.BurstMutation();

                _burstMutationCounter++;
            }
        }

        public void Recombine()
        {
            foreach (var population in _populations.Where(population => population.IsTurnedOff == false))
                population.Recombine();
        }

        public void SetInputs(IList<double> inputs)
        {
            _inputs = inputs;
        }

        public void SetOutputs(IList<double> outputs)
        {
            _expectedOutputs = outputs;
        }

        public void ResetParameters()
        {
            _actualFitnessHistory.Clear();
            _bestFitnessHistory.Clear();
            _populationHistory.Clear();
            _burstMutationCounter = default;
            //ResetFitnesses();
        }

        #endregion

        #region IReportableGeneticAlgorithm implementation

        public IList<double> GetActualFitnessHistory() => _actualFitnessHistory;

        public IList<double> GetBestFitnessHistory() => _bestFitnessHistory;

        public IList<int> GetPopulationHistory() => _populationHistory;

        #endregion

        #region Private methods

        private double Evaluate(bool isTracking)
        {
            ResetFitnesses();

            double bestFitness = 0;

            while (ShouldContinueTrials())
            {
                var randomNeuronsForHidden = _populations
                    .Where(population => population.IsTurnedOff == false)
                    .Select(population => population.GetRandomNeuron())
                    .ToList();

                CheckUniqueness(randomNeuronsForHidden);

                var hiddenLayer = _hiddenLayerBuilder.BuildHiddenLayer(randomNeuronsForHidden);

                var network = _neuralNetworkBuilder
                    .BuildNeuralNetwork(new List<IHiddenLayer>() { hiddenLayer });

                network.PushExpectedValues(_expectedOutputs);

                network.PushInputValues(_inputs);

                var fitness = network.GetFitness();

                network.ApplyFitness();

                if (fitness > bestFitness)
                    bestFitness = fitness;

                network.ResetConnection();
            }

            if (isTracking)
                RecordParameters(bestFitness);

            return bestFitness;
        }

        private void ResetFitnesses()
        {
            foreach (var population in _populations)
                population.ResetFitnesses();
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
            .Where(population => population.IsTurnedOff == false)
            .SelectMany(population => population.HiddenNeurons)
            .Any(neuron => neuron.Trials < 10);


        private bool ShouldAdaptNetwork()
        {
            if (_burstMutationCounter < 2)
                return false;

            var lastBestFitnesses = _bestFitnessHistory
                .TakeLast(_burstMutationCounter)
                .ToList();

            return !lastBestFitnesses.Any(x => x != lastBestFitnesses.First());
        }

        private void AdaptNetworkStructure()
        {
            var removedAny = RemoveUselessPopulations();

            if (removedAny)
                return;

            _populations.Add(_populationBuilder.BuildPopulation());
        }

        private bool RemoveUselessPopulations()
        {
            var bestFitnessEver = _bestFitnessEver;

            var populationsToRemove = new List<IPopulation>();

            foreach (var population in _populations)
            {
                population.IsTurnedOff = true;

                var fitness = Evaluate(isTracking: false);

                if (fitness > bestFitnessEver)
                    populationsToRemove.Add(population);

                population.IsTurnedOff = false;
            }

            if (!populationsToRemove.Any())
                return false;

            foreach (var populationToRemove in populationsToRemove)
                _populations.Remove(populationToRemove);

            return true;
        }

        private void RecordParameters(double fitness)
        {
            _populationHistory.Add(_populations.Count);

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
