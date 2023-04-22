using Esp.Core.Common;
using Esp.Core.SynapseNs;

namespace Esp.Core.NeuronNs
{
    public interface INeuron : IId
    {
        public IList<ISynapse> Inputs { get; set; }

        public IList<ISynapse> Outputs { get; set; }

        public void AddInputNeuron(INeuron inputNeuron);

        public double CalculateOutput();

        public double GetFitness();

        public void AddFitness(double fit);

        public void PushValueOnInput(double inputValue);

        public void AddInputSynapse(double inputValue);

        public int Trials { get; }
    }
}
