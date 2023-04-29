using Esp.Core.NeuronNs.Hidden;

namespace Esp.Core.NeuralLayerNs.Hidden
{
    public interface IHiddenLayerBuilder
    {
        public IHiddenLayer BuildHiddenLayer(IList<IHiddenNeuron> hiddenNeurons);
    }
}
