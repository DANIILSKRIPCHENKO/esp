using Esp.Core.NeuronNs;

namespace Esp.Core.SynapseNs
{
    public class InputSynapse : ISynapse, IInputSunapse
    {
        private readonly INeuron _toNeuron;
        private readonly double _weight = 1;
        private double _output;

        public double Weight { get => _weight; }

        public InputSynapse(INeuron toNeuron, double output)
        {
            _toNeuron = toNeuron;
            _output = output;
        }

        public double GetOutput() => _output;

        public void SetOutput(double valueToSet)
        {
            _output = valueToSet;
        }
    }
}
