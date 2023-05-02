namespace Esp.Core.EspNS
{
    public interface IReportableGeneticAlgorithm
    {
        public IList<double> GetActualFitnessHistory();

        public IList<double> GetBestFitnessHistory();

        public IList<int> GetPopulationHistory();
    }
}
