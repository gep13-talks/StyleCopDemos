using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StyleCopContrib.CustomRules.Tests.Rules
{
    [TestClass]
    public sealed class NamingTests : RuleTestBase
    {
        #region Constructors

        public NamingTests()
        {
        }

        #endregion

        #region Test Methods

        protected override void TestSetup()
        {
            this.SuppressRuleViolations(ContribRule.IndentUsingTabs);
        }

        [TestMethod]
        public void InternalClassWithNameNotMatchingFileName()
        {
            this.SetTargetFile(@"TargetFiles\Naming\FileNameMustMatchTypeName1.cs");

            this.AddExpectation(ContribRule.FileNameMustMatchTypeName, 10);

            this.Analyze();

            this.ValidateExpectations();
        }

        [TestMethod]
        public void EnumsFile()
        {
            this.SetTargetFile(@"TargetFiles\Naming\FileNameMustMatchTypeName2.cs");

            this.Analyze();

            this.ValidateExpectations();
        }

        [TestMethod]
        public void InternalClassWithAPrivateNestedClass()
        {
            this.SetTargetFile(@"TargetFiles\Naming\FileNameMustMatchTypeName3.cs");

            this.Analyze();

            this.ValidateExpectations();
        }

        [TestMethod]
        public void PublicStructWithNameMatchingFileName()
        {
            this.SetTargetFile(@"TargetFiles\Naming\FileNameMustMatchTypeName4.cs");

            this.Analyze();

            this.ValidateExpectations();
        }

        [TestMethod]
        public void InternalStructWithNameNotMatchingFileName()
        {
            this.SetTargetFile(@"TargetFiles\Naming\FileNameMustMatchTypeName5.cs");

            this.AddExpectation(ContribRule.FileNameMustMatchTypeName, 10);

            this.Analyze();

            this.ValidateExpectations();
        }

        [TestMethod]
        public void InternalClassWithGenericArgumentFileName()
        {
            this.SetTargetFile(@"TargetFiles\Naming\FileNameMustMatchTypeName6.cs");

            this.Analyze();

            this.ValidateExpectations();
        }

        [TestMethod]
        public void FileNameNotMatchingInterfaceName()
        {
            this.SetTargetFile(@"TargetFiles\Naming\FileNameNotMatchingInterfaceName.cs");

            this.AddExpectation(ContribRule.FileNameMustMatchTypeName, 5);

            this.Analyze();

            this.ValidateExpectations();
        }

        #endregion
    }
}