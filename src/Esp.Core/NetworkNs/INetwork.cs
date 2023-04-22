using Esp.Core.Common;

namespace Esp.Core.NetworkNs
{
    public interface INetwork : IId
    {
        public double ApplyFitness();

        public void ResetConnection();
    }
}
