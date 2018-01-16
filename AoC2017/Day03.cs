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
                int result = ComputePart1(input);
                Console.WriteLine($" Part 1: For input {input} the result is {result}");
            }
        }

        private int ComputePart1(int input)
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