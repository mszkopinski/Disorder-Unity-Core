using System;
using System.Collections.Generic;

namespace Disorder.Unity.Core
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T element in source)
            {
                action.Invoke(element);
            }
        }
    }

    public static class StringExtensions
    {
        public static bool Contains(this string source, string toCheck, StringComparison stringComparison)
        {
            return source?.IndexOf(toCheck, stringComparison) >= 0;
        }
    }
}