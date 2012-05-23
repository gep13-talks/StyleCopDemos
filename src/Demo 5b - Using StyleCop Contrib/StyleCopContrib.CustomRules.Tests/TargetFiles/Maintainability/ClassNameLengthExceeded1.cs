using System;
using System.Diagnostics.CodeAnalysis;

namespace StyleCopContrib.CustomRules.Tests.TargetFiles.Maintainability
{
    /// <summary>
    /// Test class for rule ClassNameLengthExceeded
    /// </summary>
    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "For test")]
    [SuppressMessage("StyleCopContrib.CustomRules.Analyzers.CustomAnalyzer", "SC1301:FileNameMustMatchTypeName",
        Justification = "For test")]
    public class ThisIsATestClassToVerifyThatTheClassNameLengthExceededCustomStyleCopRuleIsWorking
    {
    }
}