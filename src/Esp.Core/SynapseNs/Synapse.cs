using Esp.Core.NeuronNs;

namespace Esp.Core.SynapseNs
{
    public class Synapse : ISynapse
    {
        private readonly INeuron _fromNeuron;
        private readonly INeuron _toNeuron;
        private readonly double _weight;

        public double Weight { get => _weight; }

        public Synapse(INeuron fromNeuron, INeuron toNeuron, double weight)
        {
            _fromNeuron = fromNeuron;
            _toNeuron = toNeuron;

            _weight = weight;
        }

        public Synapse(INeuron fromNeuron, INeuron toNeuron)
        {
            _fromNeuron = fromNeuron;
            _toNeuron = toNeuron;
        }

        public double GetOutput() => _fromNeuron.CalculateOutput();
    }
}
