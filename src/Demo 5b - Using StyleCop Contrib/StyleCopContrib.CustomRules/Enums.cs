using System;

namespace StyleCopContrib.CustomRules
{
    /// <summary>
    /// The list of custom rule
    /// </summary>
    public enum ContribRule
    {
        /// <summary>
        /// Custom rule SC1001
        /// <remarks>
        /// This rule was inspired by Andrew Arnott blog
        /// http://blog.nerdbank.net/2008/09/notrailingwhitespace-stylecop-rule-and.html
        /// </remarks>
        /// </summary>
        NoTrailingWhiteSpace,

        /// <summary>
        /// Custom rule SC1002
        /// <remarks>
        /// This rule was inspired by Andrew Arnott blog
        /// http://blog.nerdbank.net/2008/09/notrailingwhitespace-stylecop-rule-and.html
        /// </remarks>
        /// </summary>
        IndentUsingTabs,

        /// <summary>
        /// Custom rule SC1003
        /// </summary>
        MaximumLineLengthExceeded,

        /// <summary>
        /// Custom rule SC1004
        /// </summary>
        ClassNameLengthExceeded,

        /// <summary>
        /// Custom rule SC1201
        /// </summary>
        UsingDirectiveMustBeSortedAlphabeticallyByGroup,

        /// <summary>
        /// Custom rule SC1202
        /// </summary>
        UsingDirectiveGroupMustFollowGivenOrder,

        /// <summary>
        /// Custom rule SC1203
        /// </summary>
        FirstUsingDirectiveMustBeSystem,

        /// <summary>
        /// Custom rule SC1204
        /// </summary>
        UsingDirectiveGroupMustBeSeparatedByBlankLine,

        /// <summary>
        /// Custom rule SC1301
        /// </summary>
        FileNameMustMatchTypeName,

        /// <summary>
        /// Custom rule SC1401
        /// </summary>
        SingleReturnStatement,

            /// <summary>
        /// Custom rule SC1402
        /// </summary>
        ReturnStatementOnlyInFunctions
    }
}