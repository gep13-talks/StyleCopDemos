using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StyleCopContrib.CustomRules.Tests.Rules
{
    [TestClass]
    public sealed class NoTrailingWhitespaceTests : RuleTestBase
    {
        #region Constructors

        public NoTrailingWhitespaceTests()
        {
        }

        #endregion

        #region Test Methods

        protected override void TestSetup()
        {
            this.SuppressRuleViolations(ContribRule.IndentUsingTabs);
        }

        [TestMethod]
        public void Test()
        {
            this.SetTargetFile(@"TargetFiles\NoTrailingWhitespace1.cs");

            this.AddExpectation(ContribRule.NoTrailingWhiteSpace, 11, 20, 25);

            this.Analyze();

            this.ValidateExpectations();
        }

        #endregion
    }
}