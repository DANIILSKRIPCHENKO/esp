using Esp.Core.Common;

namespace Esp.Core.NetworkNs
{
    public interface INetwork : IId
    {
        public IEnumerable<double> GetOutput();
    }
}
