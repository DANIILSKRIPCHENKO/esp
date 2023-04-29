namespace Esp.Core.NeuronNs.Hidden
{
    public interface IHiddenNeuronBuilder
    {
        public IList<IHiddenNeuron> BuildHiddenNeurons(int number);
    }
}
