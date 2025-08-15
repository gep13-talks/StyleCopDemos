using System;

namespace irobyx.SimpleDb
{
    public static class ConversionExtensions
    {
        private const int Int32Length = 10;

        public static string ConvertForSimpleDb(this DateTime value)
        {
            return value.ToString("yyyy-MM-ddTHH:mm:ss");
        }

        public static string ConvertForSimpleDb(this int value)
        {
            //return value.ToString().PadLeft(int.MaxValue.ToString().Length, '0');
            return value.ToString().PadLeft(Int32Length, '0');
        }

        public static string ConvertForSimpleDb(this decimal value)
        {
            return value.ToString().PadLeft(decimal.MaxValue.ToString().Length, '0');
        }

        public static string ConvertForSimpleDb(this bool value)
        {
            return value ? "1" : "0";
        }
    }
}