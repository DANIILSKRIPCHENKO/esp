using Esp.Core.NeuralLayerNs.Hidden;

namespace Esp.Core.NetworkNs
{
    public interface INeuralNetworkBuilder
    {
        public INeuralNetwork BuildNeuralNetwork(IList<IHiddenLayer> hiddenLayers);
    }
}
