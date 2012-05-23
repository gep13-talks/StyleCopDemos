using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StyleCopContrib.CustomRules.Tests.Rules
{
    [TestClass]
    public class UsingDirectiveTests : RuleTestBase
    {
        #region Constructors

        public UsingDirectiveTests()
        {
            this.SetBaseProjectSettingFile("Settings.StyleCop");
        }

        #endregion

        #region Test Methods

        protected override void TestSetup()
        {
            this.SuppressRuleViolations(ContribRule.IndentUsingTabs);
            this.SetAnalyzerSetting("UsingDirectiveGroups", "System;Microsoft;*;StyleCopContrib");
        }

        [TestMethod]
        public void UsingDirectiveMustBeSortedAlphabeticallyByGroup1()
        {
            this.SetTargetFile(@"TargetFiles\Using\UsingDirectiveMustBeSortedAlphabeticallyByGroup1.cs");

            this.Analyze();

            this.ValidateExpectations();
        }

        [TestMethod]
        public void UsingDirectiveMustBeSortedAlphabeticallyByGroup2()
        {
            this.SetTargetFile(@"TargetFiles\Using\UsingDirectiveMustBeSortedAlphabeticallyByGroup2.cs");

            this.AddExpectation(ContribRule.UsingDirectiveMustBeSortedAlphabeticallyByGroup, 3);
            this.AddExpectation(ContribRule.UsingDirectiveGroupMustFollowGivenOrder, 12, 17);

            this.Analyze();

            this.ValidateExpectations();
        }

        /// <summary>
        /// Multiple using prefix per group
        /// </summary>
        [TestMethod]
        public void UsingDirectiveMustBeSortedAlphabeticallyByGroup3()
        {
            this.SetAnalyzerSetting("UsingDirectiveGroups", "System;StyleCopContrib;Microsoft,*");

            this.SetTargetFile(@"TargetFiles\Using\UsingDirectiveMustBeSortedAlphabeticallyByGroup3.cs");

            this.Analyze();

            this.ValidateExpectations();
        }

        /// <summary>
        /// Multiple using prefix per group
        /// </summary>
        [TestMethod]
        public void UsingDirectiveMustBeSortedAlphabeticallyByGroup4()
        {
            this.SetAnalyzerSetting("UsingDirectiveGroups", "System;Microsoft;StyleCopContrib,*");
            this.SetAnalyzerSetting("AliasShouldBeLast", false);

            this.SetTargetFile(@"TargetFiles\Using\UsingDirectiveMustBeSortedAlphabeticallyByGroup4.cs");

            this.Analyze();

            this.ValidateExpectations();
        }

        /// <summary>
        /// Multiple using prefix per group
        /// </summary>
        [TestMethod]
        public void UsingDirectiveMustBeSortedAlphabeticallyByGroup5()
        {
            this.SetAnalyzerSetting("UsingDirectiveGroups", "System;Microsoft;StyleCopContrib,*");
            this.SetAnalyzerSetting("AliasShouldBeLast", true);

            this.SetTargetFile(@"TargetFiles\Using\UsingDirectiveMustBeSortedAlphabeticallyByGroup5.cs");

            this.Analyze();

            this.ValidateExpectations();
        }

        /// <summary>
        /// System is present but no as the first using directive
        /// </summary>
        [TestMethod]
        public void FirstUsingDirectiveMustBeSystem1()
        {
            this.SetTargetFile(@"TargetFiles\Using\FirstUsingDirectiveMustBeSystem1.cs");

            this.AddExpectation(ContribRule.FirstUsingDirectiveMustBeSystem, 1);
            this.AddExpectation(ContribRule.UsingDirectiveMustBeSortedAlphabeticallyByGroup, 3);

            this.Analyze();

            this.ValidateExpectations();
        }

        /// <summary>
        /// System is not present in th using directives
        /// </summary>
        [TestMethod]
        public void FirstUsingDirectiveMustBeSystem2()
        {
            this.SetTargetFile(@"TargetFiles\Using\FirstUsingDirectiveMustBeSystem2.cs");

            this.AddExpectation(ContribRule.FirstUsingDirectiveMustBeSystem, 1);

            this.Analyze();

            this.ValidateExpectations();
        }

        /// <summary>
        /// No using directives are present
        /// </summary>
        [TestMethod]
        public void FirstUsingDirectiveMustBeSystem3()
        {
            this.SetTargetFile(@"TargetFiles\Using\FirstUsingDirectiveMustBeSystem3.cs");

            this.AddExpectation(ContribRule.FirstUsingDirectiveMustBeSystem, 1);
            this.SuppressRuleViolations(ContribRule.MaximumLineLengthExceeded);

            this.Analyze();

            this.ValidateExpectations();
        }

        [TestMethod]
        public void UsingDirectiveGroupMustBeSeparatedByBlankLine1()
        {
            this.SetTargetFile(@"TargetFiles\Using\UsingDirectiveGroupMustBeSeparatedByBlankLine1.cs");

            this.AddExpectation(ContribRule.UsingDirectiveGroupMustBeSeparatedByBlankLine, 6, 9);

            this.Analyze();

            this.ValidateExpectations();
        }

        #endregion
    }
}