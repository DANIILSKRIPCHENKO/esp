namespace Ga.Core.Models;

public class ReportData
{
    public ReportData(
        List<double> fitnessHistory,
        List<double> lossHistory,
        List<double> accuracyHistory,
        List<int> populationHistory,
        List<double> bestFitnessHistory)
    {
        FitnessHistory = fitnessHistory;
        LossHistory = lossHistory;
        AccuracyHistory = accuracyHistory;
        PopulationHistory = populationHistory;
        BestFitnessHistory = bestFitnessHistory;
    }

    public List<double> FitnessHistory { get; }

    public List<double> LossHistory { get; }

    public List<double> AccuracyHistory { get; }

    public List<int> PopulationHistory { get; }

    public List<double> BestFitnessHistory { get; }
}