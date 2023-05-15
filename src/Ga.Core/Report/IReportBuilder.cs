using Ga.Core.Models;

namespace Ga.Core.Report
{
    public interface IReportBuilder
    {
        public void SaveReports(ReportData reportData);
    }
}
