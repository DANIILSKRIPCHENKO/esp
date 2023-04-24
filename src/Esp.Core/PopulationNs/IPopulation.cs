using Esp.Core.Common;
using Esp.Core.NeuronNs;

namespace Esp.Core.PopulationNs
{
    /// <summary>
    /// Represents interface for interaction with population
    /// </summary>
    public interface IPopulation : IId
    {
        /// <summary>
        /// Returns random neuron of population
        /// </summary>
        /// <returns></returns>
        public IHiddenNeuron GetRandomNeuron();

        /// <summary>
        /// Collection of neurons
        /// </summary>
        public IList<IHiddenNeuron> HiddenNeurons { get; }

        /// <summary>
        /// Perfoms recmnation of population
        /// </summary>
        public void Recombine();

        /// <summary>
        /// Performs burst mutation pf population
        /// </summary>
        public void BurstMutation();
    }
}