using Ga.Core.Common;
using Ga.Core.Models;
using Ga.Core.Task;

namespace Ga.Core.NetworkNs
{
    /// <summary>
    /// Represents interface for Neural Network
    /// </summary>
    public interface INeuralNetwork : IId
    {
        /// <summary>
        /// Calculates and applies fitness to neurons
        /// </summary>
        /// <returns></returns>
        public double ApplyFitness(double fitness);

        /// <summary>
        /// Reset connections of neurons
        /// </summary>
        public void ResetConnection();

        /// <summary>
        /// Pushes input values in Network
        /// </summary>
        /// <param name="inputs"></param>
        public void PushInputValues(IList<double> inputs);

        public void BindLayers();

        public IList<double> GetOutputs();

        public EvaluationResult EvaluateOnDataset(ITask task);
    }
}
