using Ga.Core.NetworkNs;

namespace Ga.Core.PersistenceManager;

public interface IPersistenceManager
{
    public void SaveToFile(string fileName, INeuralNetwork neuralNetwork);

    public T LoadFromFile<T>(string fileName);
}