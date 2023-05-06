using CommandLine;
using Ga.Configuration.OptionsNs;
using Ga.Core.ActivationFunction;
using Ga.Core.Distribution;
using Ga.Core.EspNS;
using Ga.Core.Executor;
using Ga.Core.GenotypeNs;
using Ga.Core.LossFunction;
using Ga.Core.NetworkNs;
using Ga.Core.NeuralLayerNs.Hidden;
using Ga.Core.NeuralLayerNs.Input;
using Ga.Core.NeuralLayerNs.Output;
using Ga.Core.NeuronNs.Hidden;
using Ga.Core.NeuronNs.Input;
using Ga.Core.NeuronNs.Output;
using Ga.Core.PopulationNs;
using Ga.Core.Report;
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

            services.AddTransient<IExecutable, Executor>();
            services.AddTransient<IGeneticAlgorithm, Esp>();
            services.AddTransient<INeuralNetworkBuilder, FullyConnectedNeuralNetworkBuilder>();

            services.AddTransient<IInputLayer, InputLayer>();
            services.AddTransient<IInputNeuronBuilder, InputNeuronBuilder>();

            services.AddTransient<IOutputLayer, OutputLayer>();
            services.AddTransient<IOutputNeuronBuilder, OutputNeuronBuilder>();

            services.AddTransient<IHiddenNeuronBuilder, HiddenNeuronBuilder>();
            services.AddTransient<IHiddenLayerBuilder, HiddenLayerBuilder>();

            services.AddTransient<IActivationFunction, SigmoidActivationFunction>();
            services.AddTransient<IPopulationBuilder, PopulationBuilder>();
            services.AddTransient<IGenotypeBuilder, GenotypeBuilder>();

            services.AddTransient<IDistribution, CauchyDistribution>();

            services.AddTransient<ILossFunction, Mse>();

            services.AddTransient<IGeneticAlgorithmReportBuilder, GeneticAlgorithmReportBuilder>();

            services.AddSingleton<IOptions>(options);

            return services.BuildServiceProvider();
        }
    }
}
