using Esp.Core.Activation;
using Esp.Core.InputFunction;
using Esp.Core.NeuronNs;
using Esp.Core.PopulationNs;

namespace Esp.Core.Builder
{
    public static class EspBuilder
    {
        public static EspNS.Esp Build(
            int numberOfHiddenNeurons, 
            int numberOfNeuronsInPopulation)
        {
            var numberOfPopulations = numberOfHiddenNeurons;

            var initialNumerOfNeurons = numberOfNeuronsInPopulation * numberOfPopulations;
            var neurons = BuildInitialNeurons(initialNumerOfNeurons);

            var populations = BuildInitialPopulations(
                numberOfPopulations, 
                numberOfNeuronsInPopulation,
                neurons);

            return new EspNS.Esp(populations);
        }

        private static IEnumerable<Neuron> BuildInitialNeurons(int numberOfNeurons)
        {
            var neurons = new List<Neuron>();

            for (int i = 0; i < numberOfNeurons; i++)
            {
                neurons.Add(new Neuron(new SigmoidActivationFunction(0.7), new WeightedSumFunction()));
            }

            return neurons;
        }

        private static IEnumerable<Population> BuildInitialPopulations(
            int numberOfPopulations, 
            int numberOfNeuronsInPopulation,
            IEnumerable<Neuron> neurons)
        {
            var populations = new List<Population>();

            for (int i = 0; i < numberOfPopulations; i++)
            {
                var population = new Population(neurons
                    .OrderBy(x => Guid.NewGuid())
                    .Take(numberOfNeuronsInPopulation));

                populations.Add(population);
            }

            return populations;
        }
    }
}
