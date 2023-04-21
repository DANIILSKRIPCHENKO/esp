using Esp.Core.Builder;

namespace Esp.Core.Executor
{
    public class EspExecutor : IExecutable
    {
        public void Execute()
        {
            var esp = EspBuilder.Build(5, 15);

            esp.Evaluate();
        }
    }
}
