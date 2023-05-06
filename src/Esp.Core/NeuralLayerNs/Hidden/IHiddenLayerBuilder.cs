using Ga.Core.NeuronNs.Hidden;

namespace Ga.Core.NeuralLayerNs.Hidden
{
    public interface IHiddenLayerBuilder
    {
        public IHiddenLayer BuildHiddenLayer(IList<IHiddenNeuron> hiddenNeurons);
    }
}
