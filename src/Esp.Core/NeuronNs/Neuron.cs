using Esp.Core.ActivationFunction;
using Esp.Core.InputFunction;
using Esp.Core.SynapseNs;

namespace Esp.Core.NeuronNs
{
    public class Neuron : INeuron
    {
        private readonly Guid _id = Guid.NewGuid();
        private readonly IActivationFunction _activationFunction;
        private readonly IInputFunction _inputFunction;
        private List<ISynapse> _inputs = new List<ISynapse>();
        private List<ISynapse> _outputs = new List<ISynapse>();
        private int _trials = 0;
        private double _fitness = 0;


        public IEnumerable<ISynapse> Inputs
        {
            get => _inputs;
            set { _inputs = value.ToList(); }
        }

        public IEnumerable<ISynapse> Outputs
        {
            get => _outputs;
            set { _outputs = value.ToList(); }
        }

        public Neuron(IActivationFunction activationFunction, IInputFunction inputFunction)
        {
            _activationFunction = activationFunction;
            _inputFunction = inputFunction;
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

        public void AddInputNeuron(INeuron inputNeuron)
        {
            var synapse = new Synapse(inputNeuron, this);
            _inputs.Add(synapse);
            inputNeuron.Outputs.ToList().Add(synapse);
        }

        public double CalculateOutput()
        {
            throw new NotImplementedException();
        }

        public void PushValueOnInput(double inputValue)
        {
            var inputSynapse = _inputs.First() as InputSynapse;

            if (inputSynapse == null)
                throw new ArgumentNullException();

            inputSynapse!.SetOutput(inputValue);
        }
    }
}
