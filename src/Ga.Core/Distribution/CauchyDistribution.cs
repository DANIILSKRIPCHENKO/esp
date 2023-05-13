namespace Ga.Core.Distribution
{
    public class CauchyDistribution : IDistribution
    {
        public double GenerateRandom(double referenceValue)
        {
            var randomValue = new Random().NextDouble();

            const int gamma = 3;

            var result = referenceValue + gamma * Math.Tan(Math.PI * (randomValue - 0.5));

            return result;
        }
    }
}
