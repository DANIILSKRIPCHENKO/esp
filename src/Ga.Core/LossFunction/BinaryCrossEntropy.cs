namespace Ga.Core.LossFunction;

public class BinaryCrossEntropy : ILossFunction
{
    public double CalculateError(IList<double> actual, IList<double> expected)
    {
        var probabilities = GetProbabilities(actual).ToList();

        var y = Convert.ToInt32(expected.First()) == 1 ? 1 : 0;

        var result = -(y * Math.Log(probabilities[0]) + (1-y)*Math.Log(1-probabilities[0]));

        return result;
    }

    private IEnumerable<double> GetProbabilities(IList<double> values) => values
        .Select(value =>
            Math.Exp(value) / values.Select(Math.Exp).Sum()).ToList();
}