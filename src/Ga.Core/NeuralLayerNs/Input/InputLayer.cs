using Ga.Core.NeuronNs.Input;
using Newtonsoft.Json;

namespace Ga.Core.NeuralLayerNs.Input
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

        [JsonConstructor]
        public InputLayer(IList<IInputNeuron> inputNeurons)
        {
            _inputNeurons = inputNeurons;
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
