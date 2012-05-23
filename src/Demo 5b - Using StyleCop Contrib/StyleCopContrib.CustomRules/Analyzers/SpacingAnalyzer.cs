using System;

using StyleCop;
using StyleCop.CSharp;

namespace StyleCopContrib.CustomRules.Analyzers
{
    /// <summary>
    /// Analyzer for spacing custom rules.
    /// </summary>
    internal sealed class SpacingAnalyzer : RuleAnalyzerBase
    {
        #region Fields

        private bool lastTokenWasWhitespace;
        private bool isBeginningOfLine;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SpacingAnalyzer"/> class.
        /// </summary>
        public SpacingAnalyzer()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Visits before the analysis.
        /// </summary>
        /// <param name="document">The document.</param>
        public override void VisitBeforeAnalysis(CodeDocument document)
        {
            this.lastTokenWasWhitespace = false;
            this.isBeginningOfLine = true;
        }

        //public override void VisitElement(CsElement element, CsElement parentElement, object context)
        //{
        //    if (element.ElementType == ElementType.Root)
        //    {
        //        element.
        //    }
        //}

        /// <summary>
        /// Visits the token.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="token">The token.</param>
        public override void VisitToken(CodeDocument document, CsToken token)
        {
            if (token.CsTokenType == CsTokenType.EndOfLine && this.lastTokenWasWhitespace)
            {
                this.SourceAnalyzer.AddViolation(document.DocumentContents, token.LineNumber, ContribRule.NoTrailingWhiteSpace);
            }

            if (this.isBeginningOfLine)
            {
                if ((token.CsTokenType == CsTokenType.WhiteSpace) &&
                    (!SpacingAnalyzer.IsValidIndentationWhitespace(token.Text)))
                {
                    this.SourceAnalyzer.AddViolation(document.DocumentContents, token.LineNumber, ContribRule.IndentUsingTabs);
                }

                this.isBeginningOfLine = false;
            }

            this.lastTokenWasWhitespace = token.CsTokenType == CsTokenType.WhiteSpace;
            if (token.CsTokenType == CsTokenType.EndOfLine) this.isBeginningOfLine = true;
        }

        private static bool IsValidIndentationWhitespace(string tokenText)
        {
            string noTabText = tokenText.TrimStart('\t');

            return (noTabText.TrimStart(' ').Length == 0) && (noTabText.Length <= 3);
        }

        #endregion
    }
}