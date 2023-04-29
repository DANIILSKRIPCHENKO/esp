namespace Esp.Core.ActivationFunction
{
    /// <summary>
    /// Represents sigmoid activation function
    /// </summary>
    public class SigmoidActivationFunction : IActivationFunction
    {
        private readonly double _coeficient;

        public SigmoidActivationFunction()
        {
            _coeficient = 0.7;
        }

        public double CalculateOutput(double input)
        {
            return 1 / (1 + Math.Exp(-input * _coeficient));
        }
    }
}
