namespace PerformanceTests
{
    public class ArrayTests<T> : ITestable<T[], T> where T : IEquatable<T>
    {
        private readonly Random _random = new();

        public T[] GenerateRandom(int count, Func<int, T> randomGenerator)
        {
            return Enumerable.Range(0, count)
                .Select(randomGenerator)
                .ToArray();
        }

        public T GetRandomValue(T[] array)
        {
            return array[_random.Next(0, array.Length)];
        }

        public Action<T[], T>[] Tests { get; } = new Action<T[], T>[]
        {
            ArrayFor,
            ArrayForEach,
            ArrayFirstOrDefault,
        };

        private static void ArrayFor(T[] array, T value)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].Equals(value))
                {
                    break;
                }
            }
        }

        private static void ArrayForEach(T[] array, T value)
        {
#pragma warning disable S3267 // Loops should be simplified with "LINQ" expressions
            foreach (var element in array)
            {
                if (element.Equals(value))
                {
                    break;
                }
            }
#pragma warning restore S3267 // Loops should be simplified with "LINQ" expressions
        }

        private static void ArrayFirstOrDefault(T[] array, T value)
        {
            _ = array.FirstOrDefault(value);
        }
    }
}