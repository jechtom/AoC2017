using System;

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