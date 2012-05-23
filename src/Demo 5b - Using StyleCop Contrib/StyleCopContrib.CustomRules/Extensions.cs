using System;
using System.Collections.Generic;

namespace StyleCopContrib.CustomRules
{
    /// <summary>
    /// Methods extensions class for the project.
    /// </summary>
    public static class Extensions
    {
        #region Methods

        /// <summary>
        /// Execute the action over the enumeration.
        /// </summary>
        /// <typeparam name="T">The enumerable type.</typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="action">The action.</param>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
            {
                action(item);
            }
        }

        /// <summary>
        /// Fors the each pair.
        /// </summary>
        /// <typeparam name="T">The enumerable type.</typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="action">The action.</param>
        public static void ForEachPair<T>(this IEnumerable<T> enumerable, Action<T, T> action)
        {
            var last = default(T);
            var isFirst = true;

            foreach (var item in enumerable)
            {
                if (!isFirst) action(last, item);

                last = item;
                isFirst = false;
            }
        }

        #endregion
    }
}