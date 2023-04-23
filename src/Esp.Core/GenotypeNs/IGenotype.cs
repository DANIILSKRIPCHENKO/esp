namespace Esp.Core.GenotypeNs
{
    public interface IGenotype
    {
        public IList<double> InputWeights { get; }

        public IList<double> OutputWeights { get; }

        public (IGenotype, IGenotype) Recombine(IGenotype genotype);
    }
}
