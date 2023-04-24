using Esp.Core.NeuronNs;

namespace Esp.Core.SynapseNs
{
    /// <summary>
    /// Represents ISynapse implementation
    /// </summary>
    public class Synapse : ISynapse
    {
        private readonly INeuronBase _fromNeuron;
        private readonly INeuronBase _toNeuron;
        private readonly double _weight;

        public Synapse(
            INeuronBase fromNeuron, 
            INeuronBase toNeuron,
            double weight)
        {
            _fromNeuron = fromNeuron;
            _toNeuron = toNeuron;
            _weight = weight;
        }

        public double GetOutput() => _weight * _fromNeuron.CalculateOutput();
    }
}
