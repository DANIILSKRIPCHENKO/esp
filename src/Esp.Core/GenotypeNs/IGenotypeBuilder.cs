﻿namespace Ga.Core.GenotypeNs
{
    public interface IGenotypeBuilder
    {
        public IGenotype BuildGenotype(int inputLayerSize, int outputLayerSize);
    }
}
