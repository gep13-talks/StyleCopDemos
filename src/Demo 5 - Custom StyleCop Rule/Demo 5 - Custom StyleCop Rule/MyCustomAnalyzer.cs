// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MyCustomAnalyzer.cs" company="gep13">
//   Copyright (c) gep13 2012
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Demo_5___Custom_StyleCop_Rule
{
    using StyleCop;
    using StyleCop.CSharp;

    /// <summary>
    /// Custom Analyser for Demo Purposes
    /// </summary>
    [SourceAnalyzer(typeof(CsParser))]
    public class MyCustomAnalyzer : SourceAnalyzer
    {
        #region Constants

        /// <summary>
        /// The maximum number of characters allowed in a class name
        /// </summary>
        private const int MaximumClassNameLength = 50;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Extremely simple analyser for demo purposes
        /// </summary>
        /// <param name="document">
        /// The complete code document being passed in for inspection
        /// </param>
        public override void AnalyzeDocument(CodeDocument document)
        {
            Param.RequireNotNull(document, "document");
            var doc = (CsDocument)document;

            // skipping wrong or auto-generated documents
            if (doc.RootElement == null || doc.RootElement.Generated)
            {
                return;
            }

            // check all class entries
            doc.WalkDocument(this.CheckClasses);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks whether specified element conforms to custom rule GP0001
        /// </summary>
        /// <param name="element">
        /// The current element that is to be inspected.
        /// </param>
        /// <param name="parentElement">
        /// The parent element for the element currently being inspected.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// True/False indicating whether to continue walking the elements within the class.
        /// </returns>
        private bool CheckClasses(CsElement element, CsElement parentElement, object context)
        {
            // If the overall StyleCop is cancelled externally, then stop processing
            if (Cancel)
            {
                return false;
            }

            // if current element is not a class then continue walking
            if (element.ElementType != ElementType.Class)
            {
                return true;
            }

            // check whether class name contains "a" letter
            var classElement = (Class)element;
            if (classElement.Declaration.Name.Contains("a"))
            {
                // add violation
                // (note how custom message arguments could be used)
                AddViolation(
                    classElement, classElement.Location, "AvoidUsingAInClassNames", classElement.FriendlyTypeText);
            }

            if (classElement.Declaration.Name.Length > MaximumClassNameLength)
            {
                // add violation
                // (note how custom message arguments could be used)
                AddViolation(classElement, classElement.Location, "AvoidLongClassNames", classElement.FriendlyTypeText);
            }

            // continue walking in order to find all classes in file
            return true;
        }

        #endregion
    }
}