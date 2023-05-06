using Ga.Core.Distribution;

namespace Ga.Core.GenotypeNs
{
    public class GenotypeBuilder : IGenotypeBuilder
    {
        private readonly IDistribution _distribution;

        public GenotypeBuilder(IDistribution distribution)
        {
            _distribution = distribution;
        }

        public IGenotype BuildGenotype(int inputLayerSize, int outputLayerSize)
        {
            return Genotype.CreateRandom(inputLayerSize, outputLayerSize, _distribution);
        }
    }
}
