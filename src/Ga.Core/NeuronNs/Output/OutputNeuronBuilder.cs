using Ga.Core.ActivationFunction;
using Ga.Core.InputFunction;

namespace Ga.Core.NeuronNs.Output
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
            return BuildOutputNeurons(2);
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
