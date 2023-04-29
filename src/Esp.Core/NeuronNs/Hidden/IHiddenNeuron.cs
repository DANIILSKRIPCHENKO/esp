using Esp.Core.GenotypeNs;
using Esp.Core.NeuronNs.Input;
using Esp.Core.NeuronNs.Output;
using Esp.Core.SynapseNs;

namespace Esp.Core.NeuronNs.Hidden
{
    /// <summary>
    /// Represents interface of neuron in hidden layer
    /// </summary>
    public interface IHiddenNeuron : INeuronBase
    {
        /// <summary>
        /// Input connections of neuron
        /// </summary>
        public IList<ISynapse> Inputs { get; set; }

        /// <summary>
        /// Output connections of neuron
        /// </summary>
        public IList<ISynapse> Outputs { get; set; }

        /// <summary>
        /// Adds input neuron
        /// </summary>
        /// <param name="inputNeuron"></param>
        public void AddInputNeuron(IInputNeuron inputNeuron);

        /// <summary>
        /// Add output neuron
        /// </summary>
        /// <param name="outputNeuron"></param>
        public void AddOutputNeuron(IOutputNeuron outputNeuron);

        /// <summary>
        /// Returns neuron fitness
        /// </summary>
        public double Fitness { get; }

        /// <summary>
        /// Adds fitness value to neuron
        /// </summary>
        /// <param name="fit"></param>
        public void AddFitness(double fit);

        /// <summary>
        /// Returns trial number of neurons
        /// </summary>
        public int Trials { get; }

        /// <summary>
        /// Resets connections of neuron
        /// </summary>
        public void ResetConnection();

        /// <summary>
        /// Genotype of neuron
        /// </summary>
        public IGenotype Genotype { get; }

        /// <summary>
        /// Perform recombination operation with neurons
        /// </summary>
        /// <param name="hiddenNeuron"></param>
        /// <returns></returns>
        public (IHiddenNeuron, IHiddenNeuron) Recombine(IHiddenNeuron hiddenNeuron);

        /// <summary>
        /// Perfom burst mutation with neuron
        /// </summary>
        /// <param name="numberOfNeuronsToGrow"></param>
        /// <returns></returns>
        public IList<IHiddenNeuron> BurstMutate(int numberOfNeuronsToGrow);
    }
}
