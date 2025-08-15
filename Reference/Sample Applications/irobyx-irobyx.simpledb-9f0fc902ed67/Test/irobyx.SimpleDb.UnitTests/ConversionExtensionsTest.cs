using System;

using NUnit.Framework;

namespace irobyx.SimpleDb.UnitTests
{
    [TestFixture]
    public class ConversionExtensionsTest
    {
        [Test]
        public void Convert_DateTime_IsSupported()
        {
            var value = new DateTime(2010, 10, 15);
            string forSimpleDb = value.ConvertForSimpleDb();
            Assert.AreEqual("2010-10-15T00:00:00", forSimpleDb);
            value = new DateTime(2010, 5, 31, 14, 51, 34);
            forSimpleDb = value.ConvertForSimpleDb();
            Assert.AreEqual("2010-05-31T14:51:34", forSimpleDb);
            DateTime convertBack = Convert.ToDateTime(forSimpleDb);
            Assert.AreEqual(value, convertBack);
        }

        [Test]
        public void Convert_Integer_IsSupported()
        {
            int i = 5;
            string forSimpleDb = i.ConvertForSimpleDb();
            Assert.AreEqual("0000000005", forSimpleDb);
            int convertBack = Convert.ToInt32(forSimpleDb);
            Assert.AreEqual(5, convertBack);
            i = 100000;
            forSimpleDb = i.ConvertForSimpleDb();
            Assert.AreEqual("0000100000", forSimpleDb);
            convertBack = Convert.ToInt32(forSimpleDb);
            Assert.AreEqual(100000, convertBack);
        } 
    }
}
