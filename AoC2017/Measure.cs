using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AoC2017
{
    public class Measure
    {
        class Item
        {
            public string Name { get; set; }
            public int Repeats { get; set; }
            public TimeSpan Elapsed { get; set; }
            public float PerSecond => (int)(Repeats / Elapsed.TotalSeconds);
            public float PerSecondBaseLine { get; set; }
            public float BaseLineRatio => PerSecond / PerSecondBaseLine;
            public string BaseLineRatioFormatted => $"{(BaseLineRatio * 100):0}%";
        }

        private readonly string title;
        private List<Item> results;
        private int repeat = 1;
        private TimeSpan? inTime = null;

        public Measure(string title)
        {
            this.title = title;
            this.results = new List<Item>();
        }

        public Measure Repeat(int count)
        {
            repeat = count;
            return this;
        }

        public Measure InTime(int miliseconds)
        {
            inTime = TimeSpan.FromMilliseconds(miliseconds);
            return this;
        }

        public Measure Variant(string name, Action measureItem)
        {
            Stopwatch sw = Stopwatch.StartNew();
            int repeatsTotal = 0;
            do
            {
                for (int i = 0; i < repeat; i++)
                {
                    measureItem();
                    repeatsTotal++;
                }
            } while (sw.Elapsed < inTime);
            results.Add(new Item()
            {
                Name = name,
                Elapsed = sw.Elapsed,
                Repeats = repeatsTotal
            });
            return this;
        }

        public void PrintResults()
        {
            // set baseline - slowest
            float perSecondBaseLine = results.Min(r => r.PerSecond);
            results.ForEach(r => { r.PerSecondBaseLine = perSecondBaseLine; });

            Console.WriteLine($" Measure {title}:");
            bool showTime = inTime == null;
            foreach (var item in results)
            {
                if (showTime)
                {
                    Console.WriteLine($"  - {item.Name}: {item.BaseLineRatioFormatted}; {item.PerSecond}/s; {item.Repeats} in {item.Elapsed.TotalMilliseconds:0}ms");
                }
                else
                {
                    Console.WriteLine($"  - {item.Name}: {item.BaseLineRatioFormatted}; {item.PerSecond}/s");
                }
            }
        }
    }
}
