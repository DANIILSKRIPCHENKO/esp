using Ga.Core.EspNS;

namespace Ga.Core.Report
{
    public interface IGeneticAlgorithmReportBuilder
    {
        public void BuildAndSaveReport(IReportableGeneticAlgorithm reportableGeneticAlgorithm);
    }
}
