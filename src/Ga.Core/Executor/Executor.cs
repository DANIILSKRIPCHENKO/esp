using Ga.Core.EspNS;
using Ga.Core.NetworkNs;
using Ga.Core.PersistenceManager;
using Ga.Core.Report;
using Ga.Core.Task;

namespace Ga.Core.Executor
{
    /// <summary>
    /// Represents Executor implementation
    /// </summary>
    public class Executor : IExecutable
    {
        private readonly IGeneticAlgorithm _geneticAlgorithm;
        private readonly IGeneticAlgorithmReportBuilder _geneticAlgorithmReportBuilder;
        private readonly ITask _task;
        private readonly IPersistenceManager _persistenceManager;

        public Executor(
            IGeneticAlgorithm geneticAlgorithm,
            IGeneticAlgorithmReportBuilder geneticAlgorithmReportBuilder,
            IPersistenceManager persistenceManager,
            ITask task)
        {
            _geneticAlgorithm = geneticAlgorithm;
            _geneticAlgorithmReportBuilder = geneticAlgorithmReportBuilder;
            _persistenceManager = persistenceManager;
            _task = task;
        }

        public void Execute()
        {
            _geneticAlgorithm.SetDataset(_task);

            var generation = 0;

            double fitness = 0;

            while (fitness < 3 && generation < 2000)
            {
                fitness = _geneticAlgorithm.Evaluate();

                _geneticAlgorithm.CheckStagnation();

                _geneticAlgorithm.Recombine();
                generation++;
            }

            _persistenceManager.SaveToFile("network.json", _geneticAlgorithm.GetBestNetwork());

            if (_geneticAlgorithm is IReportableGeneticAlgorithm reportableGeneticAlgorithm)
                _geneticAlgorithmReportBuilder.BuildAndSaveReport(reportableGeneticAlgorithm);

            var accuracy = TestNeuralNetwork();
        }

        private double TestNeuralNetwork()
        {
            var network = _persistenceManager.LoadFromFile<INeuralNetwork>("network.json");
            network.BindLayers();
            var successNumber = 0;
            var failedNumber = 0;

            foreach (var dataframe in _task.GetDataset().TakeLast(40))
            {
                network.PushInputValues(dataframe.Inputs);
                var expectedOutputs = dataframe.ExpectedOutputs.Select(Convert.ToInt32).ToList();
                var outputs = network.GetOutputs();
                var formattedOutput = GetFormattedOutput(outputs);

                if (expectedOutputs.SequenceEqual(formattedOutput))
                {
                    successNumber++;
                    continue;
                }

                failedNumber++;
            }

            return (double)successNumber / (failedNumber + successNumber);
        }


        private List<int> GetFormattedOutput(IList<double> predicted)
        {
            var probabilities = GetProbabilities(predicted);
            var result = probabilities.Select(Convert.ToInt32).ToList();
            return result;
        }


        /// <summary>
        /// Returns probabilities as softmax
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private IEnumerable<double> GetProbabilities(IList<double> values) => values
            .Select(value =>
                Math.Exp(value) / values.Select(Math.Exp).Sum()).ToList();

    }
}
