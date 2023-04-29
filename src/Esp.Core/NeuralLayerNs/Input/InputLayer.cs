using Esp.Core.NeuronNs.Input;

namespace Esp.Core.NeuralLayerNs.Input
{
    /// <summary>
    /// IInputLayer interface implementation
    /// </summary>
    public class InputLayer : IInputLayer
    {
        private IList<IInputNeuron> _inputNeurons;

        public InputLayer(IInputNeuronBuilder inputNeuronBuilder)
        {
            _inputNeurons = inputNeuronBuilder.BuildInputNeurons();
        }

        #region IInputLayer implementation

        public IList<IInputNeuron> InputNeurons => _inputNeurons;

        public void ResetConnections()
        {
            foreach (var neuron in _inputNeurons)
            {
                neuron.Outputs.Clear();
            }

        }

        #endregion
    }
}
