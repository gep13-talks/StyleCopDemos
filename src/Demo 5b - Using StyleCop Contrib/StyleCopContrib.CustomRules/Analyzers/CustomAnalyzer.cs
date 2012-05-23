using System;
using System.Collections.Generic;

using StyleCop;
using StyleCop.CSharp;

namespace StyleCopContrib.CustomRules.Analyzers
{
    /// <summary>
    /// A custom rule analyzer class.
    /// </summary>
    [SourceAnalyzer(typeof(CsParser))]
    public sealed class CustomAnalyzer : SourceAnalyzer
    {
        #region Fields

        private AnalyzerRegistry analyzerRegistry;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomAnalyzer"/> class.
        /// </summary>
        public CustomAnalyzer()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the settings pages.
        /// </summary>
        /// <value>The settings pages.</value>
        public override ICollection<IPropertyControlPage> SettingsPages
        {
            get
            {
                return new IPropertyControlPage[] { new UsingDirectiveGroupControl(this) };
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Analyzes the document.
        /// </summary>
        /// <param name="document">The document.</param>
        public override void AnalyzeDocument(CodeDocument document)
        {
            if (document == null) throw new ArgumentNullException("document");

            try
            {
                this.Bootstrapper();

                this.analyzerRegistry.RuleAnalyzers.ForEach(analyzer => analyzer.Initialize(this));
                this.analyzerRegistry.RuleAnalyzers.ForEach(analyzer => analyzer.VisitBeforeAnalysis(document));

                var csharpDocument = (CsDocument)document;

                this.VisiteDocument(csharpDocument);

                csharpDocument.WalkDocument(
                    this.VisitElement,
                    this.VisitStatement,
                    this.VisitExpression,
                    this.VisitQueryClause);

                csharpDocument.Tokens.ForEach(token =>
                    this.analyzerRegistry.RuleAnalyzers.ForEach(analyzer =>
                        analyzer.VisitToken(document, token)));

                this.analyzerRegistry.RuleAnalyzers.ForEach(analyzer => analyzer.VisitAfterAnalysis(document));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private void Bootstrapper()
        {
            var settingsManager = ServiceLocator.GetService<SettingsManager>();
            if (settingsManager == null) ServiceLocator.RegisterService<SettingsManager>(new SettingsManager());

            this.analyzerRegistry = ServiceLocator.GetService<AnalyzerRegistry>();
            if (this.analyzerRegistry == null)
            {
                this.analyzerRegistry = new AnalyzerRegistry();
                ServiceLocator.RegisterService<AnalyzerRegistry>(this.analyzerRegistry);

                this.analyzerRegistry.AddAnalyzer(new UsingDirectivesAnalyzer());
                this.analyzerRegistry.AddAnalyzer(new NamingAnalyzer());
                this.analyzerRegistry.AddAnalyzer(new SpacingAnalyzer());
                this.analyzerRegistry.AddAnalyzer(new MaintainabilityAnalyzer());
            }
        }

        private void VisiteDocument(CsDocument document)
        {
            this.analyzerRegistry.RuleAnalyzers.ForEach(analyzer => analyzer.VisitDocument(document));
        }

        private bool VisitElement(CsElement element, CsElement parentElement, object context)
        {
            this.analyzerRegistry.RuleAnalyzers.ForEach(analyzer => 
                analyzer.VisitElement(element, parentElement, context));

            return true;
        }

        private bool VisitStatement(Statement statement, Expression parentExpression, Statement parentStatement,
            CsElement parentElement, object context)
        {
            this.analyzerRegistry.RuleAnalyzers.ForEach(analyzer => 
                analyzer.VisitStatement(statement, parentExpression, parentStatement, parentElement, context));

            return true;
        }

        private bool VisitExpression(Expression expression, Expression parentExpression, Statement parentStatement,
            CsElement parentElement, object context)
        {
            this.analyzerRegistry.RuleAnalyzers.ForEach(analyzer => 
                analyzer.VisitExpression(expression, parentExpression, parentStatement, parentElement, context));

            return true;
        }

        private bool VisitQueryClause(QueryClause clause, QueryClause parentClause, Expression parentExpression,
            Statement parentStatement, CsElement parentElement, object context)
        {
            this.analyzerRegistry.RuleAnalyzers.ForEach(analyzer => 
                analyzer.VisitQueryClause(clause, parentClause, parentExpression, parentStatement, parentElement, context));

            return true;
        }

        #endregion
    }
}