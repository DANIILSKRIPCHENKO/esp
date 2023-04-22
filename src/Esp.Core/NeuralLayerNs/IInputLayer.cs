using Esp.Core.NeuronNs;

namespace Esp.Core.NeuralLayerNs
{
    public interface IInputLayer
    {
        public IList<IInputNeuron> InputNeurons { get; }
    }
}
