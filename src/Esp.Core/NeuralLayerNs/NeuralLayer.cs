using Esp.Core.NeuronNs;

namespace Esp.Core.NeuralLayerNs
{
    public class NeuralLayer
    {
        private readonly IEnumerable<INeuron> _neurons;

        public IEnumerable<INeuron> Neurons { get => _neurons; }

        public NeuralLayer(IEnumerable<INeuron> neurons)
        {
            _neurons = neurons;
        }

        public void ConnectLayers(NeuralLayer inputLayer)
        {
            var combos = _neurons.SelectMany(
                neuron => inputLayer.Neurons, 
                (neuron, input) => new { neuron, input });
            
            combos.ToList().ForEach(x => x.neuron.AddInputNeuron(x.input));
        }
    }
}
