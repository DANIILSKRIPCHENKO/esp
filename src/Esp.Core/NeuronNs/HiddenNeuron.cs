using Esp.Core.ActivationFunction;
using Esp.Core.Extensions;
using Esp.Core.GenotypeNs;
using Esp.Core.InputFunction;
using Esp.Core.SynapseNs;

namespace Esp.Core.NeuronNs
{
    public class HiddenNeuron : IHiddenNeuron
    {
        private readonly Guid _id = Guid.NewGuid();
        private readonly IActivationFunction _activationFunction;
        private readonly IInputFunction _inputFunction;
        private readonly IGenotype _genotype;

        private List<ISynapse> _inputs = new();
        private List<ISynapse> _outputs = new();
        private int _trials = 0;
        private double _fitness = 0;


        public IList<ISynapse> Inputs
        {
            get => _inputs;
            set { _inputs = value.ToList(); }
        }

        public IList<ISynapse> Outputs
        {
            get => _outputs;
            set { _outputs = value.ToList(); }
        }

        public int Trials { get => _trials; }

        public HiddenNeuron(
            IActivationFunction activationFunction, 
            IInputFunction inputFunction,
            IGenotype genotype)
        {
            _activationFunction = activationFunction;
            _inputFunction = inputFunction;
            _genotype = genotype;
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
            var input = _inputFunction.CalculateInput(_inputs);

            var output = _activationFunction.CalculateOutput(input);

            return output;
        }

        public void AddOutputNeuron(IOutputNeuron outputNeuron)
        {
            var index = _outputs.NextIndex();

            var synapse = new Synapse(
                this, 
                outputNeuron, 
                _genotype.OutputWeights[index]);

            _outputs.Add(synapse);
            outputNeuron.Inputs.Add(synapse);
        }

        public void AddInputNeuron(IInputNeuron inputNeuron)
        {  
            var index = _inputs.NextIndex();

            var synapse = new Synapse(
                inputNeuron, 
                this, 
                _genotype.InputWeights[index]);
            
            _inputs.Add(synapse);
            inputNeuron.Outputs.Add(synapse);
        }

        public void ResetConnection()
        {
            _inputs.Clear();
            _outputs.Clear();
        }
    }
}
