using Ga.Core.NetworkNs;

namespace Ga.Core.Models;

public class TrainResult
{
    public TrainResult(
        INeuralNetwork neuralNetwork,
        List<double> fitnessHistory,
        List<double> lossHistory,
        List<double> accuracyHistory,
        List<int> populationHistory,
        List<double> bestFitnessHistory)
    {
        NeuralNetwork = neuralNetwork;
        FitnessHistory = fitnessHistory;
        LossHistory = lossHistory;
        AccuracyHistory = accuracyHistory;
        PopulationHistory = populationHistory;
        BestFitnessHistory = bestFitnessHistory;
    }

    public INeuralNetwork NeuralNetwork { get; }

    public List<double> FitnessHistory { get; }

    public List<double> LossHistory { get; }

    public List<double> AccuracyHistory { get; }

    public List<int> PopulationHistory { get; }

    public List<double> BestFitnessHistory { get; }
}