using System.Collections;

namespace PerformanceTests
{
    public interface ITestable<E, T> where E : IEnumerable<T>
    {
        E GenerateRandom(int count, Func<int, T> randomGenerator);

        T GetRandomValue(E source);

        Action<E, T>[] Tests { get; }
    }
}