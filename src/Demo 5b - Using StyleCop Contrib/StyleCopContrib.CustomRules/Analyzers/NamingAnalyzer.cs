using System;
using System.IO;
using System.Linq;

using StyleCop.CSharp;

namespace StyleCopContrib.CustomRules.Analyzers
{
    /// <summary>
    /// Custom rule analyzer for naming conventions like FileNameMustMatchTypeName.
    /// </summary>
    internal sealed class NamingAnalyzer : RuleAnalyzerBase
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="NamingAnalyzer"/> class.
        /// </summary>
        internal NamingAnalyzer()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Visits the code element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="parentElement">The parent element.</param>
        /// <param name="context">The context.</param>
        public override void VisitElement(CsElement element, CsElement parentElement, object context)
        {
            if (element.ElementType == ElementType.Class
                || element.ElementType == ElementType.Struct
                || element.ElementType == ElementType.Interface)
            {
                if (element.AccessModifier != AccessModifierType.Private)
                {
                    string fileName = Path.GetFileNameWithoutExtension(element.Document.SourceCode.Path);
                    string typeName = NamingAnalyzer.TrimGenericTypeParameters(element.Declaration.Name);

                    if (typeName != fileName)
                    {
                        this.SourceAnalyzer.AddViolation(element, ContribRule.FileNameMustMatchTypeName);
                    }
                }
            }
        }

        private static string TrimGenericTypeParameters(string identifier)
        {
            string result = identifier;

            int startOfGenericTypeParameters = identifier.IndexOf("<");
            if (startOfGenericTypeParameters > 0)
            {
                result = identifier.Substring(0, startOfGenericTypeParameters);
            }

            return result;
        }

        #endregion
    }
}