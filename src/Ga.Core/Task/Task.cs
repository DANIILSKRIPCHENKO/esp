using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Ga.Core.ConfigurationNs;

namespace Ga.Core.Task;

public class Task : ITask
{
    private List<Dataframe> _dataframes;

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

        _dataframes = values.Select(value => new Dataframe()
        {
            Inputs = value.Take(9).ToList(),
            ExpectedOutputs = value.TakeLast(2).ToList(),
        }).ToList();
    }

    private class Record
    {
        public string Data { get; set; }
    }
}