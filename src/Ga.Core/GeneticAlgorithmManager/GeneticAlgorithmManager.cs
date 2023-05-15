using Ga.Core.EspNS;
using Ga.Core.Models;
using Ga.Core.Task;

namespace Ga.Core.GeneticAlgorithmManager
{
    /// <summary>
    /// Represents GeneticAlgorithmManager implementation
    /// </summary>
    public class GeneticAlgorithmManager : IGeneticAlgorithmManager
    {
        private readonly IGeneticAlgorithm _geneticAlgorithm;

        public GeneticAlgorithmManager(
            IGeneticAlgorithm geneticAlgorithm)
        {
            _geneticAlgorithm = geneticAlgorithm;
        }

        public TrainResult Train(ITask task, TrainingConfiguration trainingConfiguration)
        {
            var dateTimeLimit = DateTime.Now.Add(trainingConfiguration.TrainingTimeLimit);

            _geneticAlgorithm.SetDataset(task);

            var generation = 0;

            double fitness = 0;

            while (fitness < trainingConfiguration.TargetFitness
                   && generation < trainingConfiguration.GenerationsLimit
                   && DateTime.Now < dateTimeLimit)
            {
                fitness = _geneticAlgorithm.Evaluate();

                _geneticAlgorithm.CheckStagnation();

                _geneticAlgorithm.Recombine();
                generation++;
            }

            return _geneticAlgorithm.GetTrainResult();
        }
    }
}
