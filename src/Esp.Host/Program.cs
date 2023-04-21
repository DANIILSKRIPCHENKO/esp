using Esp.Core.Executor;

namespace Esp.Host
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var espExecutor = new EspExecutor();

            espExecutor.Execute();
        }
    }
}
