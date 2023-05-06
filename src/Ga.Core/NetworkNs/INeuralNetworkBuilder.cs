using Ga.Core.NeuralLayerNs.Hidden;

namespace Ga.Core.NetworkNs
{
    public interface INeuralNetworkBuilder
    {
        public INeuralNetwork BuildNeuralNetwork(IList<IHiddenLayer> hiddenLayers);
    }
}
