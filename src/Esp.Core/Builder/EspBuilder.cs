
using Esp.Core.ActivationFunction;
using Esp.Core.Extensions;
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
                neurons.Cast<INeuron>().ToList());

            var inputNeurons = BuildInputNeurons(numberOfHiddenNeurons);

            var outputNeurons = BuildOutputNeurons(numberOfHiddenNeurons);

            return new EspNS.Esp(
                populations.Cast<IPopulation>().ToList(),
                inputNeurons.Cast<INeuron>().ToList(),
                outputNeurons.Cast<INeuron>().ToList());
        }

        private static IList<Neuron> BuildInitialNeurons(int numberOfNeurons)
        {
            var neurons = new List<Neuron>();

            for (int i = 0; i < numberOfNeurons; i++)
            {
                neurons.Add(new Neuron(new SigmoidActivationFunction(0.7), new WeightedSumFunction()));
            }

            return neurons;
        }

        private static List<Population> BuildInitialPopulations(
            int numberOfPopulations, 
            int numberOfNeuronsInPopulation,
            IList<INeuron> neurons)
        {
            var populations = new List<Population>();

            var usedNeurons = new List<INeuron>();

            for (int i = 0; i < numberOfPopulations; i++)
            {
                var randomNeurons = neurons
                    .TakeRandomNotIn(usedNeurons, numberOfNeuronsInPopulation).Cast<INeuron>()
                    .ToList();

                var population = new Population(randomNeurons);
                usedNeurons.AddRange(randomNeurons);

                populations.Add(population);
            }

            return populations;
        }

        private static IList<Neuron> BuildInputNeurons(int numberOfInputNeurons)
        {
            var neurons = new List<Neuron>();

            for (int i = 0; i < numberOfInputNeurons; i++)
            {
                neurons.Add(new Neuron(new SigmoidActivationFunction(0.7), new WeightedSumFunction()));
            }

            return neurons;
        }

        private static IList<Neuron> BuildOutputNeurons(int numberOfOutputNeurons)
        {
            var neurons = new List<Neuron>();

            for (int i = 0; i < numberOfOutputNeurons; i++)
            {
                neurons.Add(new Neuron(new SigmoidActivationFunction(0.7), new WeightedSumFunction()));
            }

            return neurons;
        }
    }
}
