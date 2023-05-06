using Ga.Core.NeuronNs.Hidden;

namespace Ga.Core.NeuralLayerNs.Hidden
{
    public class HiddenLayerBuilder : IHiddenLayerBuilder
    {
        public IHiddenLayer BuildHiddenLayer(IList<IHiddenNeuron> hiddenNeurons)
        {
            return new HiddenLayer(hiddenNeurons);
        }
    }
}
