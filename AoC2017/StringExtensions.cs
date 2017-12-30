using System;
using System.Collections.Generic;
using System.Text;

namespace AoC2017
{
    static class StringExtensions
    {
        public static string MakeShortIfTooLong(this string input)
        {
            if (input == null) return null;

            int limit = 10;
            if (input.Length <= limit) return input;
            return input.Substring(0, limit) + "...";
        }
    }
}
