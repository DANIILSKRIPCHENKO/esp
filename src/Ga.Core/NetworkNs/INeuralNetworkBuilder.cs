using Ga.Core.NeuralLayerNs.Hidden;
using Ga.Core.NeuralLayerNs.Input;
using Ga.Core.NeuralLayerNs.Output;

namespace Ga.Core.NetworkNs
{
    public interface INeuralNetworkBuilder
    {
        public INeuralNetwork BuildNeuralNetwork(IInputLayer inputLayer, IList<IHiddenLayer> hiddenLayers, IOutputLayer outputLayer);
    }
}
