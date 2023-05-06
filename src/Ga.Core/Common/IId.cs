namespace Ga.Core.Common
{
    /// <summary>
    /// Represents interface of entities with identifier
    /// </summary>
    public interface IId
    {
        /// <summary>
        /// Returns identifier of an entity
        /// </summary>
        /// <returns></returns>
        public Guid GetId();
    }
}
