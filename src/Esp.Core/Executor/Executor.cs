using Esp.Core.EspNS;

namespace Esp.Core.Executor
{
    /// <summary>
    /// Represents Executor implementation
    /// </summary>
    public class Executor : IExecutable
    {
        private readonly IGeneticAlgorithm _geneticAlgorithm;

        public Executor(IGeneticAlgorithm geneticAlgorithm)
        {
            _geneticAlgorithm = geneticAlgorithm;
        }

        public void Execute()
        {
            double fitness = 0;

            while (fitness <  6000000)
            {
                fitness = _geneticAlgorithm.Evaluate();

                _geneticAlgorithm.CheckStagnation();

                _geneticAlgorithm.Recombine();
            }
        }
    }
}
