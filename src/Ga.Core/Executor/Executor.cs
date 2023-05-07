using Ga.Core.EspNS;
using Ga.Core.NetworkNs;
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

        public Executor(
            IGeneticAlgorithm geneticAlgorithm,
            IGeneticAlgorithmReportBuilder geneticAlgorithmReportBuilder,
            ITask task)
        {
            _geneticAlgorithm = geneticAlgorithm;
            _geneticAlgorithmReportBuilder = geneticAlgorithmReportBuilder;
            _task = task;
        }

        public void Execute()
        {
            var currentDataFrame = 0;
            foreach (var dataframe in _task.GetDataset().Take(40))
            {
                double fitness = 0;
                var generation = 0;

                _geneticAlgorithm.SetInputs(dataframe.Inputs);
                _geneticAlgorithm.SetOutputs(dataframe.ExpectedOutputs);

                while (fitness < 10)
                {
                    fitness = _geneticAlgorithm.Evaluate();

                    _geneticAlgorithm.CheckStagnation();

                    _geneticAlgorithm.Recombine();

                    generation++;
                }

                //_geneticAlgorithm.ResetParameters();
                currentDataFrame++;
            }

            if (_geneticAlgorithm is IReportableGeneticAlgorithm reportableGeneticAlgorithm)
                _geneticAlgorithmReportBuilder.BuildAndSaveReport(reportableGeneticAlgorithm);
        }
    }
}
