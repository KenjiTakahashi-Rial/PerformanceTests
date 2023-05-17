namespace PerformanceTests
{
    public class ListTests<T> : ITestable<List<T>, T> where T : IEquatable<T>
    {
        private readonly Random _random = new();

        public List<T> GenerateRandom(int count, Func<int, T> randomGenerator)
        {
            return Enumerable.Range(0, count)
                .Select(randomGenerator)
                .ToList();
        }

        public T GetRandomValue(List<T> source)
        {
            return source[_random.Next(0, source.Count)];
        }

        public Action<List<T>, T>[] Tests { get; } = new Action<List<T>, T>[]
        {
            ListFor,
            ListForEach,
            ListFirstOrDefault,
        };

        private static void ListFor(List<T> list, T value)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Equals(value))
                {
                    break;
                }
            }
        }

        private static void ListForEach(List<T> list, T value)
        {
#pragma warning disable S3267 // Loops should be simplified with "LINQ" expressions
            foreach (var element in list)
            {
                if (element.Equals(value))
                {
                    break;
                }
            }
#pragma warning restore S3267 // Loops should be simplified with "LINQ" expressions
        }

        private static void ListFirstOrDefault(List<T> list, T value)
        {
            _ = list.FirstOrDefault(value);
        }
    }
}