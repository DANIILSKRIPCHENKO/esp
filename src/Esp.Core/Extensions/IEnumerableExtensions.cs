using Esp.Core.Common;

namespace Esp.Core.Extensions
{
    public static class IListExtensions
    { 
        public static IList<T> TakeRandom<T>(this IList<T> collection, int numberOfElements) => 
            collection
            .OrderBy(x => Guid.NewGuid())
            .Take(numberOfElements)
            .ToList();

        public static T FirstRandom<T>(this IList<T> collection) where T : class
        {
            var random = new Random();
            var index = random.Next(collection.Count);

            return collection[index];
        }

        public static T FirstRandomNotIn<T>(this IList<T> collection, IList<T> notInCollection) where T : class, IId
        {
            var filteredCollection = collection
                .Where(x => !notInCollection.Any(y => y.GetId() == x.GetId()))
                .ToList();

            var random = new Random();
            var index = random.Next(filteredCollection.Count);

            return filteredCollection[index];
        }
    }
}
