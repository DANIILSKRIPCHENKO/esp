using Esp.Core.Common;
using Esp.Core.Synapse;

namespace Esp.Core.NeuronNs
{
    public interface INeuron : IId
    {
        public IEnumerable<ISynapse> Inputs { get; set; }

        public IEnumerable<ISynapse> Outputs { get; set; }

        public double CalculateOutput();

        public double GetFitness();

        public void AddFitness(double fit);
    }
}
