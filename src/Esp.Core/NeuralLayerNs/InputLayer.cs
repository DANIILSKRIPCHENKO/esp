﻿using Esp.Core.NeuronNs;

namespace Esp.Core.NeuralLayerNs
{
    public class InputLayer : IInputLayer
    {
        private readonly IList<IInputNeuron> _inputNeurons;

        public InputLayer(IList<IInputNeuron> inputNeurons)
        {
            _inputNeurons = inputNeurons;
        }

        public IList<IInputNeuron> InputNeurons => _inputNeurons;
    }
}
