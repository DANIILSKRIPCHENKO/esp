using Ga.Core.LossFunction;
using Ga.Core.NeuralLayerNs.Hidden;
using Ga.Core.NeuralLayerNs.Input;
using Ga.Core.NeuralLayerNs.Output;

namespace Ga.Core.NetworkNs
{
    public class FullyConnectedNeuralNetworkBuilder : INeuralNetworkBuilder
    {
        private readonly ILossFunction _lossFunction;

        public FullyConnectedNeuralNetworkBuilder(
            ILossFunction lossFunction)
        {
            _lossFunction = lossFunction;
        }

        public INeuralNetwork BuildNeuralNetwork(IInputLayer inputLayer, IList<IHiddenLayer> hiddenLayers, IOutputLayer outputLayer)
        {
            return new FullyConnectedNetwork(inputLayer, hiddenLayers, outputLayer, _lossFunction);
        }
    }
}
