using Ga.Core.ConfigurationNs;
using Ga.Core.Extensions;
using Ga.Core.NeuronNs.Hidden;

namespace Ga.Core.PopulationNs
{
    public class PopulationBuilder : IPopulationBuilder
    {
        private readonly IGeneticAlgorithmConfiguration _configuration;
        private readonly IHiddenNeuronBuilder _hiddenNeuronBuilder;

        public PopulationBuilder(
            IGeneticAlgorithmConfiguration configuration,
            IHiddenNeuronBuilder hiddenNeuronBuilder)
        {
            _configuration = configuration;
            _hiddenNeuronBuilder = hiddenNeuronBuilder;
        }

        public IList<IPopulation> BuildInitialPopulations()
        {
            return BuildInitialPopulations(
                _configuration.NumberOfPopulations,
                _configuration.NumberOfNeuronsInPopulation);
        }

        public IPopulation BuildPopulation()
        {
            var neurons = _hiddenNeuronBuilder.BuildHiddenNeurons(_configuration.NumberOfNeuronsInPopulation);
            return new Population(neurons);
        }

        private IList<IPopulation> BuildInitialPopulations(
            int numberOfPopulations,
            int numberOfNeuronsInPopulation)
        {

            var initialNeurons = _hiddenNeuronBuilder
                .BuildHiddenNeurons(_configuration.NumberOfNeuronsInPopulation * _configuration.NumberOfPopulations);

            var populations = new List<IPopulation>();

            var usedNeurons = new List<IHiddenNeuron>();

            for (int i = 0; i < numberOfPopulations; i++)
            {
                var randomNeurons = initialNeurons
                    .TakeRandomNotIn(usedNeurons, numberOfNeuronsInPopulation)
                    .ToList();

                var population = new Population(randomNeurons);
                usedNeurons.AddRange(randomNeurons);

                populations.Add(population);
            }

            return populations;
        }
    }
}
