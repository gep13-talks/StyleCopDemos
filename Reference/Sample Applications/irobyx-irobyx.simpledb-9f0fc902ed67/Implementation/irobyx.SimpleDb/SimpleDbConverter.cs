using System;
using System.Collections.Generic;
using System.Linq;

using irobyx.SimpleDb.Interfaces;

namespace irobyx.SimpleDb
{
    public class SimpleDbConverter : ISimpleDbConverter
    {
        /// <summary>
        /// Supported types include String, Int32, Decimal, DateTime, Boolean, Enum, IEnumerable(string)
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns>Formatted string for persistence to SimpleDb</returns>
        public string ConvertToSimpleDb(Type type, object value)
        {
            if (type.Name == TypeCode.String.ToString())
            {
                return value == null ? string.Empty : value.ToString();
            }
            if (type.Name == TypeCode.Int32.ToString())
            {
                return Convert.ToInt32(value).ConvertForSimpleDb();
            }
            if (type.Name == TypeCode.DateTime.ToString())
            {
                return Convert.ToDateTime(value).ConvertForSimpleDb();
            }
            if (type.Name == TypeCode.Decimal.ToString())
            {
                return Convert.ToDecimal(value).ConvertForSimpleDb();
            }
            if (type.Name == TypeCode.Boolean.ToString())
            {
                return Convert.ToBoolean(value).ConvertForSimpleDb();
            }
            if (type.IsEnum)
            {
                return value.ToString();
            }
            if (IsIEnumerableOfT(type))
            {
                var theList = value as IEnumerable<string>;
                if (theList != null)
                {
                    return theList.Any() ? string.Join(",", theList) : string.Empty;
                }
            }
            throw new ArgumentOutOfRangeException(type.Name);
        }

        /// <summary>
        /// Supported types include String, Int32, Decimal, DateTime, Boolean, Enum, IEnumerable(string)
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public dynamic ConvertFromSimpleDb(Type type, string value)
        {
            if (type.Name == TypeCode.String.ToString())
            {
                return value;
            }
            if (type.Name == TypeCode.Int32.ToString())
            {
                return Convert.ToInt32(value);
            }
            if (type.Name == TypeCode.DateTime.ToString())
            {
                return Convert.ToDateTime(value);
            }
            if (type.Name == TypeCode.Decimal.ToString())
            {
                return Convert.ToDecimal(value);
            }
            if (type.Name == TypeCode.Boolean.ToString())
            {
                return Convert.ToBoolean(int.Parse(value));
            }
            if (type.IsEnum)
            {
                return Enum.Parse(type, value);
            }
            if (IsIEnumerableOfT(type))
            {
                var theList = new List<string>();
                if(!string.IsNullOrWhiteSpace(value))
                {
                    theList.AddRange(value.Split(','));                    
                }
                return theList;
            }
            throw new ArgumentOutOfRangeException(type.Name);
        }

        private static bool IsIEnumerableOfT(Type type)
        {
            if (type.Name.Equals("IEnumerable`1", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return type.GetInterfaces().Any(t => t.IsGenericType && (t.GetGenericTypeDefinition() == typeof(IEnumerable<>)));
        }

    }
}