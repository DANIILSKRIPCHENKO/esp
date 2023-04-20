namespace Esp.Core.NeuronNs
{
    public class Neuron : INeuron
    {
        private readonly Guid _id = Guid.NewGuid();

        private int _trials = 0;

        private double _fitness = 0;

        public Neuron()
        {

        }

        public Guid GetId() => _id;

        public double GetFitness() =>
            _trials == 0
            ? _fitness
            : _fitness / _trials;

        public void AddFitness(double fit)
        {
            _fitness += fit;
            _trials++;
        }
    }
}
