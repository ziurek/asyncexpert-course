using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Dotnetos.AsyncExpert.Homework.Module01.Benchmark
{
    [DisassemblyDiagnoser(exportCombinedDisassemblyReport: true)]
    public class FibonacciCalc
    {
        Dictionary<ulong, ulong> cache = new Dictionary<ulong, ulong>();

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
            var isCached = cache.TryGetValue(n, out var result);

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
            return 0;
        }

        public IEnumerable<ulong> Data()
        {
            yield return 15;
            yield return 35;
        }
    }
}
