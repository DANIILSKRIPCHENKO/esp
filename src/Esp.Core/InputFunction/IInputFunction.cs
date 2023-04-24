using Esp.Core.SynapseNs;

namespace Esp.Core.InputFunction
{

    /// <summary>
    /// Represents interface for input function of a Neuron
    /// </summary>
    public interface IInputFunction
    {
        /// <summary>
        /// Calculates output based on inputs
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public double CalculateInput(IList<ISynapse> inputs);
    }
}
