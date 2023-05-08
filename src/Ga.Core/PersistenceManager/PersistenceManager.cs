using Ga.Core.NetworkNs;
using Newtonsoft.Json;

namespace Ga.Core.PersistenceManager;

public class PersistenceManager : IPersistenceManager
{
    public void SaveToFile(string fileName, INeuralNetwork network)
    {
        var settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            NullValueHandling = NullValueHandling.Ignore,
        };
        var json = JsonConvert.SerializeObject(network, typeof(INeuralNetwork), settings);
        File.WriteAllText(fileName, json);
    }

    public T LoadFromFile<T>(string fileName)
    {
        var settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            NullValueHandling = NullValueHandling.Ignore,
        };

        var deserialized = JsonConvert.DeserializeObject<T>(File.ReadAllText(fileName), settings);
        if (deserialized == null) throw new ArgumentNullException(nameof(deserialized));
        return deserialized;
    }
}