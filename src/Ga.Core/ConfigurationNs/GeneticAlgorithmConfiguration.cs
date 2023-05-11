namespace Ga.Core.ConfigurationNs
{
    public class GeneticAlgorithmConfiguration : IGeneticAlgorithmConfiguration
    {
        public int NumberOfPopulations { get; set; }

        public int NumberOfNeuronsInPopulation { get; set; }

        public string DatasetFileName { get; set; }
    }
}
