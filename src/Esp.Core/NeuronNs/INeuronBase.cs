using Esp.Core.Common;

namespace Esp.Core.NeuronNs
{
    public interface INeuronBase : IId
    {
        public double CalculateOutput();
    }
}
