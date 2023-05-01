using CommandLine;
using Esp.Api;
using Esp.Core.ActivationFunction;
using Esp.Core.Distribution;
using Esp.Core.EspNS;
using Esp.Core.Executor;
using Esp.Core.GenotypeNs;
using Esp.Core.LossFunction;
using Esp.Core.NetworkNs;
using Esp.Core.NeuralLayerNs.Hidden;
using Esp.Core.NeuralLayerNs.Input;
using Esp.Core.NeuralLayerNs.Output;
using Esp.Core.NeuronNs.Hidden;
using Esp.Core.NeuronNs.Input;
using Esp.Core.NeuronNs.Output;
using Esp.Core.PopulationNs;
using Microsoft.Extensions.DependencyInjection;

namespace Esp.Host
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed(options =>
            {
                IServiceProvider serviceProvider = BuildServiceProvider(options);

                var exucutor = serviceProvider.GetService<IExecutable>();

                exucutor!.Execute();
            });
            
        }

        private static IServiceProvider BuildServiceProvider(Options options)
        {
            var services = new ServiceCollection();

            services.AddTransient<IExecutable, Executor>();
            services.AddTransient<IGeneticAlgorithm, Core.EspNS.Esp>();
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

            services.AddSingleton<IOptions>(options);

            return services.BuildServiceProvider();
        }
    }
}
