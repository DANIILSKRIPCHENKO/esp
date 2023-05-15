using Ga.Core.Models;
using ScottPlot;

namespace Ga.Core.Report
{
    public class ReportBuilder : IReportBuilder
    {
        public void SaveReports(ReportData reportData)
        {
            var dateTimeNow = DateTime.Now.ToString("yyyy-MM-dd/HH-mm-ss");
            Directory.CreateDirectory(dateTimeNow);

            SaveFitnessHistoryReport(dateTimeNow, reportData.FitnessHistory);
            SaveBestFitnessHistoryReport(dateTimeNow, reportData.BestFitnessHistory);
            SaveLossHistoryReport(dateTimeNow, reportData.LossHistory);
            SavePopulationHistoryReport(dateTimeNow, reportData.PopulationHistory);
            SaveAccuracyHistoryReport(dateTimeNow, reportData.AccuracyHistory);
        }

        private static void SaveFitnessHistoryReport(string folderName, ICollection<double> values)
        {
            SaveReport(string.Concat(folderName, "/fitness-history.png"), values);
        }

        private static void SaveLossHistoryReport(string folderName, ICollection<double> values)
        {
            SaveReport(string.Concat(folderName, "/loss-history.png"), values);
        }

        private static void SaveBestFitnessHistoryReport(string folderName, ICollection<double> values)
        {
            SaveReport(string.Concat(folderName, "/best-fitness-history.png"), values);
        }

        private static void SavePopulationHistoryReport(string folderName, ICollection<int> values)
        {
            SaveReport(string.Concat(folderName, "/population-history.png"), values);
        }

        private static void SaveAccuracyHistoryReport(string folderName, ICollection<double> values)
        {
            SaveReport(string.Concat(folderName, "/accuracy-history.png"), values);
        }

        private static void SaveReport(string path, ICollection<int> values)
        {
            var plot = new Plot(1000, 1000);

            var pointCount = DataGen.Consecutive(values.Count);

            plot.AddScatter(pointCount, values.Select<int, double>(x => x).ToArray());

            plot.SaveFig(path);
        }

        private static void SaveReport(string path, ICollection<double> values)
        {
            var plot = new Plot(1000, 1000);

            var pointCount = DataGen.Consecutive(values.Count);

            plot.AddScatter(pointCount, values.ToArray());

            plot.SaveFig(path);
        }
    }
}
