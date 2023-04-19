using Esp.Core.PopulationNs;

namespace Esp.Core.NetworkNs
{
    public class Network : INetwork
    {
        private readonly List<IPopulation> _populations;

        public Network(List<IPopulation> populations)
        {
            _populations = populations;
        }

        public List<IPopulation> GetPopulations()
        {
            throw new NotImplementedException();
        }
    }
}
