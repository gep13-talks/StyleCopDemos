using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StyleCopContrib.CustomRules.Tests.Rules
{
    [TestClass]
    public class MaintainabilityTests : RuleTestBase
    {
        #region Test Methods

        protected override void TestSetup()
        {
            this.SuppressRuleViolations(ContribRule.IndentUsingTabs);
        }

        [TestMethod]
        public void MaximumLineLengthExceeded()
        {
            this.SetTargetFile(@"TargetFiles\Maintainability\MaximumLineLengthExceeded1.cs");

            this.AddExpectation(ContribRule.MaximumLineLengthExceeded, 8, 9, 14);

            this.Analyze();

            this.ValidateExpectations();
        }

        [TestMethod]
        public void ClassNameLengthExceeded()
        {
            this.SetTargetFile(@"TargetFiles\Maintainability\ClassNameLengthExceeded1.cs");

            this.AddExpectation(ContribRule.ClassNameLengthExceeded, 12);

            this.Analyze();

            this.ValidateExpectations();
        }

        [TestMethod]
        public void SingleReturnStatement()
        {
            this.SetTargetFile(@"TargetFiles\Maintainability\SingleReturnStatement1.cs");

            this.AddExpectation(ContribRule.SingleReturnStatement, 9, 24, 33);

            this.Analyze();

            this.ValidateExpectations();
        }

        [TestMethod]
        public void ReturnStatementOnlyInFunctions()
        {
            this.SetTargetFile(@"TargetFiles\Maintainability\ReturnStatementOnlyInFunctions1.cs");

            this.AddExpectation(ContribRule.ReturnStatementOnlyInFunctions, 9, 20, 29, 36);

            this.Analyze();

            this.ValidateExpectations();
        }

        #endregion
    }
}
