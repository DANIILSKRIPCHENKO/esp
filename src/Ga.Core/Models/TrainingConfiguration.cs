namespace Ga.Core.Models;

public class TrainingConfiguration
{
    public double TargetFitness { get; set; }
    public int GenerationsLimit { get; set; }
    public TimeSpan TrainingTimeLimit { get; set; }
}