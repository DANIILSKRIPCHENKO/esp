using Esp.Core.SynapseNs;

namespace Esp.Core.NeuronNs
{
    /// <summary>
    /// Represents interface of neuron in output layer
    /// </summary>
    public interface IOutputNeuron : INeuronBase
    {
        /// <summary>
        /// Input connecions of neuron
        /// </summary>
        public IList<ISynapse> Inputs { get; }
    }
}
