using Esp.Core.SynapseNs;

namespace Esp.Core.NeuronNs
{
    public interface IOutputNeuron : INeuronBase
    {
        public IList<ISynapse> Inputs { get; }
    }
}
