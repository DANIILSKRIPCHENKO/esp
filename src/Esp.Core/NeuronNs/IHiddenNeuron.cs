using Esp.Core.SynapseNs;

namespace Esp.Core.NeuronNs
{
    public interface IHiddenNeuron : INeuronBase
    {
        public IList<ISynapse> Inputs { get; set; }

        public IList<ISynapse> Outputs { get; set; }

        public void AddInputNeuron(IInputNeuron inputNeuron);

        public void AddOutputNeuron(IOutputNeuron outputNeuron);

        public double GetFitness();

        public void AddFitness(double fit);

        public int Trials { get; }
    }
}
