using Ga.Core.Models;
using Ga.Core.Task;

namespace Ga.Core.GeneticAlgorithmManager
{
    /// <summary>
    /// Represents interface for entities with Execute method
    /// </summary>
    public interface IGeneticAlgorithmManager
    {
        /// <summary>
        /// Runs execution
        /// </summary>
        public TrainResult Train(ITask task, TrainingConfiguration trainingConfiguration);
    }
}
