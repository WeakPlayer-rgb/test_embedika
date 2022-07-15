using System;
using System.Collections.Generic;
using System.Text;

namespace test_embedika
{
    public static class Extensions
    {
        public static int ParseInt(this char a)
        {
            return int.Parse(a.ToString());
        }

        public static string ParseToString(this IEnumerable<char> a)
        {
            var builder = new StringBuilder();
            foreach (var chr in a) builder.Append(chr);
            return builder.ToString();
        }
    }
}