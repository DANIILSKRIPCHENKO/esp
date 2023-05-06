using Ga.Core.SynapseNs;

namespace Ga.Core.NeuronNs.Output
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
