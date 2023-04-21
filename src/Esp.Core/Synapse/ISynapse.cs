namespace Esp.Core.Synapse
{
    public interface ISynapse
    {
        public double Weight { get; set; }

        public double GetOutput();
    }
}
