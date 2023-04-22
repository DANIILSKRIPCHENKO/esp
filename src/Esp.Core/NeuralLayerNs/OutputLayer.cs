using Esp.Core.NeuronNs;

namespace Esp.Core.NeuralLayerNs
{
    public class OutputLayer : IOutputLayer
    {
        private IList<IOutputNeuron> _outputNeurons;

        public OutputLayer(IList<IOutputNeuron> outputNeurons)
        {
            _outputNeurons = outputNeurons;
        }

        public IList<IOutputNeuron> OutputNeurons => _outputNeurons;

        public void ResetConnections()
        {
            foreach (var neuron in _outputNeurons)
            {
                neuron.Inputs.Clear();
            }
        }
    }
}
