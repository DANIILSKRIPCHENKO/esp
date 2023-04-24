using Esp.Core.NeuronNs;

namespace Esp.Core.NeuralLayerNs
{
    /// <summary>
    /// Implementation of IOutputLayer
    /// </summary>
    public class OutputLayer : IOutputLayer
    {
        private IList<IOutputNeuron> _outputNeurons;

        public OutputLayer(IList<IOutputNeuron> outputNeurons)
        {
            _outputNeurons = outputNeurons;
        }

        #region OutputLayer implementation

        public IList<IOutputNeuron> OutputNeurons => _outputNeurons;

        public void ResetConnections()
        {
            foreach (var neuron in _outputNeurons)
            {
                neuron.Inputs.Clear();
            }
        }

        #endregion
    }
}
