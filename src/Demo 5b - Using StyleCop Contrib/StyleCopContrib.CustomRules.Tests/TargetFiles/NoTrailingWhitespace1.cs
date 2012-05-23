using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace StyleCopContrib.CustomRules.Tests.TargetFiles
{
    /// <summary>
    /// Test class for rule NoTrailingWhitespace
    /// </summary>
    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "For test")]
    internal sealed class NoTrailingWhitespace1 
    {
        /// <summary>
        /// Method1s this instance.       
        /// </summary>
        internal static void Method1()
        {
            IList<int> test = new List<int>()
                {
                    1, 2, 3, 
                    4, 5, 6
                };

            Console.WriteLine(test);
	
            Console.WriteLine(1);
        }
    }
}