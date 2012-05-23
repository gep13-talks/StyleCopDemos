using System;
using System.Collections.Generic;

using StyleCop;

namespace StyleCopContrib.Runner
{
    /// <summary>
    /// Container of an analysis results
    /// </summary>
    public sealed class AnalysisResults
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalysisResults"/> class.
        /// </summary>
        /// <param name="codeProjects">The analyzed code project list.</param>
        /// <param name="outputs">The output message list.</param>
        /// <param name="violations">The violation list.</param>
        /// <param name="minuteDuration">Duration of the analysis in minute.</param>
        public AnalysisResults(IEnumerable<CodeProject> codeProjects, IEnumerable<string> outputs,
                               IEnumerable<Violation> violations, double minuteDuration)
        {
            this.CodeProjects = codeProjects;
            this.Outputs = outputs;
            this.Violations = violations;
            this.MinuteDuration = minuteDuration;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the analyzed code project list.
        /// </summary>
        /// <value>The code projects.</value>
        public IEnumerable<CodeProject> CodeProjects { get; private set; }

        /// <summary>
        /// Gets the output message list.
        /// </summary>
        /// <value>The outputs.</value>
        public IEnumerable<string> Outputs { get; private set; }

        /// <summary>
        /// Gets the violation list.
        /// </summary>
        /// <value>The violation list.</value>
        public IEnumerable<Violation> Violations { get; private set; }

        /// <summary>
        /// Gets the duration of the analysis in minute.
        /// </summary>
        /// <value>The duration of the minute.</value>
        public double MinuteDuration { get; private set; }

        #endregion
    }
}