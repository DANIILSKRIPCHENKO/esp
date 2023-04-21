using Esp.Core.Synapse;

namespace Esp.Core.InputFunction
{
    public interface IInputFunction
    {
        public double CalculateInput(IEnumerable<ISynapse> inputs);
    }
}
