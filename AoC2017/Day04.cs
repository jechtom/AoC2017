using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AoC2017
{
    internal class Day04
    {
        public Day04()
        {
        }

        internal void Run()
        {
            Console.WriteLine("DAY 04");
            string[] inputLines = File.ReadAllLines("Day04Input.txt");

            int result = inputLines.Where(IsLineValidPart1).Count();
            Console.WriteLine($" Part1 result: {result}");

            int result2naive = inputLines.Where(IsLineValidPart2Naive).Count();
            Console.WriteLine($" Part2 result: {result2naive}");

            int result2optimized = inputLines.Where(IsLineValidPart2Optimized).Count();
            if(result2optimized != result2naive) throw new InvalidOperationException("Results should be same");

            new Measure("Part2 algorithms")
                .InTime(miliseconds: 100)
                .Variant("Naive", () => inputLines.Where(IsLineValidPart2Naive).Count())
                .Variant("Optimized", () => inputLines.Where(IsLineValidPart2Optimized).Count())
                .PrintResults();
        }

        private bool IsLineValidPart1(string line)
        {
            return !line.Split(' ')
                .GroupBy(word => word)
                .Any(gr => gr.Count() > 1);
        }

        private bool IsLineValidPart2Naive(string line)
        {
            return !line.Split(' ')
                .Select(word => new string(word.OrderBy(c => c).ToArray()))
                .GroupBy(word => word)
                .Any(gr => gr.Count() > 1);
        }

        private bool IsLineValidPart2Optimized(string line)
        {
            var hashSet = new HashSet<string>(comparer);
            foreach (var item in line.Split(' '))
            {
                if (!hashSet.Add(item)) return false;
            }
            return true;
        }

        AnagramComparer comparer = new AnagramComparer();

        class AnagramComparer : IEqualityComparer<string>
        {
            public bool Equals(string x, string y)
            {
                if (x.Length != y.Length) return false;
                return x.OrderBy(c => c).SequenceEqual(y.OrderBy(c => c));
            }

            public int GetHashCode(string obj)
            {
                int result = 0;
                for (int i = 0; i < obj.Length; i++)
                {
                    result += obj[i];
                }
                return result;
            }
        }
    }
}