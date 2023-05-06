using Ga.Core.EspNS;
using ScottPlot;

namespace Ga.Core.Report
{
    public class GeneticAlgorithmReportBuilder : IGeneticAlgorithmReportBuilder
    {
        public void BuildAndSaveReport(IReportableGeneticAlgorithm reportableGeneticAlgorithm)
        {
            BuildAndSaveActualFitnessHistoryReport(reportableGeneticAlgorithm.GetActualFitnessHistory());

            BuildAndSaveBestFitnessHistoryReport(reportableGeneticAlgorithm.GetBestFitnessHistory());

            BuildAndSavePopulationHistoryReport(reportableGeneticAlgorithm.GetPopulationHistory());
        }

        private void BuildAndSaveActualFitnessHistoryReport(IList<double> values)
        {
            var plot = new Plot(1000, 1000);

            var pointCount = DataGen.Consecutive(values.Count);

            plot.AddScatter(pointCount, values.ToArray());

            plot.SaveFig("actualFitness.png");
        }

        private void BuildAndSaveBestFitnessHistoryReport(IList<double> values)
        {
            var plot = new Plot(1000, 1000);

            var pointCount = DataGen.Consecutive(values.Count);

            plot.AddScatter(pointCount, values.ToArray());

            plot.SaveFig("bestFitness.png");
        }

        private void BuildAndSavePopulationHistoryReport(IList<int> values)
        {
            var plot = new Plot(1000, 1000);

            var pointCount = DataGen.Consecutive(values.Count);

            plot.AddScatter(pointCount, values.Select<int, double>(x => x).ToArray());

            plot.SaveFig("populationHistory.png");
        }
    }
}
