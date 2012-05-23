using System;
using System.Diagnostics.CodeAnalysis;

namespace StyleCopContrib.CustomRules.Tests.TargetFiles.Maintainability
{
    // zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz139
    // zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz140
    // zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz145
    public class MaximumLineLengthExceeded1 // zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz141
    {
        [SuppressMessage("Blahblah", "CheckId", Justification = "zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz")]
        public void Toto(object v)
        {
            int a = 12345 + 12345 + 12345 + 12345 + 12345 + 12345 + 12345 + 12345 + 12345 + 12345 + 12345 + 12345 + 12345 + 12345 + 12345 + 12345;
            Console.WriteLine(a);

            int b = 12345 + 12345 + 12345 + 12345 + 12345 + 12345 + 12345 + 12345 + 12345 + 12345 + 12345 + 12345 + 12345 + 12345 + 123+ 45;
            Console.WriteLine(b);
            /*
                                                                                         [SuppressMessage("Blahblah", "CheckId", Justificationadasdasdasdasdasdasjdhasjkdh 
             */
        }
    }
}
