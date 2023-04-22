using Esp.Core.Extensions;
using Esp.Core.NeuronNs;

namespace Esp.Core.PopulationNs
{
    public class Population : IPopulation
    {
        private readonly Guid _id = Guid.NewGuid();

        private readonly IList<INeuron> _neurons;

        public Population(IList<INeuron> neurons)
        {
            _neurons = neurons;
        }

        public Guid GetId() => _id;

        public IList<INeuron> GetNeurons() => _neurons;

        public INeuron GetRandomNeuronNotIn(IList<INeuron> neurons) => 
            _neurons.FirstRandomNotIn(neurons);
    }
}
