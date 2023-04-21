using Esp.Core.SynapseNs;

namespace Esp.Core.InputFunction
{
    public class WeightedSumFunction : IInputFunction
    {
        public double CalculateInput(IEnumerable<ISynapse> inputs) => 
            inputs
            .Select(x => x.Weight * x.GetOutput())
            .Sum();
    }
}
