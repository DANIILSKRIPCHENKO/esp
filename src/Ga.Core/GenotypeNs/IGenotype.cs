namespace Ga.Core.GenotypeNs
{
    /// <summary>
    /// Represents an interface of genotype of Neuron
    /// </summary>
    public interface IGenotype
    {
        /// <summary>
        /// Returns input weights of neuron
        /// </summary>
        public IList<double> InputWeights { get; }

        /// <summary>
        /// Returns output weights of neuron
        /// </summary>
        public IList<double> OutputWeights { get; }

        /// <summary>
        /// Performs recombination with genotypes
        /// </summary>
        /// <param name="genotype"></param>
        /// <returns></returns>
        public (IGenotype, IGenotype) Recombine(IGenotype genotype);

        /// <summary>
        /// Performs burst mutation
        /// </summary>
        /// <param name="genotype"></param>
        /// <returns></returns>
        public IList<IGenotype> BurstMutate(int numberOfGenotypes);
    }
}
