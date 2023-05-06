using Ga.Core.Extensions;
using Ga.Core.NeuronNs.Hidden;

namespace Ga.Core.PopulationNs
{
    public class Population : IPopulation
    {
        private readonly Guid _id = Guid.NewGuid();
        private IList<IHiddenNeuron> _neurons;
        private bool _isTurnedOff = false;

        public Population(IList<IHiddenNeuron> neurons)
        {
            _neurons = neurons;
        }

        #region IPopulation implementation

        public Guid GetId() => _id;

        public IList<IHiddenNeuron> HiddenNeurons => _neurons;

        public bool IsTurnedOff { get => _isTurnedOff; set => _isTurnedOff = value; }

        public IHiddenNeuron GetRandomNeuron() => _neurons.FirstRandom();

        public void Recombine()
        {
            var neuronsToRecombine = _neurons
                .OrderByDescending(neuron => neuron.Fitness)
                .Take(_neurons.Count / 4)
                .ToList();

            var offsptingNeurons = new List<IHiddenNeuron>();

            for (var i = 1; i < neuronsToRecombine.Count; i++)
            {
                (var child1, var child2) = neuronsToRecombine[i - 1]
                    .Recombine(neuronsToRecombine[i]);

                offsptingNeurons.Add(child1);
                offsptingNeurons.Add(child2);
            }

            _neurons = _neurons
                .OrderByDescending(neuron => neuron.Fitness)
                .ToList()
                .ReplaceFromLast(offsptingNeurons);
        }

        public void BurstMutation()
        {
            var bestNeuron = _neurons
                .OrderByDescending(neuron => neuron.Fitness)
                .First();

            var newNeurons = bestNeuron.BurstMutate(_neurons.Count / 4);

            _neurons = _neurons
                .OrderByDescending(neuron => neuron.Fitness)
                .ToList()
                .ReplaceFromLast(newNeurons);
        }

        public void ResetFitnesses()
        {
            foreach (var neuron in _neurons)
                neuron.ResetFitness();
        }

        #endregion
    }
}
