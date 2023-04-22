using Esp.Core.NeuronNs;

namespace Esp.Core.NeuralLayerNs
{
    public interface IOutputLayer
    {
        public IList<IOutputNeuron> OutputNeurons { get; }
    }
}
