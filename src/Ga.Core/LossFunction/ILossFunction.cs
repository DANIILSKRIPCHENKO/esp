namespace Ga.Core.LossFunction
{
    public interface ILossFunction
    {
        public double CalculateError(IList<double> actual, IList<double> expected);
    }
}
