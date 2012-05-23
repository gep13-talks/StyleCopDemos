using System;
using System.Collections.Generic;

using StyleCop;
using StyleCop.CSharp;

using StyleCopContrib.CustomRules.Analyzers.UsingDirectives;

namespace StyleCopContrib.CustomRules.Analyzers
{
    /// <summary>
    /// Custom rule analyzer for using usage conventions like FirstUsingDirectiveMustBeSystem, 
    /// UsingDirectiveGroupMustFollowGivenOrder, UsingDirectiveGroupMustBeSeparatedByBlankLine,
    /// UsingDirectiveMustBeSortedAlphabeticallyByGroup.
    /// </summary>
    internal sealed class UsingDirectivesAnalyzer : RuleAnalyzerBase
    {
        #region Fields

        private List<UsingDirective> usingDirectives;
        private UsingSettings usingSettings;

        #endregion

        #region Constructor

        internal UsingDirectivesAnalyzer()
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
            this.usingDirectives = new List<UsingDirective>();

            this.usingSettings = new UsingSettings(this.SourceAnalyzer, document.Settings);
        }

        /// <summary>
        /// Visits the code element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="parentElement">The parent element.</param>
        /// <param name="context">The context.</param>
        public override void VisitElement(CsElement element, CsElement parentElement, object context)
        {
            UsingDirective usingDirective = element as UsingDirective;

            if (usingDirective != null)
            {
                this.usingDirectives.Add(usingDirective);
            }
        }

        /// <summary>
        /// Visits before the analysis.
        /// </summary>
        /// <param name="document">The document.</param>
        public override void VisitAfterAnalysis(CodeDocument document)
        {
            UsingValidator usingValidator = new UsingValidator(this.SourceAnalyzer, this.usingSettings, this.usingDirectives);

            usingValidator.Validate(document);
        }

        #endregion
    }
}