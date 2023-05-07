namespace Ga.Core.Task;

public interface ITask
{
    public IReadOnlyCollection<Dataframe> GetDataset();
}