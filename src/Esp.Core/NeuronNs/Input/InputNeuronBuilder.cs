using Esp.Core.ActivationFunction;

namespace Esp.Core.NeuronNs.Input
{
    public class InputNeuronBuilder : IInputNeuronBuilder
    {
        private readonly IActivationFunction _activationFunction;

        public InputNeuronBuilder(IActivationFunction activationFunction)
        {
            _activationFunction = activationFunction;
        }

        public IList<IInputNeuron> BuildInputNeurons()
        {
            return BuildInputNeurons(3);
        }

        private List<IInputNeuron> BuildInputNeurons(int numberOfInputNeurons)
        {
            var neurons = new List<IInputNeuron>();

            for (int i = 0; i < numberOfInputNeurons; i++)
            {
                neurons.Add(new InputNeuron(_activationFunction));
            }

            return neurons;
        }
    }
}
