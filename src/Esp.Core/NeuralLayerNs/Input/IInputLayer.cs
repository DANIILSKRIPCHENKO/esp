using Esp.Core.NeuronNs.Input;

namespace Esp.Core.NeuralLayerNs.Input
{
    /// <summary>
    /// Represents interface of input layer in Neural Network
    /// </summary>
    public interface IInputLayer
    {
        /// <summary>
        /// Collection of neurons of input layer
        /// </summary>
        public IList<IInputNeuron> InputNeurons { get; }

        /// <summary>
        /// Resets neuron connection in layer
        /// </summary>
        public void ResetConnections();
    }
}
