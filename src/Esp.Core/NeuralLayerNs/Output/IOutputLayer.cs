using Esp.Core.NeuronNs.Output;

namespace Esp.Core.NeuralLayerNs.Output
{
    /// <summary>
    /// Represents interface of output layer in Neural Network
    /// </summary>
    public interface IOutputLayer
    {
        /// <summary>
        /// Collection of output neurons
        /// </summary>
        public IList<IOutputNeuron> OutputNeurons { get; }

        /// <summary>
        /// Resets connections of neurons in output layer
        /// </summary>
        public void ResetConnections();
    }
}
