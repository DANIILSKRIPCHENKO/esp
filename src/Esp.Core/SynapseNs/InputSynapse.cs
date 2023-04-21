using Esp.Core.NeuronNs;

namespace Esp.Core.SynapseNs
{
    public class InputSynapse : ISynapse
    {
        private readonly INeuron _toNeuron;
        private readonly double _weight = 1;
        private readonly double _output;

        public double Weight { get => _weight; }

        public InputSynapse(INeuron toNeuron, double output)
        {
            _toNeuron = toNeuron;
            _output = output;
        }

        public double GetOutput() => _output;
    }
}
