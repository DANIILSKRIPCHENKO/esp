namespace Ga.Core.Distribution
{
    public class CauchyDistribution : IDistribution
    {
        public double GenerateRandom(double referenceValue)
        {
            var randomValue = new Random().NextDouble();

            var gamma = 5;

            var result = referenceValue + gamma * Math.Tan(Math.PI * (randomValue - 0.5));

            return result;
        }
    }
}
