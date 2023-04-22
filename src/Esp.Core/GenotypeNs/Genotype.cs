namespace Esp.Core.GenotypeNs
{
    public class Genotype : IGenotype
    {
        private readonly List<double> _inputWeights;
        private readonly List<double> _outputWeights;

        private Genotype(int hiddenLayerSize)
        {
            var inputWeights = new List<double>();
            var outputWeights = new List<double>();
            var random = new Random();

            for (int i = 0; i < hiddenLayerSize; i++)
            {
                inputWeights.Add(random.NextDouble());
                outputWeights.Add(random.NextDouble());
            }

            _inputWeights = inputWeights;
            _outputWeights = outputWeights;
        }

        public IList<double> InputWeights => _inputWeights;

        public IList<double> OutputWeights => _outputWeights;

        public static Genotype CreateRandom(int hiddenLayerSize) =>
            new(hiddenLayerSize);
    }
}
