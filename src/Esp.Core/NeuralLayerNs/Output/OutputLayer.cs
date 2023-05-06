using Ga.Core.NeuronNs.Output;

namespace Ga.Core.NeuralLayerNs.Output
{
    /// <summary>
    /// Implementation of IOutputLayer
    /// </summary>
    public class OutputLayer : IOutputLayer
    {
        private IList<IOutputNeuron> _outputNeurons;

        public OutputLayer(IOutputNeuronBuilder outputNeuronBuilder)
        {
            _outputNeurons = outputNeuronBuilder.BuildOutputNeurons();
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
