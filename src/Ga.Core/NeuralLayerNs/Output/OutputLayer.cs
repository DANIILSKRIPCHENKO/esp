using Ga.Core.NeuronNs.Output;
using Newtonsoft.Json;

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

        [JsonConstructor]
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
