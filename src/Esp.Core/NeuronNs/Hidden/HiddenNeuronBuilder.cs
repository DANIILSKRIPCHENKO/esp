using Esp.Api;
using Esp.Core.ActivationFunction;
using Esp.Core.GenotypeNs;
using Esp.Core.InputFunction;

namespace Esp.Core.NeuronNs.Hidden
{
    public class HiddenNeuronBuilder : IHiddenNeuronBuilder
    {
        private readonly IActivationFunction _activationFunction;
        private readonly IGenotypeBuilder _genotypeBuilder;

        public HiddenNeuronBuilder(
            IActivationFunction activationFunction,
            IGenotypeBuilder genotypeBuilder)
        {
            _activationFunction = activationFunction;
            _genotypeBuilder = genotypeBuilder;
        }

        public IList<IHiddenNeuron> BuildHiddenNeurons(int numberOfNeuronsToBuild)
        {
            return BuildInitialNeurons(numberOfNeuronsToBuild);
        }

        private IList<IHiddenNeuron> BuildInitialNeurons(
            int numberOfHiddenNeurons)
        {
            var neurons = new List<IHiddenNeuron>();

            for (int i = 0; i < numberOfHiddenNeurons; i++)
            {
                neurons.Add(new HiddenNeuron(
                    _activationFunction,
                    new WeightedSumFunction(),
                    _genotypeBuilder.BuildGenotype(3, 3)));
            }

            return neurons;
        }
    }
}
