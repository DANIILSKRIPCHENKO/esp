using Esp.Core.Extensions;

namespace Esp.Core.GenotypeNs
{
    /// <summary>
    /// Implementation of IGenotype interface
    /// </summary>
    public class Genotype : IGenotype
    {
        private readonly IList<double> _inputWeights;
        private readonly IList<double> _outputWeights;

        #region ctor

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

        private Genotype(
            IList<double> inputWeights,
            IList<double> outputWeights)
        {
            _inputWeights = inputWeights;
            _outputWeights = outputWeights;
        }

        #endregion

        #region IGenotype implementation

        public IList<double> InputWeights => _inputWeights;

        public IList<double> OutputWeights => _outputWeights;

        public static Genotype CreateRandom(int hiddenLayerSize) =>
            new(hiddenLayerSize);

        public IList<IGenotype> BurstMutate(int numberOfGenotypes)
        {
            var weights = new List<double>();
            weights.AddRange(_inputWeights);
            weights.AddRange(_outputWeights);

            var result = new List<IGenotype>();
            
            for(var i = 0; i < numberOfGenotypes; i++)
            {
                var newWeights = new List<double>();

                foreach(var weight in weights)
                {
                    newWeights.Add(weight + GetCauchyNoise());
                }

                (var inputWeights, var outputWeights) = newWeights.Half();

                result.Add(new Genotype(inputWeights, outputWeights));
            }

            return result;
        }

        public (IGenotype, IGenotype) Recombine(IGenotype genotype)
        {
            if (genotype.InputWeights.Count != _inputWeights.Count
                || genotype.OutputWeights.Count != _outputWeights.Count)
                throw new Exception("Collection sizes are not matched");

            var weights = new List<double>();
            weights.AddRange(_inputWeights);
            weights.AddRange(_outputWeights);

            var newWeights = new List<double>();
            newWeights.AddRange(genotype.InputWeights);
            newWeights.AddRange(genotype.OutputWeights);

            var random = new Random();
            var crossOverPointIndex = random.Next(weights.IndexOf(weights.Last()));

            (var result1, var result2) = weights.CrossOver(newWeights, crossOverPointIndex);

            (var inputWeights1, var outputWeights1) = result1.Half();
            var resultGenotype1 = new Genotype(inputWeights1, outputWeights1);

            (var inputWeights2, var outputWeights2) = result2.Half();
            var resultGenotype2 = new Genotype(inputWeights2, outputWeights2);

            return (resultGenotype1, resultGenotype2);
        }

        #endregion

        #region Private methods

        private double GetCauchyNoise()
        {
            var x = GetPseudoDoubleWithinRange(-1, 1);

            double a = 0.3;

            return a / (Math.PI * (Math.Pow(a, 2) + Math.Pow(x, 2)));
        }

        private static double GetPseudoDoubleWithinRange(double lowerBound, double upperBound)
        {
            var random = new Random();
            var rDouble = random.NextDouble();
            var rRangeDouble = rDouble * (upperBound - lowerBound) + lowerBound;
            return rRangeDouble;
        }

        #endregion
    }
}
