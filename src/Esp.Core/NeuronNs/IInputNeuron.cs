using Esp.Core.SynapseNs;

namespace Esp.Core.NeuronNs
{
    public interface IInputNeuron : INeuronBase
    {
        public IList<ISynapse> Outputs { get;}

        public void PushValueOnInput(double value);
    }
}
