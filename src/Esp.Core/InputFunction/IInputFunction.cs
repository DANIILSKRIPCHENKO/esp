using Esp.Core.SynapseNs;

namespace Esp.Core.InputFunction
{
    public interface IInputFunction
    {
        public double CalculateInput(IList<ISynapse> inputs);
    }
}
