using System;

namespace StyleCopContrib.CustomRules.Tests.TargetFiles.Maintainability
{
    class ReturnStatementOnlyInFunctions1
    {
        private object badProperty;

        public ReturnStatementOnlyInFunctions1()
        {
            return;
        }

        public object BadProperty
        {
            get
            {
                return this.badProperty;
            }
            set
            {
                this.badProperty = value;
                return;
            }
        }

        public object BadProperty2
        {
            set
            {
                this.badProperty = value;
                return;
            }
        }

        public void BadProcedure()
        {
            return;
        }
    }
}
