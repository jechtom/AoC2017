using System;
using System.IO;
using System.Linq;

namespace AoC2017
{
    internal class Day05
    {
        public Day05()
        {
        }

        internal void Run()
        {
            Console.WriteLine("DAY 05");

            int[] inputTest = File.ReadAllLines("Day05InputTest.txt").Select(int.Parse).ToArray();
            int[] input = File.ReadAllLines("Day05Input.txt").Select(int.Parse).ToArray();

            CalculateResultPart1(inputTest);
            CalculateResultPart1(input);

            CalculateResultPart2(inputTest);
            CalculateResultPart2(input);
        }

        private void CalculateResultPart1(int[] input)
        {
            var instance = new Memory(input);
            instance.RunToEnd();
            Console.WriteLine($" Part1 - steps: {instance.Counter}");
        }

        private void CalculateResultPart2(int[] input)
        {
            var instance = new Memory(input);
            instance.IsPart2BehaviorSet = true;
            instance.RunToEnd();
            Console.WriteLine($" Part2 - steps: {instance.Counter}");
        }

        class Memory
        {
            private int[] numbers;

            public Memory(int[] numbers)
            {
                this.numbers = numbers.ToArray();
            }

            public bool IsPart2BehaviorSet { get; set; }

            public int Position { get; private set; }
            public int Counter { get; private set; }
            public bool IsEnded => Position < 0 || Position >= numbers.Length;

            public void Step()
            {
                if (IsEnded) throw new InvalidOperationException("Already ended.");

                int offset = numbers[Position];

                int newPosition = Position + offset;

                if (IsPart2BehaviorSet && offset >= 3)
                {
                    numbers[Position]--;
                }
                else
                {
                    numbers[Position]++;
                }

                Position = newPosition;
                Counter++;
            }

            public void RunToEnd()
            {
                while (!IsEnded) Step();
            }
        }
    }
}