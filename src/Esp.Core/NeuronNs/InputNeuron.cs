using Esp.Core.ActivationFunction;
using Esp.Core.SynapseNs;

namespace Esp.Core.NeuronNs
{
    public class InputNeuron : IInputNeuron
    {
        private readonly Guid _id = Guid.NewGuid();
        private readonly IList<ISynapse> _outputs = new List<ISynapse>();
        private readonly IActivationFunction _activationFunction;
        private double _input;

        public InputNeuron(IActivationFunction activationFunction)
        {
            _activationFunction = activationFunction;
        }
        
        public IList<ISynapse> Outputs { get => _outputs;}

        // Hardcode output for inputs neurons
        public double CalculateOutput() => _activationFunction.CalculateOutput(_input);

        public Guid GetId() => _id;

        public void PushValueOnInput(double input)
        {
            _input = input;
        }
    }
}
