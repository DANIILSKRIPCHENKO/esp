namespace Ga.Core.Extensions
{
    /// <summary>
    /// Provides extension methods for IList collection
    /// </summary>
    public static class IListExtensions
    {
        /// <summary>
        /// Returns N random unique elements from collection, 
        /// which are not in input collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="notInCollection"></param>
        /// <param name="numberOfElements"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns first random element of collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static T FirstRandom<T>(this IList<T> collection) where T : class
        {
            var random = new Random();
            var index = random.Next(collection.Count);

            return collection[index];
        }

        /// <summary>
        /// Returns last + 1 index of collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static int NextIndex<T>(this IList<T> collection) where T : class
        {
            if (!collection.Any())
                return 0;

            return collection.IndexOf(collection.Last()) + 1;
        }

        /// <summary>
        /// Return true if collection is ascending
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static bool IsAscending<T>(this IList<T> collection) where T : IComparable<T>
        {
            if (!collection.Any() || collection.Count == 1)
                throw new ArgumentException("Collection should contatin more then one elements");

            for (var i = 1; i < collection.Count; i++)
            {
                if (collection[i - 1].CompareTo(collection[i]) >= 0)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Returns two collections after performing crossover operation
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="collection2"></param>
        /// <param name="crossOverIndex"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static (IList<T>, IList<T>) CrossOver<T>(
            this IList<T> collection,
            IList<T> collection2,
            int crossOverIndex)
        {
            if (collection.Count != collection2.Count)
                throw new ArgumentException("Collections should be the same size");

            if (crossOverIndex > collection.IndexOf(collection.Last()))
                throw new ArgumentException("CrossOverIndex should be less " +
                    "then index of last element in collection");

            var result1 = new List<T>();
            var result2 = new List<T>();

            for (var i = 0; i < crossOverIndex; i++)
            {
                result1.Add(collection[i]);
                result2.Add(collection2[i]);
            }

            for (int i = crossOverIndex; i < collection.Count; i++)
            {
                result1.Add(collection2[i]);
                result2.Add(collection[i]);
            }

            return (result1, result2);
        }


        /// <summary>
        /// Replaces last elements in collection with elements, passed in a parameter
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="collectionToPaste"></param>
        /// <returns></returns>
        public static IList<T> ReplaceFromLast<T>(this IList<T> collection, IList<T> collectionToPaste)
        {
            var result = new List<T>();

            result.AddRange(collection.Take(collection.Count - collectionToPaste.Count));
            result.AddRange(collectionToPaste);

            return result;
        }
    }
}
