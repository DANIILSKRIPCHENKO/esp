using Esp.Core.Builder;

namespace Esp.Host
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var esp = EspBuilder.Build(5, 15);

            esp.Run();
        }
    }
}
