using Ga.Core.Distribution;
using Ga.Core.Extensions;
using Newtonsoft.Json;

namespace Ga.Core.GenotypeNs
{
    /// <summary>
    /// Implementation of IGenotype interface
    /// </summary>
    public class Genotype : IGenotype
    {
        private readonly IList<double> _inputWeights;
        private readonly IList<double> _outputWeights;
        [JsonProperty]
        private readonly IDistribution _distribution;

        #region ctor

        private Genotype(
            int inputConnectionsNumber,
            int outputConnectionsNumber,
            IDistribution distribution)
        {
            _distribution = distribution;

            var inputWeights = new List<double>();
            var outputWeights = new List<double>();

            for (int i = 0; i < inputConnectionsNumber; i++)
                inputWeights.Add(GetPseudoDoubleWithinRange(-1, 1));

            for (int i = 0; i < outputConnectionsNumber; i++)
                outputWeights.Add(GetPseudoDoubleWithinRange(-1, 1));

            _inputWeights = inputWeights;
            _outputWeights = outputWeights;
        }

        private Genotype(
            IList<double> inputWeights,
            IList<double> outputWeights,
            IDistribution distribution)
        {
            _inputWeights = inputWeights;
            _outputWeights = outputWeights;
            _distribution = distribution;
        }

        [JsonConstructor]
        public Genotype(
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

        public static Genotype CreateRandom(
            int inputConnectionsNumber,
            int outputConnectionsNumber,
            IDistribution distribution) =>
            new(inputConnectionsNumber,
                outputConnectionsNumber,
                distribution);

        public IList<IGenotype> BurstMutate(int numberOfGenotypes)
        {
            var weights = new List<double>();
            weights.AddRange(_inputWeights);
            weights.AddRange(_outputWeights);

            var result = new List<IGenotype>();

            for (var i = 0; i < numberOfGenotypes; i++)
            {
                var newWeights = new List<double>();

                foreach (var weight in weights)
                {
                    if (!ShouldMutateGene())
                    {
                        newWeights.Add(weight);
                        continue;
                    }

                    newWeights.Add(_distribution.GenerateRandom(weight));
                }

                var inputWeights = newWeights
                    .Take(_inputWeights.Count)
                    .ToList();

                var outputWeights = newWeights
                    .TakeLast(_outputWeights.Count)
                    .ToList();

                result.Add(new Genotype(inputWeights, outputWeights, _distribution));
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

            var (result1, result2) = weights.CrossOver(newWeights, crossOverPointIndex);

            var result1News = new List<double>();
            foreach (var value in result1)
            {
                if (!ShouldMutateGene())
                {
                    result1News.Add(value);
                    continue;
                }

                result1News.Add(_distribution.GenerateRandom(value));
            }

            var result2News = new List<double>();
            foreach (var value in result2)
            {
                if (!ShouldMutateGene())
                {
                    result2News.Add(value);
                    continue;
                }

                result2News.Add(_distribution.GenerateRandom(value));
            }

            var inputWeights1 = result1News.Take(_inputWeights.Count).ToList();
            var outputWeights1 = result1News.TakeLast(_outputWeights.Count).ToList();
            var resultGenotype1 = new Genotype(inputWeights1, outputWeights1, _distribution);

            var inputWeights2 = result2News.Take(_inputWeights.Count).ToList();
            var outputWeights2 = result2News.TakeLast(_outputWeights.Count).ToList();
            var resultGenotype2 = new Genotype(inputWeights2, outputWeights2, _distribution);

            return (resultGenotype1, resultGenotype2);
        }

        #endregion

        #region Private methods

        private bool ShouldMutateGene()
        {
            var mutateGeneProbability = 0.5;

            var randomValue = new Random().NextDouble();
            if (randomValue <= mutateGeneProbability)
                return true;

            return false;
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
