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

        public static bool IsSorted<T>(this IList<T> collection) where T : IComparable<T>
        {
            for(var i=1; i < collection.Count; i++)
            {
                if (collection[i - 1].CompareTo(collection[i]) > 0)
                    return false;
            }

            return true;
        }

        public static (IList<T>, IList<T>) CrossOver<T>(
            this IList<T> collection,
            IList<T> collection2,
            int crossOverIndex)
        {
            if(collection.Count != collection2.Count)
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

            for (int i = crossOverIndex; i < collection.Count ; i++)
            {
                result1.Add(collection2[i]);
                result2.Add(collection[i]);
            }

            return (result1, result2);
        }

        public static (IList<T>, IList<T>) Half<T>(this IList<T> collection)
        {
            if (collection.Count % 2 != 0)
                throw new ArgumentException("Failed to split collection");

            var size = collection.Count;

            return (collection.Take(size / 2).ToList(), collection.TakeLast(size / 2).ToList());
        }
    }
}
