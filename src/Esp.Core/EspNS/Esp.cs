using Esp.Core.NetworkNs;
using Esp.Core.PopulationNs;

namespace Esp.Core.EspNS
{
    public class Esp : IEsp
    {
        private readonly Guid _id = Guid.NewGuid();
        private readonly INetwork _network;
        private readonly IEnumerable<IPopulation> _populations;

        public Esp(INetwork network, IEnumerable<IPopulation> populations)
        {
            _network = network;
            _populations = populations;
        }

        public Guid GetId() => _id;
    }
}
