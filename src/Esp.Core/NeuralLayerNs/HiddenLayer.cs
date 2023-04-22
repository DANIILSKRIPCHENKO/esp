using Esp.Core.NeuronNs;

namespace Esp.Core.NeuralLayerNs
{
    public class HiddenLayer : IHiddenLayer
    {
        private readonly IList<IHiddenNeuron> _hiddenNeurons;

        public IList<IHiddenNeuron> HiddenNeurons { get => _hiddenNeurons; }

        public HiddenLayer(IList<IHiddenNeuron> hiddenNeurons)
        {
            _hiddenNeurons = hiddenNeurons;
        }

        public void ConnectInput(IInputLayer inputLayer)
        {
            var combos = _hiddenNeurons.SelectMany(
                neuron => inputLayer.InputNeurons, 
                (neuron, input) => new { neuron, input });
            
            combos.ToList().ForEach(x => x.neuron.AddInputNeuron(x.input));
        }

        public void ConnectOutput(IOutputLayer outputLayer)
        {
            var combos = _hiddenNeurons.SelectMany(
                neuron => outputLayer.OutputNeurons,
                (neuron, output) => new { neuron, output });

            combos.ToList().ForEach(x => x.neuron.AddOutputNeuron(x.output));
        }

        public void ResetConnections()
        {
            foreach (var neuron in _hiddenNeurons)
            {
                neuron.ResetConnection();
            }
        }
    }
}
