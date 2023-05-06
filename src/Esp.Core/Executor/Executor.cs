using Ga.Core.EspNS;
using Ga.Core.Report;

namespace Ga.Core.Executor
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

            while (fitness < 100000000 && generation < 100)
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
