using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using StyleCop.CSharp;

namespace StyleCopContrib.CustomRules.Analyzers.UsingDirectives
{
    /// <summary>
    /// Represents a group of using directives.
    /// </summary>
    internal sealed class UsingDirectiveGroup : IEnumerable<UsingDirective>
    {
        #region Fields

        private readonly List<UsingDirective> usingDirectives;

        #endregion

        #region Constructor

        internal UsingDirectiveGroup(int index)
        {
            this.usingDirectives = new List<UsingDirective>();

            this.Index = index;
        }

        #endregion

        #region Properties

        internal int Index { get; private set; }

        internal int FirstLineNumber
        {
            get
            {
                return this.usingDirectives.Min(x => x.LineNumber);
            }
        }

        internal int LastLineNumber
        {
            get
            {
                return this.usingDirectives.Max(x => x.LineNumber);
            }
        }

        #endregion

        #region Methods

        internal void Add(UsingDirective usingDirective)
        {
            this.usingDirectives.Add(usingDirective);
        }

        #endregion

        #region Implementation of IEnumerable

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<UsingDirective> GetEnumerator()
        {
            return this.usingDirectives.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}