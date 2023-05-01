using Esp.Core.ActivationFunction;
using Esp.Core.InputFunction;

namespace Esp.Core.NeuronNs.Output
{
    public class OutputNeuronBuilder : IOutputNeuronBuilder
    {
        private readonly IActivationFunction _activationFunction;

        public OutputNeuronBuilder(IActivationFunction activationFunction)
        {
            _activationFunction = activationFunction;
        }

        public IList<IOutputNeuron> BuildOutputNeurons()
        {
            return BuildOutputNeurons(3);
        }

        private IList<IOutputNeuron> BuildOutputNeurons(int numberOfOutputNeurons)
        {
            var neurons = new List<IOutputNeuron>();

            for (int i = 0; i < numberOfOutputNeurons; i++)
            {
                neurons.Add(new OutputNeuron(_activationFunction, new WeightedSumFunction()));
            }

            return neurons;
        }
    }
}
