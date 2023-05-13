using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Ga.Core.ConfigurationNs;

namespace Ga.Core.Task;

public class Task : ITask
{
    private List<Dataframe> _dataframes = new();

    public Task(IGeneticAlgorithmConfiguration configuration)
    {
        ReadFileData(configuration.DatasetFileName);
    }

    public IReadOnlyCollection<Dataframe> GetDataset() => _dataframes.AsReadOnly();

    private void ReadFileData(string filename)
    {
        using var reader = new StreamReader(filename);
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false
        });

        var records = csv.GetRecords<Record>();

        var values = records
            .Select(record =>
                Array.ConvertAll(record.Data.Split(" "), double.Parse)
                    .ToList())
            .ToList();

        var temp = values.Select(value => new Dataframe()
        {
            Inputs = value.Take(9).ToList(),
            ExpectedOutputs = value.TakeLast(2).ToList(),
        }).ToList();

        var dataframes1 = temp
            .Where(x => Convert.ToInt32(x.ExpectedOutputs.First()) == 0)
            .Take(100).ToList();

        var dataframes2 = temp
            .Where(x => Convert.ToInt32(x.ExpectedOutputs.First()) == 1)
            .Take(100).ToList();

        for (var index = 0; index < dataframes1.Count; index++)
        {
            _dataframes.Add(dataframes1[index]);
            _dataframes.Add(dataframes2[index]);
        }
    }

    private class Record
    {
        public string Data { get; set; }
    }
}