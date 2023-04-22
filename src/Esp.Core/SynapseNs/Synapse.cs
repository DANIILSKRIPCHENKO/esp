using Esp.Core.NeuronNs;

namespace Esp.Core.SynapseNs
{
    public class Synapse : ISynapse
    {
        private readonly INeuronBase _fromNeuron;
        private readonly INeuronBase _toNeuron;
        private readonly double _weight;

        public double Weight { get => _weight; }

        public Synapse(INeuronBase fromNeuron, INeuronBase toNeuron)
        {
            _fromNeuron = fromNeuron;
            _toNeuron = toNeuron;
        }

        public double GetOutput() => _fromNeuron.CalculateOutput();
    }
}
