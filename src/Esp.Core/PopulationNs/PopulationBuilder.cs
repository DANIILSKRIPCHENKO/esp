using Esp.Api;
using Esp.Core.Extensions;
using Esp.Core.NeuronNs.Hidden;

namespace Esp.Core.PopulationNs
{
    public class PopulationBuilder : IPopulationBuilder
    {
        private readonly IOptions _options;
        private readonly IHiddenNeuronBuilder _hiddenNeuronBuilder;

        public PopulationBuilder(
            IOptions options,
            IHiddenNeuronBuilder hiddenNeuronBuilder)
        {
            _options = options;
            _hiddenNeuronBuilder = hiddenNeuronBuilder;
        }

        public IList<IPopulation> BuildInitialPopulations()
        {
            return BuildInitialPopulations(
                _options.NumberOfPopulations, 
                _options.NumberOfNeuronsInPopulation);
        }

        public IPopulation BuildPopulation()
        {
            var neurons = _hiddenNeuronBuilder.BuildHiddenNeurons(_options.NumberOfNeuronsInPopulation);
            return new Population(neurons);
        }

        private IList<IPopulation> BuildInitialPopulations(
            int numberOfPopulations,
            int numberOfNeuronsInPopulation)
        {

            var initialNeurons = _hiddenNeuronBuilder
                .BuildHiddenNeurons(_options.NumberOfNeuronsInPopulation * _options.NumberOfPopulations);

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
