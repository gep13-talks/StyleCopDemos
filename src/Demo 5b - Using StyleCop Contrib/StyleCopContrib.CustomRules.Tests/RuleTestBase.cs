using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using StyleCop;
using StyleCopContrib.Runner;

namespace StyleCopContrib.CustomRules.Tests
{
    public class RuleTestBase
    {
        #region Fields

        private readonly string projectRootPath;
        private readonly IList<Expectation> expectations;
        private readonly List<string> ruleSuppressions;
        private ConsoleRunner consoleRunner;
        private string codeFile;
        private AnalysisResults analysisResults;
        private SettingsManager settingsManager;

        private string projectSettingsPath;

        #endregion

        #region Constructors

        public RuleTestBase()
        {
            this.projectRootPath =
                Path.GetDirectoryName(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            this.expectations = new List<Expectation>();
            this.ruleSuppressions = new List<string>();
        }

        #endregion

        #region Methods

        [TestInitialize]
        public void Setup()
        {
            this.expectations.Clear();
            this.codeFile = null;
            this.analysisResults = null;

            this.settingsManager = ServiceLocator.GetService<SettingsManager>();
            if (this.settingsManager == null)
            {
                this.settingsManager = new SettingsManager();
                ServiceLocator.RegisterService<SettingsManager>(this.settingsManager);
            }

            this.settingsManager.ResetSettings();

            this.TestSetup();
        }

        protected virtual void TestSetup()
        {
        }

        protected void SetTargetFile(string relativeFilePathFromProjectRoot)
        {
            this.codeFile = Path.Combine(this.projectRootPath, relativeFilePathFromProjectRoot);
        }

        protected void SetBaseProjectSettingFile(string relativeFilePathFromProjectRoot)
        {
            this.projectSettingsPath = Path.Combine(this.projectRootPath, relativeFilePathFromProjectRoot);
        }

        protected void SetAnalyzerSetting<T>(string propertyName, T propertyValue)
        {
            this.settingsManager.SetSetting(propertyName, propertyValue);
        }

        protected void Analyze()
        {
            if (this.codeFile == null) throw new InvalidOperationException("TargetFile must be set before analysis");

            string settingsPath = this.GetSettingsFilePath();

            this.consoleRunner = new ConsoleRunner(settingsPath, null);

            CodeProject codeProject = ProjectUtility.CreateOneFileProject(this.codeFile, this.consoleRunner.Environment);

            this.analysisResults = this.consoleRunner.Analyze(codeProject);
        }

        protected void AddExpectation(Enum violatedRule, params int[] lineNumbers)
        {
            foreach (int lineNumber in lineNumbers)
            {
                this.expectations.Add(new Expectation { ViolatedRule = violatedRule, LineNumber = lineNumber });
            }
        }

        protected void SuppressRuleViolations(Enum rule)
        {
            this.ruleSuppressions.Add(rule.ToString());
        }

        protected void ValidateExpectations()
        {
            IList<Expectation> unmatchedExpectations = new List<Expectation>();
            IList<Violation> actualViolations = new List<Violation>(this.analysisResults.Violations
                .Where(x => !this.ruleSuppressions.Contains(x.Rule.Name)));

            foreach (Expectation expectation in this.expectations)
            {
                Violation matchingViolation = null;

                foreach (Violation violation in actualViolations)
                {
                    if ((expectation.ViolatedRule.ToString() == violation.Rule.Name) &&
                        (expectation.LineNumber == violation.Line))
                    {
                        matchingViolation = violation;
                        break;
                    }
                }

                if (matchingViolation != null)
                {
                    actualViolations.Remove(matchingViolation);
                }
                else
                {
                    unmatchedExpectations.Add(expectation);
                }
            }

            RuleTestBase.ReportUnmatchedViolations(unmatchedExpectations, actualViolations);
        }

        private string GetSettingsFilePath()
        {
            return this.projectSettingsPath;
        }

        private static void ReportUnmatchedViolations(ICollection<Expectation> unmatchedExpectations,
                                                      ICollection<Violation> unmatchedViolations)
        {
            StringBuilder message = new StringBuilder();

            if (unmatchedExpectations.Count > 0)
            {
                message.AppendLine("The following expectations where not found in the code file");
                foreach (Expectation expectation in unmatchedExpectations)
                {
                    message.AppendLine(expectation.ViolatedRule + " at line " + expectation.LineNumber);
                }
            }

            if (unmatchedViolations.Count > 0)
            {
                message.AppendLine("The following violations where not expected in the code file");
                foreach (Violation violation in unmatchedViolations)
                {
                    message.AppendLine(violation.Rule.Name + " at line " + violation.Line);
                }
            }

            if (message.Length > 0) Assert.Fail(message.ToString());
        }

        #endregion

        #region Nested Types

        private sealed class Expectation
        {
            public Enum ViolatedRule { get; set; }

            public int LineNumber { get; set; }
        }

        #endregion
    }
}