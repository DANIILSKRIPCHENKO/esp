namespace Esp.Core.Extensions
{
    public static class IListExtensions
    {
        public static IList<T> TakeRandom<T>(this IList<T> collection, int numberOfElements)
        {
            var result = new List<T>();

            while(result.Count != numberOfElements)
            {
                var random = new Random();
                var index = random.Next(collection.Count);
                result.Add(collection[index]);
                collection.Remove(collection[index]);
            }

            return result;
        }

        public static IList<T> TakeRandomNotIn<T>(
            this IList<T> collection, 
            IList<T> notInCollection, 
            int numberOfElements) where T : class
        {
            var filteredCollection = collection
                .Where(x => !notInCollection.Any(y => x == y))
                .ToList();

            var result = new List<T>();

            while (result.Count != numberOfElements)
            {
                var random = new Random();
                var index = random.Next(filteredCollection.Count);
                result.Add(filteredCollection[index]);
                filteredCollection.Remove(filteredCollection[index]);
            }

            return result;
        }

        public static T FirstRandom<T>(this IList<T> collection) where T : class
        {
            var random = new Random();
            var index = random.Next(collection.Count);

            return collection[index];
        }

        public static T FirstRandomNotIn<T>(this IList<T> collection, IList<T> notInCollection) where T : class
        {
            var filteredCollection = collection
                .Where(x => !notInCollection.Any(y => y == x))
                .ToList();

            var random = new Random();
            var index = random.Next(filteredCollection.Count);

            return filteredCollection[index];
        }

        public static int NextIndex<T>(this IList<T> collection) where T : class
        {
            if (!collection.Any())
                return 0;

            return collection.IndexOf(collection.Last()) + 1;
        }
    }
}
