using Esp.Core.EspNS;

namespace Esp.Core.Report
{
    public interface IGeneticAlgorithmReportBuilder
    {
        public void BuildAndSaveReport(IReportableGeneticAlgorithm reportableGeneticAlgorithm);
    }
}
