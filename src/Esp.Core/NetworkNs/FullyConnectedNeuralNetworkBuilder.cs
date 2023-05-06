using Ga.Core.LossFunction;
using Ga.Core.NeuralLayerNs.Hidden;
using Ga.Core.NeuralLayerNs.Input;
using Ga.Core.NeuralLayerNs.Output;

namespace Ga.Core.NetworkNs
{
    public class FullyConnectedNeuralNetworkBuilder : INeuralNetworkBuilder
    {
        private readonly IInputLayer _inputLayer;
        private readonly IOutputLayer _outputLayer;
        private readonly ILossFunction _lossFunction;

        public FullyConnectedNeuralNetworkBuilder(
            IInputLayer inputLayer,
            IOutputLayer outputLayer,
            ILossFunction lossFunction)
        {
            _inputLayer = inputLayer;
            _outputLayer = outputLayer;
            _lossFunction = lossFunction;
        }

        public INeuralNetwork BuildNeuralNetwork(IList<IHiddenLayer> hiddenLayers)
        {
            return new FullyConnectedNetwork(_inputLayer, hiddenLayers, _outputLayer, _lossFunction);
        }
    }
}
