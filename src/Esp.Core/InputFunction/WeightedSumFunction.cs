using Esp.Core.SynapseNs;

namespace Esp.Core.InputFunction
{
    public class WeightedSumFunction : IInputFunction
    {
        public double CalculateInput(IList<ISynapse> inputs)
        {
            double result = 0;

            foreach(var input in inputs)
            {
                var value = input.Weight * input.GetOutput();
                result += value;
            }

            return result;
        }
    }
}
