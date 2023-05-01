using Esp.Core.Extensions;
using Esp.Core.NeuronNs.Hidden;

namespace Esp.Core.PopulationNs
{
    public class PopulationBuilder : IPopulationBuilder
    {
        private readonly IList<IHiddenNeuron> _hiddenNeurons;

        public PopulationBuilder(IHiddenNeuronBuilder hiddenNeuronBuilder)
        {
            _hiddenNeurons = hiddenNeuronBuilder.BuildHiddenNeurons(2500);
        }

        public IList<IPopulation> BuildInitialPopulations()
        {
            return BuildInitialPopulations(5, 500);
        }

        private IList<IPopulation> BuildInitialPopulations(
            int numberOfPopulations,
            int numberOfNeuronsInPopulation)
        {
            var populations = new List<IPopulation>();

            var usedNeurons = new List<IHiddenNeuron>();

            for (int i = 0; i < numberOfPopulations; i++)
            {
                var randomNeurons = _hiddenNeurons
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
