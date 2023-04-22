using Esp.Core.NeuronNs;

namespace Esp.Core.NeuralLayerNs
{
    public class NeuralLayer
    {
        private readonly IList<INeuron> _neurons;

        public IList<INeuron> Neurons { get => _neurons; }

        public NeuralLayer(IList<INeuron> neurons)
        {
            _neurons = neurons;
        }

        public void ConnectInput(NeuralLayer inputLayer)
        {
            var combos = _neurons.SelectMany(
                neuron => inputLayer.Neurons, 
                (neuron, input) => new { neuron, input });
            
            combos.ToList().ForEach(x => x.neuron.AddInputNeuron(x.input));
        }

        public void ConnectOutput(NeuralLayer outputLayer)
        {
            var combos = _neurons.SelectMany(
                neuron => outputLayer.Neurons,
                (neuron, output) => new { neuron, output });

            combos.ToList().ForEach(x => x.neuron.AddOutputNeuron(x.output));
        }
    }
}
