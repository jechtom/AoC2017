using System;
using System.IO;
using System.Linq;

namespace AoC2017
{
    internal class Day01
    {
        public Day01()
        {
        }

        internal void Run()
        {
            Console.WriteLine("DAY 01");

            string input = File.ReadAllText("Day01Input.txt");

            CalculatePart1("1122");
            CalculatePart1("1111");
            CalculatePart1("1234");
            CalculatePart1("91212129");
            CalculatePart1(input);

            CalculatePart2("1212");
            CalculatePart2("1221");
            CalculatePart2("123425");
            CalculatePart2("123123");
            CalculatePart2("12131415");
            CalculatePart2(input);
        }

        private void CalculatePart1(string v) => Calculate(v, offset: 1);
        private void CalculatePart2(string v) => Calculate(v, offset: v.Length / 2);

        private void Calculate(string v, int offset)
        {
            int result = v.Select((current, index) =>
                (current: current, next: v[(index + offset) % v.Length])
            ).Where(item => item.current == item.next)
            .Sum(item => item.current - '0');

            Console.WriteLine($" For input {v} result is {result}");
        }
    }
}