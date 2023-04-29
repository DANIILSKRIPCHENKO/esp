using Esp.Core.ActivationFunction;
using Esp.Core.EspNS;
using Esp.Core.Executor;
using Esp.Core.GenotypeNs;
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
            var services = new ServiceCollection();
            
            ConfigureServices(services);
            
            services.BuildServiceProvider()
                .GetService<IExecutable>()
                !.Execute();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IExecutable, Executor>();
            services.AddSingleton<IGeneticAlgorithm, Core.EspNS.Esp>();
            services.AddSingleton<INeuralNetworkBuilder, FullyConnectedNeuralNetworkBuilder>();
            
            services.AddSingleton<IInputLayer, InputLayer>();
            services.AddSingleton<IInputNeuronBuilder, InputNeuronBuilder>();

            services.AddSingleton<IOutputLayer, OutputLayer>();
            services.AddSingleton<IOutputNeuronBuilder, OutputNeuronBuilder>();

            services.AddSingleton<IHiddenNeuronBuilder, HiddenNeuronBuilder>();
            services.AddSingleton<IHiddenLayerBuilder, HiddenLayerBuilder>();

            services.AddSingleton<IActivationFunction, SigmoidActivationFunction>();
            services.AddSingleton<IPopulationBuilder, PopulationBuilder>();
            services.AddSingleton<IGenotypeBuilder, GenotypeBuilder>();

        }
    }
}
