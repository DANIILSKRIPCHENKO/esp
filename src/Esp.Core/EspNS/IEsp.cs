using Esp.Core.Common;

namespace Esp.Core.EspNS
{
    public interface IEsp : IId
    {
        public void Evaluate();

        public void CheckStagnation();

        public void Recombine();
    }
}
