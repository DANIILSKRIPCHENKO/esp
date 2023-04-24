
using Esp.Core.ActivationFunction;
using Esp.Core.Extensions;
using Esp.Core.GenotypeNs;
using Esp.Core.InputFunction;
using Esp.Core.NeuronNs;
using Esp.Core.PopulationNs;

namespace Esp.Core.Builder
{
    public static class EspBuilder
    {
        #region Public methods

        /// <summary>
        /// Builds new Esp instance based on input parameters
        /// </summary>
        /// <param name="numberOfHiddenNeurons"></param>
        /// <param name="numberOfNeuronsInPopulation"></param>
        /// <returns></returns>
        public static EspNS.Esp Build(
            int numberOfHiddenNeurons, 
            int numberOfNeuronsInPopulation)
        {
            var numberOfPopulations = numberOfHiddenNeurons;

            var initialNumerOfNeurons = numberOfNeuronsInPopulation * numberOfPopulations;
            
            var neurons = BuildInitialNeurons(initialNumerOfNeurons, numberOfHiddenNeurons);

            var populations = BuildInitialPopulations(
                numberOfPopulations, 
                numberOfNeuronsInPopulation,
                neurons.Cast<IHiddenNeuron>().ToList());

            var inputNeurons = BuildInputNeurons(numberOfHiddenNeurons);

            var outputNeurons = BuildOutputNeurons(numberOfHiddenNeurons);

            return new EspNS.Esp(
                populations.Cast<IPopulation>().ToList(),
                inputNeurons.Cast<IInputNeuron>().ToList(),
                outputNeurons.Cast<IOutputNeuron>().ToList());
        }

        #endregion Public methods

        #region Private methods

        private static List<HiddenNeuron> BuildInitialNeurons(
            int numberOfNeurons, 
            int numberOfHiddenNeurons)
        {
            var neurons = new List<HiddenNeuron>();

            for (int i = 0; i < numberOfNeurons; i++)
            {
                neurons.Add(new HiddenNeuron(
                    new SigmoidActivationFunction(0.7), 
                    new WeightedSumFunction(),
                    Genotype.CreateRandom(numberOfHiddenNeurons)));
            }

            return neurons;
        }

        private static List<Population> BuildInitialPopulations(
            int numberOfPopulations, 
            int numberOfNeuronsInPopulation,
            IList<IHiddenNeuron> neurons)
        {
            var populations = new List<Population>();

            var usedNeurons = new List<IHiddenNeuron>();

            for (int i = 0; i < numberOfPopulations; i++)
            {
                var randomNeurons = neurons
                    .TakeRandomNotIn(usedNeurons, numberOfNeuronsInPopulation).Cast<IHiddenNeuron>()
                    .ToList();

                var population = new Population(randomNeurons);
                usedNeurons.AddRange(randomNeurons);

                populations.Add(population);
            }

            return populations;
        }

        private static List<InputNeuron> BuildInputNeurons(int numberOfInputNeurons)
        {
            var neurons = new List<InputNeuron>();

            for (int i = 0; i < numberOfInputNeurons; i++)
            {
                neurons.Add(new InputNeuron(new SigmoidActivationFunction(0.7)));
            }

            return neurons;
        }

        private static List<OutputNeuron> BuildOutputNeurons(int numberOfOutputNeurons)
        {
            var neurons = new List<OutputNeuron>();

            for (int i = 0; i < numberOfOutputNeurons; i++)
            {
                neurons.Add(new OutputNeuron(new SigmoidActivationFunction(0.7), new WeightedSumFunction()));
            }

            return neurons;
        }

        #endregion
    }
}
