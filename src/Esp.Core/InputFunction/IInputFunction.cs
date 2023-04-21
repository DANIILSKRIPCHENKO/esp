using Esp.Core.SynapseNs;

namespace Esp.Core.InputFunction
{
    public interface IInputFunction
    {
        public double CalculateInput(IEnumerable<ISynapse> inputs);
    }
}
