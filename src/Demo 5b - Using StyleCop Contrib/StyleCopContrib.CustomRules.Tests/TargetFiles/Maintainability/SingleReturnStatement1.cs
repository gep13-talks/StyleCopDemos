using System;

namespace StyleCopContrib.CustomRules.Tests.TargetFiles.Maintainability
{
    class SingleReturnStatement1
    {
        private object a;

        public int Bad(int input)
        {
            if (input > 0) return 1;
            if (input < 0) return -1;
            return 0;
        }

        public int Good(int input)
        {
            var result = 0;
            if (input > 0) result = 1;
            if (input < 0) result = -1;
            return result;
        }

        public static int StaticBad(int input)
        {
            if (input > 0) return 1;
            if (input < 0) return -1;
            return 0;
        }

        public object BadProperty
        {
            get
            {
                if (a == null)
                {
                    a = "Hello";
                    return a;
                }
                return a;
            }
        }
    }
}
