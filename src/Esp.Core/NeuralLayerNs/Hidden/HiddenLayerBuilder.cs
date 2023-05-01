using Esp.Core.NeuronNs.Hidden;

namespace Esp.Core.NeuralLayerNs.Hidden
{
    public class HiddenLayerBuilder : IHiddenLayerBuilder
    {
        public IHiddenLayer BuildHiddenLayer(IList<IHiddenNeuron> hiddenNeurons)
        {
            return new HiddenLayer(hiddenNeurons);
        }
    }
}
