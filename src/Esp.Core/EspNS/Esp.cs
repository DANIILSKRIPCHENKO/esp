using Esp.Core.PopulationNs;

namespace Esp.Core.EspNS
{
    public class Esp : IEsp
    {
        private readonly Guid _id = Guid.NewGuid();
        private readonly IEnumerable<IPopulation> _populations;

        public Esp(IEnumerable<IPopulation> populations)
        {
            _populations = populations;
        }

        public Guid GetId() => _id;

        public void Run()
        {
            var randomNeurons = _populations.Select(x => x.GetRandomNeuron());
        }
    }
}
