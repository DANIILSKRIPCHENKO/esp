using Esp.Core.Common;
using Esp.Core.SynapseNs;

namespace Esp.Core.NeuronNs
{
    public interface INeuron : IId
    {
        public IEnumerable<ISynapse> Inputs { get; set; }

        public IEnumerable<ISynapse> Outputs { get; set; }

        public void AddInputNeuron(INeuron inputNeuron);

        public double CalculateOutput();

        public double GetFitness();

        public void AddFitness(double fit);
    }
}
