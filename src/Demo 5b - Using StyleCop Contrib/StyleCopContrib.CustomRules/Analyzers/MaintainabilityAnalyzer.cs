using System;
using System.Linq;

using StyleCop.CSharp;

namespace StyleCopContrib.CustomRules.Analyzers
{
    /// <summary>
    /// Custom rule analyzer for maintainability rules.
    /// </summary>
    internal class MaintainabilityAnalyzer : RuleAnalyzerBase
    {
        public override void VisitDocument(CsDocument document)
        {
            using (var reader = document.SourceCode.Read())
            {
                var line = reader.ReadLine();
                var lineNumber = 1;
                while (line != null)
                {
                    if (line.Length > 140
                        && !line.Trim().StartsWith("[SuppressMessage("))
                    {
                        this.SourceAnalyzer.AddViolation(
                            document.RootElement,
                            lineNumber,
                            ContribRule.MaximumLineLengthExceeded);
                    }

                    line = reader.ReadLine();
                    lineNumber++;
                }
            }
        }

        public override void VisitElement(CsElement element, CsElement parentElement, object context)
        {
            if ((element.ElementType == ElementType.Method && ((Method)element).ReturnType.Text != "void") // Func
                || (element.ElementType == ElementType.Accessor && ((Accessor)element).AccessorType == AccessorType.Get)) // Getter
            {
                if (element.ElementTokens.Count(x => x.CsTokenType == CsTokenType.Return) > 1)
                {
                    this.SourceAnalyzer.AddViolation(element, ContribRule.SingleReturnStatement);
                }
            }
            else if (element.ElementType == ElementType.Constructor
                || (element.ElementType == ElementType.Method && ((Method)element).ReturnType.Text == "void") // Proc
                || (element.ElementType == ElementType.Accessor && ((Accessor)element).AccessorType != AccessorType.Get)) // Setter, Add, Remove
            {
                if (element.ElementTokens.Any(x => x.CsTokenType == CsTokenType.Return))
                {
                    this.SourceAnalyzer.AddViolation(element, ContribRule.ReturnStatementOnlyInFunctions);
                }
            }
        }

        //public static Expression GetExpressionByLine(CsDocument document, int lineNumber)
        //{
        //    var context = new LineExpression { LineNumber = lineNumber };
        //    document.WalkDocument(null, null, FindByLineExpressionVisitor, context);

        //    return context.Expression;
        //}

        //private static bool FindByLineExpressionVisitor(Expression expression, Expression parentExpression, Statement parentStatement, 
        //    CsElement parentElement, LineExpression context)
        //{
        //    if (expression.Location.StartPoint.LineNumber > context.LineNumber) return false;

        //    if (expression.Location.StartPoint.LineNumber <= context.LineNumber
        //        && expression.Location.EndPoint.LineNumber >= context.LineNumber)
        //    {
        //        context.Expression = expression;
        //    }

        //    return true;
        //}

        //private struct LineExpression
        //{
        //    public int LineNumber;

        //    public Expression Expression;
        //}
    }
}
