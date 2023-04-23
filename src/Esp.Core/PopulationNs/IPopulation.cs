using Esp.Core.Common;
using Esp.Core.NeuronNs;

namespace Esp.Core.PopulationNs
{
    public interface IPopulation : IId
    {
        public IHiddenNeuron GetRandomNeuron();

        public IList<IHiddenNeuron> HiddenNeurons { get; }

        public void Recombine();
    }
}