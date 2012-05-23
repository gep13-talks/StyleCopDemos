using System;
using System.Collections.Generic;

using StyleCopContrib.CustomRules.Analyzers;

namespace StyleCopContrib.CustomRules
{
    /// <summary>
    /// RuleAnalyzers of the custom rule analyzers
    /// </summary>
    public sealed class AnalyzerRegistry
    {
        #region Fields

        private readonly List<RuleAnalyzerBase> ruleAnalyzers;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyzerRegistry"/> class.
        /// </summary>
        public AnalyzerRegistry()
        {
            this.ruleAnalyzers = new List<RuleAnalyzerBase>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the rule analyzers.
        /// </summary>
        /// <value>The rule analyzers.</value>
        public IEnumerable<RuleAnalyzerBase> RuleAnalyzers
        {
            get
            {
                return this.ruleAnalyzers;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a custom rule analyzerBase.
        /// </summary>
        /// <param name="analyzerBase">The rule analyzer.</param>
        public void AddAnalyzer(RuleAnalyzerBase analyzerBase)
        {
            this.ruleAnalyzers.Add(analyzerBase);
        }

        #endregion
    }
}