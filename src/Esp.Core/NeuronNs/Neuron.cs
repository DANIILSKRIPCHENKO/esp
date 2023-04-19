namespace Esp.Core.NeuronNs
{
    public class Neuron : INeuron
    {
        private readonly Guid _id = Guid.NewGuid();

        private readonly int _trials = 0;

        private readonly double _fitness = 0;

        public Neuron()
        {

        }

        public Guid GetId() => _id;

        public double GetFitness() =>
            _trials == 0
            ? _fitness
            : _fitness / _trials;
    }
}
