namespace Esp.Core.Extensions
{
    public static class IEnumerableExtensions
    { 
        public static IEnumerable<T> TakeRandom<T>(this IEnumerable<T> collection, int numberOfElements)
        {
            return collection.OrderBy(x => Guid.NewGuid()).Take(numberOfElements);
        }

        public static T FirstRandom<T>(this IEnumerable<T> collection)
        {
            return collection.OrderBy(x => Guid.NewGuid()).First();
        }
    }
}
