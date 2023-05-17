namespace PerformanceTests
{
    public class DictionaryTests<K, V> : ITestable<Dictionary<K, V>, KeyValuePair<K, V>> where K : notnull
    {
        private readonly Random _random = new();

        public Dictionary<K, V> GenerateRandom(int count, Func<int, KeyValuePair<K, V>> randomGenerator)
        {
            return Enumerable.Range(0, count)
                .Select(randomGenerator)
                .ToDictionary(p => p.Key, p => p.Value);
        }

        public KeyValuePair<K, V> GetRandomValue(Dictionary<K, V> source)
        {
            return source.ElementAt(_random.Next(0, source.Count));
        }

        public Action<Dictionary<K, V>, KeyValuePair<K, V>>[] Tests { get; } = new Action<Dictionary<K, V>, KeyValuePair<K, V>>[]
        {
            DictionaryGet,
            DictionaryContainsAndGet,
            DictionaryTryGet,
        };

        private static void DictionaryGet(Dictionary<K, V> dictionary, KeyValuePair<K, V> pair)
        {
            _ = dictionary[pair.Key];
        }

        private static void DictionaryContainsAndGet(Dictionary<K, V> dictionary, KeyValuePair<K, V> pair)
        {
            if (dictionary.ContainsKey(pair.Key))
            {
                _ = dictionary[pair.Key];
            }
        }

        private static void DictionaryTryGet(Dictionary<K, V> dictionary, KeyValuePair<K, V> pair)
        {
            dictionary.TryGetValue(pair.Key, out V _);
        }
    }
}