namespace Esp.Core.NetworkNs
{
    public class Network : INetwork
    {
        private readonly Guid _id = Guid.NewGuid();

        public Guid GetId() => _id;
    }
}
