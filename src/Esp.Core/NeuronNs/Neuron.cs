using Esp.Core.Activation;
using Esp.Core.InputFunction;
using Esp.Core.Synapse;

namespace Esp.Core.NeuronNs
{
    public class Neuron : INeuron
    {
        private readonly Guid _id = Guid.NewGuid();
        private readonly IActivationFunction _activationFunction;
        private readonly IInputFunction _inputFunction;
        private int _trials = 0;
        private double _fitness = 0;

        public IEnumerable<ISynapse> Inputs { get; set; }
        public IEnumerable<ISynapse> Outputs { get; set; }

        public Neuron(IActivationFunction activationFunction, IInputFunction inputFunction)
        {
            _activationFunction = activationFunction;
            _inputFunction = inputFunction;

            Inputs = new List<ISynapse>();
            Outputs = new List<ISynapse>();
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

        public double CalculateOutput()
        {
            throw new NotImplementedException();
        }
    }
}
