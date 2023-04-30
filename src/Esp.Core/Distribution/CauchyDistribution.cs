namespace Esp.Core.Distribution
{
    public class CauchyDistribution : IDistribution
    {
        public double GenerateRandom(double referenceValue)
        {
            var randomValue = new Random().NextDouble();

            var result = referenceValue + Math.Tan(Math.PI * (randomValue - 0.5));
            
            return result;
        }
    }
}
