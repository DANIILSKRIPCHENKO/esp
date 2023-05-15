using Ga.Core.GeneticAlgorithmManager;
using Ga.Core.Models;
using Ga.Core.NetworkNs;
using Ga.Core.PersistenceManager;
using Ga.Core.Report;
using Ga.Core.Task;

namespace Ga.Host.StartUpNs;

public class StartUp : IStartUp
{
    private readonly IGeneticAlgorithmManager _geneticAlgorithmManager;
    private readonly IPersistenceManager _persistenceManager;
    private readonly ITask _task;
    private readonly IReportBuilder _reportBuilder;

    public StartUp(
        IGeneticAlgorithmManager geneticAlgorithmManager,
        IPersistenceManager persistenceManager,
        IReportBuilder reportBuilder,
        ITask task)
    {
        _geneticAlgorithmManager = geneticAlgorithmManager;
        _persistenceManager = persistenceManager;
        _reportBuilder = reportBuilder;
        _task = task;
    }

    public void Run()
    {
        var trainingConfiguration = new TrainingConfiguration()
        {
            TargetFitness = 3,
            GenerationsLimit = 2000,
            TrainingTimeLimit = TimeSpan.FromMinutes(60)
        };

        var trainResult = _geneticAlgorithmManager.Train(_task, trainingConfiguration);

        _persistenceManager.SaveToFile("network.json", trainResult.NeuralNetwork);

        var reportData = new ReportData(trainResult.FitnessHistory, trainResult.LossHistory,
            trainResult.AccuracyHistory, trainResult.PopulationHistory, trainResult.BestFitnessHistory);

        _reportBuilder.SaveReports(reportData);

        var loadedNetwork = _persistenceManager.LoadFromFile<INeuralNetwork>("network.json");

        loadedNetwork.BindLayers();

        var evaluationResult = loadedNetwork.EvaluateOnDataset(_task);
    }
}