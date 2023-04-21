using Esp.Core.Activation;
using Esp.Core.NeuronNs;

namespace Esp.Core.NetworkNs
{
    public class SimpleRecurrentNetwork : INetwork
    {
        private readonly Guid _id = Guid.NewGuid();

        private readonly IActivationFunction _activationFunction;

        private readonly int _geneSize;
        
        public SimpleRecurrentNetwork(
            IEnumerable<INeuron> inputNeurons,
            IEnumerable<INeuron> hiddenNeurons,
            IEnumerable<INeuron> outputNeurons,
            IActivationFunction activationFunction)
        {
            _geneSize = inputNeurons.Count() 
                + hiddenNeurons.Count() 
                + outputNeurons.Count();

            _activationFunction = activationFunction;
        }

        public Guid GetId() => _id;

        public IEnumerable<double> GetOutput()
        {
            throw new NotImplementedException();
        }
    }
}
