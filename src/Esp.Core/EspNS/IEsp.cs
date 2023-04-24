using Esp.Core.Common;

namespace Esp.Core.EspNS
{
    /// <summary>
    /// Represents interface of genetic algorithm (GA)
    /// </summary>
    public interface IEsp : IId
    {
        /// <summary>
        /// Starts evaluation process of an GA
        /// </summary>
        public void Evaluate();

        /// <summary>
        /// Starts check stagnation process of an GA
        /// </summary>
        public void CheckStagnation();

        /// <summary>
        /// Starts recombination process of GA
        /// </summary>
        public void Recombine();
    }
}
