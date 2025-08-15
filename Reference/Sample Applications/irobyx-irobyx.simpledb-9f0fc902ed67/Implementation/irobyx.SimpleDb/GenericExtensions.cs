using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace irobyx.SimpleDb
{ 
    public static class GenericExtensions
    {
        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TR"></typeparam>
        /// <param name="obj">The object</param>
        /// <param name="expr">The expression</param>
        /// <param name="fullyQualified">if set to <c>true</c> [fully qualified].</param>
        /// <returns></returns>
        public static string GetPropertyName<T, TR>(this T obj, Expression<Func<T, TR>> expr, bool fullyQualified)
        {
            if (fullyQualified)
            {
                var outerMostMemberExpression = (MemberExpression)(expr.Body);
                var memberExpressions =
                    Iterate(memberExpression => memberExpression.Expression as MemberExpression,
                    outerMostMemberExpression)
                .TakeWhile(x => x != null)
                .Aggregate(string.Empty, (left, right) => right.Member.Name + "." + left);
                return memberExpressions.Substring(0, memberExpressions.Length - 1);
            }
            return ((MemberExpression)(expr.Body)).Member.Name;
        }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TR"></typeparam>
        /// <param name="obj">The object</param>
        /// <param name="expr">The expression</param>
        /// <returns></returns>
        public static string GetPropertyName<T, TR>(this T obj, Expression<Func<T, TR>> expr)
        {
            return ((MemberExpression)(expr.Body)).Member.Name;
        }

        /// <summary>
        /// Iterates the specified func.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func">The func.</param>
        /// <param name="initialValue">The initial value.</param>
        /// <returns></returns>
        private static IEnumerable<T> Iterate<T>(Func<T, T> func, T initialValue)
        {
            var value = initialValue;
            while (true)
            {
                yield return value;
                value = func(value);
            }
        } 
    }
}
