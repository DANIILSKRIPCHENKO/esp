using Esp.Core.NeuronNs;

namespace Esp.Core.NeuralLayerNs
{
    public interface IHiddenLayer
    {
        public void ConnectInput(IInputLayer inputLayer);

        public void ConnectOutput(IOutputLayer outputLayer);

        public IList<IHiddenNeuron> HiddenNeurons { get; }

        public void ResetConnections();
    }
}
