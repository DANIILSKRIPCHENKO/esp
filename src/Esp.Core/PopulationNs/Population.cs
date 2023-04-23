using Esp.Core.Extensions;
using Esp.Core.NeuronNs;

namespace Esp.Core.PopulationNs
{
    public class Population : IPopulation
    {
        private readonly Guid _id = Guid.NewGuid();
        private readonly int _initialSize;
        private IList<IHiddenNeuron> _neurons;

        public Population(IList<IHiddenNeuron> neurons)
        {
            _initialSize = neurons.Count;
            _neurons = neurons;
        }

        public Guid GetId() => _id;

        public IList<IHiddenNeuron> HiddenNeurons => _neurons;

        public IHiddenNeuron GetRandomNeuron() => _neurons.FirstRandom();

        public void Recombine()
        {
            var neuronsToRecombine = _neurons
                .OrderByDescending(neuron => neuron.Fitness)
                .Take(_neurons.Count/4)
                .ToList();

            var offsptingNeurons = new List<IHiddenNeuron>();

            for(var i = 1; i < neuronsToRecombine.Count; i++)
            {
                (var child1, var child2) = neuronsToRecombine[i - 1]
                    .Recombine(neuronsToRecombine[i]);

                offsptingNeurons.Add(child1);
                offsptingNeurons.Add(child2);
            }

            foreach(var offsptingNeuron in offsptingNeurons)
                _neurons.Add(offsptingNeuron);

            RemoveRedundantNeurons();
        }

        private void RemoveRedundantNeurons()
        {
            _neurons = _neurons
                .OrderByDescending(neuron => neuron.Fitness)
                .Take(_initialSize)
                .ToList();
        }
    }
}
