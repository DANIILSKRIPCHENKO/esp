using Esp.Core.Builder;

namespace Esp.Core.Executor
{
    public class EspExecutor : IExecutable
    {
        public void Execute()
        {
            var esp = EspBuilder.Build(3, 20);

            while (true)
            {
                esp.Evaluate();

                esp.CheckStagnation();

                esp.Recombine();
            }
        }
    }
}
