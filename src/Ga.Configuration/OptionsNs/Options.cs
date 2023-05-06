

using CommandLine;

namespace Ga.Configuration.OptionsNs
{
    public class Options : IOptions
    {
        [Option('p', "populations", Required = true, Default = 30, HelpText = "The number of populations (also number of neurons in hidden layer)")]
        public int NumberOfPopulations { get; set; }

        [Option('n', "neurons", Required = true, Default = 500, HelpText = "The number of neurons in population")]
        public int NumberOfNeuronsInPopulation { get; set; }
    }
}
