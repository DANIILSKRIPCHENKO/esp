using Esp.Core.Builder;

namespace Esp.Core.Executor
{
    /// <summary>
    /// Represents EspExecutor implementation
    /// </summary>
    public class EspExecutor : IExecutable
    {
        public void Execute()
        {
            var esp = EspBuilder.Build(50, 50);

            double fitness = 0;

            while (fitness <  600000)
            {
                fitness = esp.Evaluate();

                esp.CheckStagnation();

                esp.Recombine();
            }
        }
    }
}
