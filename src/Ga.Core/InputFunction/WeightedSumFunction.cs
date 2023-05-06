using Ga.Core.SynapseNs;

namespace Ga.Core.InputFunction
{
    /// <summary>
    ///  WeightedSum implementation of IInputFunction interafce
    /// </summary>
    public class WeightedSumFunction : IInputFunction
    {
        public double CalculateInput(IList<ISynapse> inputs)
        {
            double result = 0;

            foreach (var input in inputs)
            {
                var value = input.GetOutput();
                result += value;
            }

            return result;
        }
    }
}
