namespace Ga.Core.LossFunction;

public class BinaryCrossEntropy : ILossFunction
{
    public double CalculateError(IList<double> actual, IList<double> expected)
    {
        var probabilities = GetProbabilities(actual);

        var result = -(1 * Math.Log(probabilities.First()));

        return result;
    }

    private IEnumerable<double> GetProbabilities(IList<double> values) => values
        .Select(value =>
            Math.Exp(value) / values.Select(Math.Exp).Sum()).ToList();
}