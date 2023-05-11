using CommandLine;
using Ga.Core.Executor;
using Ga.Extensions.Microsoft.DependencyInjection;
using Ga.Host.OptionsNs;
using Microsoft.Extensions.DependencyInjection;

namespace Ga.Host
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed(options =>
            {
                var serviceProvider = BuildServiceProvider(options);

                var exucutor = serviceProvider.GetService<IExecutable>();

                exucutor!.Execute();
            });
        }

        private static IServiceProvider BuildServiceProvider(Options options)
        {
            var services = new ServiceCollection();

            var configuration = new Core.ConfigurationNs.GeneticAlgorithmConfiguration
            {
                NumberOfNeuronsInPopulation = options.NumberOfNeuronsInPopulation,
                NumberOfPopulations = options.NumberOfPopulations,
                DatasetFileName = options.DatasetFileName
            };

            services.AddGeneticAlgorithms(configuration);

            services.AddSingleton<IOptions>(options);

            return services.BuildServiceProvider();
        }
    }
}
