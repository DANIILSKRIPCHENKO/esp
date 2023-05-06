namespace Ga.Core.ActivationFunction
{
    /// <summary>
    /// Represents interface for activation function of Neuron
    /// </summary>
    public interface IActivationFunction
    {
        public double CalculateOutput(double input);
    }
}
