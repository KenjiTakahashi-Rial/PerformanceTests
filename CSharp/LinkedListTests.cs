namespace PerformanceTests
{
    public class LinkedListTests<T> : ITestable<LinkedList<T>, T> where T : IEquatable<T>
    {
        private readonly Random _random = new();

        public LinkedList<T> GenerateRandom(int count, Func<int, T> randomGenerator)
        {
            return new LinkedList<T>(
                Enumerable.Range(0, count)
                    .Select(randomGenerator)
                    .ToList()
            );
        }

        public T GetRandomValue(LinkedList<T> source)
        {
            return source.ToList()[_random.Next(0, source.Count)];
        }

        public Action<LinkedList<T>, T>[] Tests { get; } = new Action<LinkedList<T>, T>[]
        {
            LinkedListIterate,
            LinkedListFirstOrDefault,
        };

        private static void LinkedListIterate(LinkedList<T> linkedList, T value)
        {
            var currentNode = linkedList.First;

            while (currentNode != null && !currentNode.Value.Equals(value))
            {
                currentNode = currentNode.Next;
            }
        }

        private static void LinkedListFirstOrDefault(LinkedList<T> linkedList, T value)
        {
            _ = linkedList.FirstOrDefault(value);
        }
    }
}