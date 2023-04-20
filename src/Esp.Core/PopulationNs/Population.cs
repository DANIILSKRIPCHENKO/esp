using Esp.Core.NeuronNs;

namespace Esp.Core.PopulationNs
{
    public class Population : IPopulation
    {
        private readonly Guid _id = Guid.NewGuid();

        private readonly IEnumerable<INeuron> _neurons;

        public Population(IEnumerable<INeuron> neurons)
        {
            _neurons = neurons;
        }

        public Guid GetId() => _id;
    }
}
