using Esp.Core.NeuronNs;

namespace Esp.Core.PopulationNs
{
    public class Population : IPopulation
    {
        private readonly List<INeuron> _neurons;

        public Population(List<INeuron> neurons)
        {
            _neurons = neurons;
        }
    }
}
