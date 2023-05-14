using Ga.Core.Common;
using Ga.Core.Models;
using Ga.Core.NetworkNs;
using Ga.Core.NeuralLayerNs.Hidden;
using Ga.Core.PopulationNs;
using Ga.Core.Task;

namespace Ga.Core.EspNS
{
    /// <summary>
    /// Enforces sub population implementation of GA
    /// </summary>
    public class Esp : IGeneticAlgorithm
    {
        private readonly Guid _id = Guid.NewGuid();
        private readonly IList<IPopulation> _populations;
        private IList<IPopulation> _clonedPopulations;

        private readonly List<double> _fitnessHistory = new();
        private readonly List<double> _lossHistory = new();
        private readonly List<double> _bestFitnessHistory = new();
        private readonly List<double> _accuracyHistory = new();
        private readonly List<int> _populationHistory = new();
        private INeuralNetwork _bestNetwork;

        private double _bestFitnessEver { get => _bestFitnessHistory.LastOrDefault(); }

        private int _burstMutationCounter = 0;

        private readonly INeuralNetworkBuilder _neuralNetworkBuilder;
        private readonly IHiddenLayerBuilder _hiddenLayerBuilder;
        private readonly IPopulationBuilder _populationBuilder;
        private ITask _task;

        public Esp(
            INeuralNetworkBuilder neuralNetworkBuilder,
            IHiddenLayerBuilder hiddenLayerBuilder,
            IPopulationBuilder populationBuilder)
        {
            _neuralNetworkBuilder = neuralNetworkBuilder;
            _hiddenLayerBuilder = hiddenLayerBuilder;
            _populationBuilder = populationBuilder;
            _populations = populationBuilder.BuildInitialPopulations();
            _clonedPopulations = _populations;
        }

        #region IGeneticAlgorith implementation

        public Guid GetId() => _id;

        public double Evaluate()
        {
            return EvaluateInternal();
        }

        public void CheckStagnation()
        {
            if (ShouldAdaptNetwork())
            {
                AdaptNetworkStructure();
                _burstMutationCounter = 0;

                return;
            }

            // hardcode number of generations to check
            if (!ShouldBurstMutate(3)) return;
            foreach (var population in _populations.Where(population => population.IsTurnedOff == false))
                population.BurstMutation();

            _burstMutationCounter++;
        }

        public void Recombine()
        {
            foreach (var population in _populations.Where(population => population.IsTurnedOff == false))
                population.Recombine();
        }

        public void SetDataset(ITask task)
        {
            _task = task;
        }

        public void ResetParameters()
        {
            _fitnessHistory.Clear();
            _bestFitnessHistory.Clear();
            _populationHistory.Clear();
            _burstMutationCounter = default;
        }

        public TrainResult GetTrainResult()
        {
            return new TrainResult(
                _bestNetwork,
                _fitnessHistory,
                _lossHistory,
                _accuracyHistory,
                _populationHistory,
                _bestFitnessHistory);
        }

        public INeuralNetwork GetBestNetwork() => _bestNetwork;

        #endregion

        #region Private methods

        private double EvaluateInternal()
        {
            ResetFitnesses(_populations);

            double bestFitness = 0;
            double bestAccuracy = 0;
            double bestLoss = int.MaxValue;
            INeuralNetwork bestNetwork = null;

            while (ShouldContinueTrials(_populations))
            {
                var randomNeuronsForHidden = _populations
                    .Select(population => population.GetRandomNeuron())
                    .ToList();

                CheckUniqueness(randomNeuronsForHidden);

                var hiddenLayer = _hiddenLayerBuilder.BuildHiddenLayer(randomNeuronsForHidden);

                var network = _neuralNetworkBuilder
                    .BuildNeuralNetwork(new List<IHiddenLayer>() { hiddenLayer });

                var evaluationResult = network.EvaluateOnDataset(_task);

                network.ApplyFitness(evaluationResult.Fitness);

                if (evaluationResult.Fitness > bestFitness)
                {
                    bestFitness = evaluationResult.Fitness;
                    bestNetwork = network;
                }

                if (evaluationResult.Accuracy > bestAccuracy)
                    bestAccuracy = evaluationResult.Accuracy;

                if (evaluationResult.Loss < bestLoss)
                    bestLoss = evaluationResult.Loss;

                network.ResetConnection();
            }

            RecordParameters(bestFitness, bestAccuracy, bestLoss, bestNetwork);

            return bestFitness;
        }

        private double EvaluateInternalReadOnly()
        {
            _clonedPopulations = new List<IPopulation>(_populations);
            ResetFitnesses(_clonedPopulations);

            double bestFitness = 0;

            while (ShouldContinueTrials(_clonedPopulations))
            {
                var randomNeuronsForHidden = _clonedPopulations
                    .Where(population => population.IsTurnedOff == false)
                    .Select(population => population.GetRandomNeuron())
                    .ToList();

                CheckUniqueness(randomNeuronsForHidden);

                var hiddenLayer = _hiddenLayerBuilder.BuildHiddenLayer(randomNeuronsForHidden);

                var network = _neuralNetworkBuilder
                    .BuildNeuralNetwork(new List<IHiddenLayer>() { hiddenLayer });

                var evaluationResult = network.EvaluateOnDataset(_task);

                network.ApplyFitness(evaluationResult.Fitness);

                if (evaluationResult.Fitness > bestFitness)
                {
                    bestFitness = evaluationResult.Fitness;
                }

                network.ResetConnection();
            }

            return bestFitness;
        }

        private static void ResetFitnesses(IEnumerable<IPopulation> populations)
        {
            foreach (var population in populations)
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
        private static bool ShouldContinueTrials(IEnumerable<IPopulation> populations) => populations
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
            if (_populations.Count < 2) return false;

            var fitnessToCompare = EvaluateInternalReadOnly();

            var populationsFitnessMappings = new Dictionary<IPopulation, double>();
            foreach (var population in _populations)
            {
                population.IsTurnedOff = true;

                var fitness = EvaluateInternalReadOnly();

                if (fitness >= fitnessToCompare)
                    populationsFitnessMappings.Add(population, fitness);

                population.IsTurnedOff = false;
            }

            if (!populationsFitnessMappings.Any())
                return false;

            if (populationsFitnessMappings.Count == _populations.Count)
            {
                populationsFitnessMappings = populationsFitnessMappings
                    .OrderByDescending(pf => pf.Value)
                    .Take(_populations.Count - 1)
                    .ToDictionary(x => x.Key, y => y.Value);
            }

            var populationsToDelete = populationsFitnessMappings
                .Select(pf => pf.Key);

            foreach (var population in populationsToDelete)
            {
                _populations.Remove(population);
            }

            return true;
        }

        private void RecordParameters(double fitness, double accuracy, double loss, INeuralNetwork neuralNetwork)
        {
            _populationHistory.Add(_populations.Count);

            _fitnessHistory.Add(fitness);
            _lossHistory.Add(loss);
            _accuracyHistory.Add(accuracy);

            if (fitness > _bestFitnessEver)
            {
                _bestFitnessHistory.Add(fitness);
                _bestNetwork = neuralNetwork;
                return;
            }

            _bestFitnessHistory.Add(_bestFitnessEver);
        }

        #endregion
    }
}
