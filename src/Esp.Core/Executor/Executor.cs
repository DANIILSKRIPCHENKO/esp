using Esp.Core.EspNS;
using Esp.Core.Report;

namespace Esp.Core.Executor
{
    /// <summary>
    /// Represents Executor implementation
    /// </summary>
    public class Executor : IExecutable
    {
        private readonly IGeneticAlgorithm _geneticAlgorithm;
        private readonly IGeneticAlgorithmReportBuilder _geneticAlgorithmReportBuilder;

        public Executor(
            IGeneticAlgorithm geneticAlgorithm,
            IGeneticAlgorithmReportBuilder geneticAlgorithmReportBuilder)
        {
            _geneticAlgorithm = geneticAlgorithm;
            _geneticAlgorithmReportBuilder = geneticAlgorithmReportBuilder;
        }

        public void Execute()
        {
            double fitness = 0;
            int generation = 0;

            while (fitness < 10000000 && generation < 35)
            {
                fitness = _geneticAlgorithm.Evaluate();

                _geneticAlgorithm.CheckStagnation();

                _geneticAlgorithm.Recombine();

                generation++;
            }

            if (_geneticAlgorithm is IReportableGeneticAlgorithm reportableGeneticAlgorithm)
                _geneticAlgorithmReportBuilder.BuildAndSaveReport(reportableGeneticAlgorithm);
        }
    }
}
