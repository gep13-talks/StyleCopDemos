using System;

using StyleCop;
using StyleCop.CSharp;

namespace StyleCopContrib.CustomRules.Analyzers
{
    /// <summary>
    /// Base class for custom rule implementation.
    /// </summary>
    public class RuleAnalyzerBase
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleAnalyzerBase"/> class.
        /// </summary>
        public RuleAnalyzerBase()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the source analyzer.
        /// </summary>
        /// <value>The source analyzer.</value>
        protected SourceAnalyzer SourceAnalyzer { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the rule analyzer.
        /// </summary>
        /// <param name="sourceAnalyzer">The source analyzer.</param>
        public void Initialize(SourceAnalyzer sourceAnalyzer)
        {
            this.SourceAnalyzer = sourceAnalyzer;
        }

        /// <summary>
        /// Visits before the analysis.
        /// </summary>
        /// <param name="document">The document.</param>
        public virtual void VisitBeforeAnalysis(CodeDocument document)
        {
        }

        /// <summary>
        /// Visits before the analysis.
        /// </summary>
        /// <param name="document">The document.</param>
        public virtual void VisitAfterAnalysis(CodeDocument document)
        {
        }

        /// <summary>
        /// Visits the document.
        /// </summary>
        /// <param name="document">The document</param>
        public virtual void VisitDocument(CsDocument document)
        {
        }

        /// <summary>
        /// Visits the code element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="parentElement">The parent element.</param>
        /// <param name="context">The context.</param>
        public virtual void VisitElement(CsElement element, CsElement parentElement, object context)
        {
        }

        /// <summary>
        /// Visits the statement.
        /// </summary>
        /// <param name="statement">The statement.</param>
        /// <param name="parentExpression">The parent expression.</param>
        /// <param name="parentStatement">The parent statement.</param>
        /// <param name="parentElement">The parent element.</param>
        /// <param name="context">The context.</param>
        public virtual void VisitStatement(Statement statement, Expression parentExpression, Statement parentStatement,
            CsElement parentElement, object context)
        {
        }

        /// <summary>
        /// Visits the expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="parentExpression">The parent expression.</param>
        /// <param name="parentStatement">The parent statement.</param>
        /// <param name="parentElement">The parent element.</param>
        /// <param name="context">The context.</param>
        public virtual void VisitExpression(Expression expression, Expression parentExpression, Statement parentStatement, 
            CsElement parentElement, object context)
        {
        }

        /// <summary>
        /// Visits the query clause.
        /// </summary>
        /// <param name="clause">The clause.</param>
        /// <param name="parentClause">The parent clause.</param>
        /// <param name="parentExpression">The parent expression.</param>
        /// <param name="parentStatement">The parent statement.</param>
        /// <param name="parentElement">The parent element.</param>
        /// <param name="context">The context.</param>
        public virtual void VisitQueryClause(QueryClause clause, QueryClause parentClause, Expression parentExpression,
            Statement parentStatement, CsElement parentElement, object context)
        {
        }

        /// <summary>
        /// Visits the token.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="token">The token.</param>
        public virtual void VisitToken(CodeDocument document, CsToken token)
        {
        }

        #endregion
    }
}