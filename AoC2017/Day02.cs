using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2017
{
    internal class Day02
    {
        public Day02()
        {
        }

        internal void Run()
        {
            Console.WriteLine("DAY 02");

            string inputTest = File.ReadAllText("Day02InputTest.txt");
            string inputTest2 = File.ReadAllText("Day02InputTest2.txt");
            string input = File.ReadAllText("Day02Input.txt");

            ProcessPart1(inputTest);
            ProcessPart1(input);

            ProcessPart2(inputTest2);
            ProcessPart2(input);
        }

        private IEnumerable<int[]> ParseInput(string input) => input
            .Split(Environment.NewLine)
            .Select(line => line.Split('\t', ' ').Select(int.Parse).ToArray());

        private void ProcessPart1(string input)
        {
            int result = ParseInput(input)
                .Select(numbers => numbers.Max() - numbers.Min())
                .Sum();

            Console.WriteLine($" Result of part 1 is: {result}");
        }

        private void ProcessPart2(string input)
        {
            int result = ParseInput(input)
                .Select(FindPart2Numbers)
                .Sum();

            Console.WriteLine($" Result of part 2 is: {result}");
        }

        private int FindPart2Numbers(int[] numbers)
        {
            for (int i = 0; i < numbers.Length - 1; i++)
            {
                int n1 = numbers[i];
                for (int j = i + 1; j < numbers.Length; j++)
                {
                    int n2 = numbers[j];

                    if(n1 > n2)
                    {
                        if (n1 % n2 == 0) return n1 / n2;
                    }
                    else
                    {
                        if (n2 % n1 == 0) return n2 / n1;
                    }
                }
            }

            throw new InvalidOperationException("Invalid input.");
        }
    }
}