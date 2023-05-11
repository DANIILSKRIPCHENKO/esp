using Ga.Core.ActivationFunction;
using Ga.Core.ConfigurationNs;
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
using Ga.Core.PersistenceManager;
using Ga.Core.PopulationNs;
using Ga.Core.Report;
using Ga.Core.Task;
using Microsoft.Extensions.DependencyInjection;

namespace Ga.Extensions.Microsoft.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGeneticAlgorithms(this IServiceCollection services, GeneticAlgorithmConfiguration configuration)
        {
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

            services.AddTransient<ILossFunction, BinaryCrossEntropy>();

            services.AddTransient<ITask, Core.Task.Task>();

            services.AddTransient<IGeneticAlgorithmReportBuilder, GeneticAlgorithmReportBuilder>();

            services.AddTransient<IPersistenceManager, PersistenceManager>();

            services.AddSingleton<IGeneticAlgorithmConfiguration>(configuration);

            return services;
        }
    }
}