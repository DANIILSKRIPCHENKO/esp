﻿using Esp.Core.ActivationFunction;
using Esp.Core.InputFunction;
using Esp.Core.SynapseNs;

namespace Esp.Core.NeuronNs
{
    public class OutputNeuron : IOutputNeuron
    {
        private readonly Guid _id = Guid.NewGuid();
        private readonly IList<ISynapse> _inputs = new List<ISynapse>();
        private readonly IInputFunction _inputFunction;
        private readonly IActivationFunction _activationFunction;

        public OutputNeuron(
            IActivationFunction activationFunction,
            IInputFunction inputFunction)
        {
            _inputFunction = inputFunction;
            _activationFunction = activationFunction;
        }

        public IList<ISynapse> Inputs { get => _inputs; }

        public double CalculateOutput()
        {
            var input = _inputFunction.CalculateInput(_inputs);

            var output = _activationFunction.CalculateOutput(input);

            return output;
        }

        public Guid GetId() => _id;
    }
}
