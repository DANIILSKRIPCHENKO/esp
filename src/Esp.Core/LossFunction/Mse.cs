namespace Ga.Core.LossFunction
{
    public class Mse : ILossFunction
    {
        public double CalculateError(IList<double> actual, IList<double> expected)
        {
            if (actual.Count != expected.Count)
                throw new ArgumentException("Failed to calculate loss, dimensions must be the same");

            if (!actual.Any() || !expected.Any())
                throw new ArgumentException("Failed to calculate loss, arguments should not be empty");

            double sum = 0;

            for (var i = 0; i < actual.Count; i++)
                sum += Math.Pow(expected[i] - actual[i], 2);

            return sum / actual.Count;
        }
    }
}
