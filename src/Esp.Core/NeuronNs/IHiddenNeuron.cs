using Esp.Core.GenotypeNs;
using Esp.Core.SynapseNs;

namespace Esp.Core.NeuronNs
{
    public interface IHiddenNeuron : INeuronBase
    {
        public IList<ISynapse> Inputs { get; set; }

        public IList<ISynapse> Outputs { get; set; }

        public void AddInputNeuron(IInputNeuron inputNeuron);

        public void AddOutputNeuron(IOutputNeuron outputNeuron);

        public double Fitness { get; }

        public void AddFitness(double fit);

        public int Trials { get; }

        public void ResetConnection();

        public IList<double> FitnessHistory { get; }

        public IGenotype Genotype { get; }

        public (IHiddenNeuron, IHiddenNeuron) Recombine(IHiddenNeuron hiddenNeuron);

        public IList<IHiddenNeuron> BurstMutate(int numberOfNeuronsToGrow);
    }
}
