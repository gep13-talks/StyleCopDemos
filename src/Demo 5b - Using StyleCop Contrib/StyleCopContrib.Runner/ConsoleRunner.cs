using System;
using System.Collections.Generic;

using StyleCop;

namespace StyleCopContrib.Runner
{
    /// <summary>
    /// Wrapper around the StyleCopConsole class
    /// </summary>
    public sealed class ConsoleRunner
    {
        #region Fields

        private readonly StyleCopConsole console;
        private readonly IList<string> outputs;
        private readonly IList<Violation> violations;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleRunner"/> class.
        /// </summary>
        /// <param name="settingsPath">The settings path.</param>
        /// <param name="outputPath">The output path.</param>
        public ConsoleRunner(string settingsPath, string outputPath)
        {
            List<string> addinPaths = new List<string>();
            this.console = new StyleCopConsole(settingsPath, false, outputPath, addinPaths, true);

            this.outputs = new List<string>();
            this.violations = new List<Violation>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the StyleCop environment.
        /// </summary>
        /// <value>The StyleCop environment.</value>
        public StyleCopEnvironment Environment
        {
            get
            {
                return this.console.Core.Environment;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Analyzes the specified code project.
        /// </summary>
        /// <param name="codeProject">The code project.</param>
        /// <returns>The <see cref="AnalysisResults"/> object of the analyzed <see cref="CodeProject"/> list.</returns>
        public AnalysisResults Analyze(CodeProject codeProject)
        {
            return this.Analyze(new List<CodeProject> { codeProject });
        }

        /// <summary>
        /// Analyzes the specified code projects.
        /// </summary>
        /// <param name="codeProjects">The code projects.</param>
        /// <returns>The <see cref="AnalysisResults"/> object of the analyzed <see cref="CodeProject"/> list.</returns>
        public AnalysisResults Analyze(IEnumerable<CodeProject> codeProjects)
        {
            if (codeProjects == null) throw new ArgumentNullException("codeProjects");

            this.console.ViolationEncountered += (sender, args) => this.AddViolation(args.Violation);
            this.console.OutputGenerated += (sender, args) => this.AddOutput(args.Output);

            DateTime start = DateTime.Now;

            this.console.Start(new List<CodeProject>(codeProjects), true);

            return new AnalysisResults(codeProjects, this.outputs, this.violations, DateTime.Now.Subtract(start).TotalSeconds);
        }

        private void AddOutput(string output)
        {
            this.outputs.Add(output);
        }

        private void AddViolation(Violation violation)
        {
            this.violations.Add(violation);
        }

        #endregion
    }
}