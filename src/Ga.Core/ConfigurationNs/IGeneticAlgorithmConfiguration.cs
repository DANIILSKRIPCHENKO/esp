namespace Ga.Core.ConfigurationNs
{
    public interface IGeneticAlgorithmConfiguration
    {
        public int NumberOfPopulations { get; }

        public int NumberOfNeuronsInPopulation { get; }

        public string DatasetFileName { get; }
    }
}
