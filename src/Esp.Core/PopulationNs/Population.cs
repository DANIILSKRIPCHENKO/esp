using Esp.Core.Extensions;
using Esp.Core.NeuronNs;

namespace Esp.Core.PopulationNs
{
    public class Population : IPopulation
    {
        private readonly Guid _id = Guid.NewGuid();

        private readonly IList<IHiddenNeuron> _neurons;

        public Population(IList<IHiddenNeuron> neurons)
        {
            _neurons = neurons;
        }

        public Guid GetId() => _id;

        public IList<IHiddenNeuron> GetNeurons() => _neurons;

        public IHiddenNeuron GetRandomNeuron() => _neurons.FirstRandom();
    }
}
