namespace Ga.Core.LossFunction;

public class PseudoAccuracy : ILossFunction
{
    public double CalculateError(IList<double> actual, IList<double> expected)
    {
        var probabilities = GetProbabilities(actual).ToList();

        var formattedActual = probabilities.Select(Convert.ToInt32).ToList();

        var isEqual = formattedActual.SequenceEqual(expected.Select(Convert.ToInt32));

        return isEqual ? 1 : 100;
    }

    private IEnumerable<double> GetProbabilities(IList<double> values) => values
        .Select(value =>
            Math.Exp(value) / values.Select(Math.Exp).Sum()).ToList();
}