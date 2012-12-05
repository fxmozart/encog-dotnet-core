namespace WCFNeuro.Extensions
{
    using System;
    using System.Collections.Generic;

    public static class MoreEnumerable
    {
        /// <summary>
        /// Immediately executes the given action on each element in the source sequence.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the sequence</typeparam>
        /// <param name="source">The sequence of elements</param>
        /// <param name="action">The action to execute on each element</param>

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            source.ThrowIfNull("source");
            action.ThrowIfNull("action");
            foreach (T element in source)
            {
                action(element);
            }
        }
       
            internal static void ThrowIfNull<T>(this T argument, string name) where T : class
            {
                if (argument == null)
                {
                    throw new ArgumentNullException(name);
                }
            }

            public static void ThrowIfNegative(this int argument, string name)
            {
                if (argument < 0)
                {
                    throw new ArgumentOutOfRangeException(name);
                }
            }

            public static void ThrowIfNonPositive(this int argument, string name)
            {
                if (argument <= 0)
                {
                    throw new ArgumentOutOfRangeException(name);
                }
            }
        }
    }

