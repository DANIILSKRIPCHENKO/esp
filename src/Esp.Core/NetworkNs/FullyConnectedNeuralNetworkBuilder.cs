using Esp.Core.NeuralLayerNs.Hidden;
using Esp.Core.NeuralLayerNs.Input;
using Esp.Core.NeuralLayerNs.Output;

namespace Esp.Core.NetworkNs
{
    public class FullyConnectedNeuralNetworkBuilder : INeuralNetworkBuilder
    {
        private readonly IInputLayer _inputLayer;
        private readonly IOutputLayer _outputLayer;

        public FullyConnectedNeuralNetworkBuilder(
            IInputLayer inputLayer, 
            IOutputLayer outputLayer)
        {
            _inputLayer = inputLayer;
            _outputLayer = outputLayer;
        }

        public INeuralNetwork BuildNeuralNetwork(IList<IHiddenLayer> hiddenLayers)
        {
            return new FullyConnectedNetwork(_inputLayer, hiddenLayers, _outputLayer);
        }
    }
}
