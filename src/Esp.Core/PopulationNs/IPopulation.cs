using Esp.Core.Common;
using Esp.Core.NeuronNs;

namespace Esp.Core.PopulationNs
{
    public interface IPopulation : IId
    {
        public INeuron GetRandomNeuronNotIn(IList<INeuron> neurons);
    }
}
