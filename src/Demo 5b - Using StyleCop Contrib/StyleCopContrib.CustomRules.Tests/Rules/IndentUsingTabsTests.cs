using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StyleCopContrib.CustomRules.Tests.Rules
{
    [TestClass]
    public sealed class IndentUsingTabsTests : RuleTestBase
    {
        #region Constructors

        public IndentUsingTabsTests()
        {
        }

        #endregion

        #region Test Methods

        [TestMethod]
        public void Test()
        {
            this.SetTargetFile(@"TargetFiles\IndentUsingTabs1.cs");

            this.AddExpectation(ContribRule.IndentUsingTabs, 16, 21, 24);

            this.Analyze();

            this.ValidateExpectations();
        }

        #endregion
    }
}