using Esp.Core.Common;
using Esp.Core.Synapse;

namespace Esp.Core.NetworkNs
{
    public interface INetwork : IId
    {
        public IEnumerable<double> GetOutput();

    }
}
