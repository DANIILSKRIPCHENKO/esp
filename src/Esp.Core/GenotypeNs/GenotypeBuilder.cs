namespace Esp.Core.GenotypeNs
{
    public class GenotypeBuilder : IGenotypeBuilder
    {
        public IGenotype BuildGenotype(int inputLayerSize, int outputLayerSize)
        {
            return Genotype.CreateRandom(inputLayerSize, outputLayerSize);
        }
    }
}
