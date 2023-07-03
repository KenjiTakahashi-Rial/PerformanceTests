using System.Diagnostics;

namespace PerformanceTests
{
    public static class Program
    {
        private const int Runs = 10000;
        private const int Count = 10000;
        private const int RangeStart = int.MinValue;
        private const int RangeEnd = int.MaxValue;
        private const int DecimalPlaces = 2;

        private static readonly Random _random = new();

        public static void Main()
        {
            static int intGenerator(int _) => _random.Next(RangeStart, RangeEnd);
            static KeyValuePair<int, int> pairGenerator(int _)
            {
                var n = intGenerator(0);
                return new KeyValuePair<int, int>(n, n);
            }

            PerformanceTest<ArrayTests<int>, int[], int>(new ArrayTests<int>(), intGenerator);
            PerformanceTest<ListTests<int>, List<int>, int>(new ListTests<int>(), intGenerator);
            PerformanceTest<LinkedListTests<int>, LinkedList<int>, int>(new LinkedListTests<int>(), intGenerator);
            PerformanceTest<DictionaryTests<int, int>, Dictionary<int, int>, KeyValuePair<int, int>>(
                new DictionaryTests<int, int>(), pairGenerator
            );
        }

        private static double TimeIt(Action action)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            action();

            stopWatch.Stop();
            var ts = stopWatch.Elapsed;
            return ts.TotalNanoseconds;
        }

        private static void PerformanceTest<TC, E, T>(
            TC testClass, Func<int, T> randomGenerator
        ) where TC : ITestable<E, T> where E : IEnumerable<T>
        {
            var generated = testClass.GenerateRandom(Count, randomGenerator);

            var bestTimes = new double[Runs];
            var avgTimes = new double[Runs];
            var worstTimes = new double[Runs];

            foreach (var test in testClass.Tests)
            {
                for (var i = 0; i < Runs; i++)
                {
                    var first = generated.First();
                    var random = testClass.GetRandomValue(generated);
                    var last = generated.Last();

                    bestTimes[i] = TimeIt(() => test(generated, first));
                    avgTimes[i] = TimeIt(() => test(generated, random));
                    worstTimes[i] = TimeIt(() => test(generated, last));
                }

                var bestAvg = bestTimes.Average();
                var avgAvg = avgTimes.Average();
                var worstAvg = worstTimes.Average();

                Console.WriteLine($"{test.Method.Name} performance:");
                Console.WriteLine($"\tBest case: {bestAvg,DecimalPlaces} ns");
                Console.WriteLine($"\tAverage case: {avgAvg,DecimalPlaces} ns");
                Console.WriteLine($"\tWorst case: {worstAvg,DecimalPlaces} ns");
            }
        }
    }
}