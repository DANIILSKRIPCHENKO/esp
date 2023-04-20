using Esp.Core.Common;

namespace Esp.Core.NeuronNs
{
    public interface INeuron : IId
    {
        public double GetFitness();
    }
}
