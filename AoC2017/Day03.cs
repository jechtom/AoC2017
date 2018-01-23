using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2017
{
    internal class Day03
    {
        public Day03()
        {
        }

        internal void Run()
        {
            Console.WriteLine("DAY 03");
            int[] inputs = new[] { 1, 12, 23, 1024, 312051 };
            foreach (var input in inputs)
            {
                int result = ComputePart1Var1(input);
                Console.WriteLine($" Part 1: For input {input} the result A is {result}");

                result = ComputePart1Var2(input);
                Console.WriteLine($" Part 1: For input {input} the result B is {result}");
            }

            for (int i = 1; i < 100_000; i++)
            {
                int r1 = ComputePart1Var1(i);
                int r2 = ComputePart1Var2(i);
                if (r1 != r2) throw new InvalidOperationException();
            }
            Console.WriteLine(" Check OK");

            foreach (var number in GetPart2Sequence().Take(30))
            {
                Console.Write($" {number};");
            }
            Console.WriteLine();

            int part2result = GetPart2Sequence().SkipWhile(n => n <= 312051).First();
            Console.WriteLine($" Part 2 result is {part2result}");
        }
        
        private IEnumerable<int> GetPart2Sequence()
        {
            var current = new LayerData(layer: 0);
            current[0] = 1;
            yield return 1;

            while (true)
            {
                LayerData inner = current;
                current = new LayerData(layer: current.Layer + 1);

                int innerIndex = -1;
                for (int index = 0; index < current.Size; index++)
                {
                    int sideIndex = index % current.SideSize;

                    bool isBeforeCorner = sideIndex == current.SideSize - 2;
                    bool isCorner = sideIndex == current.SideSize - 1;
                    bool isAfterCorner = sideIndex == 0;

                    if (!isCorner && !isAfterCorner)
                    {
                        innerIndex++;
                    }

                    int value = 0;

                    // same layer
                    value += current[index - 1];
                    value += current[index + 1];

                    if (isAfterCorner) value += current[index - 2];
                    if (isBeforeCorner) value += current[index + 2];

                    // inner layer
                    value += inner[innerIndex];

                    if(!isCorner)
                    {
                        if (!isBeforeCorner) value += inner[innerIndex + 1];
                        if (!isAfterCorner) value += inner[innerIndex - 1];
                    }

                    current[index] = value;
                    yield return value;
                }
            }
        }

        class LayerData
        {
            int[] numbers;

            public LayerData(int layer)
            {
                Layer = layer;
                Size = layer == 0 ? 1 : layer * 8;
                SideSize = Size / 4;
                numbers = new int[Size];
            }

            public int this[int index]
            {
                get => numbers[FixIndex(index)];
                set => numbers[FixIndex(index)] = value;
            }

            private int FixIndex(int index)
            {
                index %= Size;
                if (index < 0) index += Size;
                return index;
            }

            public int Layer { get; }
            public int Size { get; }
            public int SideSize { get; }
        }

        private int ComputePart1Var2(int input)
        {
            int layer = (int)Math.Ceiling((Math.Sqrt(input) - 1) / 2);
            if (layer == 0) return 0;
            double layerSize = layer * 8;
            double layerStart = layer * (layer - 1) * 4 + 2;
            double layerSideSize = layerSize / 4;
            double layerSideSizeHalf = layerSideSize / 2;

            double distance = Math.Abs(((input - layerStart) % layerSideSize) + 1 - layerSideSizeHalf);
            distance += layer;
            return (int)distance;
        }

        private int ComputePart1Var1(int input)
        {
            if (input == 1) return 0;

            int layer = 1;
            int number = 1;
            while(true)
            {
                int layerSize = layer * 8;
                int layerNumberEnd = number + layerSize;
                if(layerNumberEnd < input)
                {
                    number = layerNumberEnd;
                    layer++;
                    continue;
                }

                int layerSideSize = layerSize / 4;
                int layerSideSizeHalf = layerSideSize / 2;

                int index = input - (number + 1);

                int result = Math.Abs((index % layerSideSize) + 1 - (layerSideSizeHalf));
                result += layer;
                return result;
            }
        }
    }
}