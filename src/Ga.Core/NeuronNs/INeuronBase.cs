using Ga.Core.Common;

namespace Ga.Core.NeuronNs
{
    /// <summary>
    /// Represents interface of neuron
    /// </summary>
    public interface INeuronBase : IId
    {
        /// <summary>
        /// Calculates and return output of neuron
        /// </summary>
        /// <returns></returns>
        public double CalculateOutput();
    }
}
