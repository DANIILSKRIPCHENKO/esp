using Ga.Core.Common;
using Ga.Core.Models;
using Ga.Core.Task;

namespace Ga.Core.EspNS
{
    /// <summary>
    /// Represents interface of genetic algorithm (GA)
    /// </summary>
    public interface IGeneticAlgorithm : IId
    {
        /// <summary>
        /// Starts evaluation process of an GA
        /// </summary>
        public double Evaluate();

        /// <summary>
        /// Starts check stagnation process of an GA
        /// </summary>
        public void CheckStagnation();

        /// <summary>
        /// Starts recombination process of GA
        /// </summary>
        public void Recombine();

        public void SetDataset(ITask task);

        public void ResetParameters();

        public TrainResult GetTrainResult();
    }
}
