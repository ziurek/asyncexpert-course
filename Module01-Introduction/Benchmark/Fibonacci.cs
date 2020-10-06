using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Dotnetos.AsyncExpert.Homework.Module01.Benchmark
{
    [MemoryDiagnoser]
    [DisassemblyDiagnoser(exportCombinedDisassemblyReport: true)]
    public class FibonacciCalc
    {
        // HOMEWORK:
        // 1. Write implementations for RecursiveWithMemoization and Iterative solutions
        // 2. Add MemoryDiagnoser to the benchmark
        // 3. Run with release configuration and compare results
        // 4. Open disassembler report and compare machine code
        // 
        // You can use the discussion panel to compare your results with other students

        [Benchmark(Baseline = true)]
        [ArgumentsSource(nameof(Data))]
        public ulong Recursive(ulong n)
        {
            if (n == 1 || n == 2) return 1;
            return Recursive(n - 2) + Recursive(n - 1);
        }

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public ulong RecursiveWithMemoization(ulong n)
        {
            var cache = new Dictionary<ulong, ulong>();
            var isCached = cache.TryGetValue(n, out _);

            if(!isCached)
            {
                if (n == 1 || n == 2)
                {
                    cache[n] = 1;
                }
                else
                {
                    cache[n] = RecursiveWithMemoization(n - 2) + RecursiveWithMemoization(n - 1);
                }
            }
            
            return cache[n];
        }
        
        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public ulong Iterative(ulong n)
        {
            ulong previous = 1, twoPrevious = 1, result = 1;

            for (ulong i = 3; i <= n; i++)
            {
                result = previous + twoPrevious;
                twoPrevious = previous;
                previous = result;
            }

            return result;
        }

        public IEnumerable<ulong> Data()
        {
            yield return 15;
            yield return 35;
        }
    }
}
