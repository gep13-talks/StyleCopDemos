using System;
using System.Collections.Generic;

using NUnit.Framework;

using irobyx.SimpleDb.UnitTests.Samples;

namespace irobyx.SimpleDb.UnitTests
{
    [TestFixture]
    public class SimpleDbConverterTest
    {
        [Test]
        public void Convert_Enum_IsSupported()
        {
            var converter = new SimpleDbConverter();
            var thisStatus = ItemStatus.Deleted;
            var forSimpleDb = converter.ConvertToSimpleDb(thisStatus.GetType(), thisStatus);
            Assert.AreEqual("Deleted", forSimpleDb);
            var convertedStatus = converter.ConvertFromSimpleDb(typeof(ItemStatus), forSimpleDb);
            Assert.AreEqual(convertedStatus, thisStatus);
        }

        [Test]
        public void Convert_IEnumerableOfString_IsSupported()
        {
            var converter = new SimpleDbConverter();
            var theList = new List<string>();
            var forSimpleDb = converter.ConvertToSimpleDb(theList.GetType(), theList);
            Assert.AreEqual(string.Empty, forSimpleDb);
            theList.Add("blue");
            theList.Add("red");
            theList.Add("green");
            forSimpleDb = converter.ConvertToSimpleDb(theList.GetType(), theList);
            Assert.AreEqual("blue,red,green", forSimpleDb);
            var toSimpleDb = converter.ConvertFromSimpleDb(theList.GetType(), forSimpleDb);
            Assert.AreEqual(toSimpleDb, theList);
        }

        [ExpectedException(ExpectedException = typeof(ArgumentOutOfRangeException))]
        [Test]
        public void Convert_UnsupportedType_ThrowsArgumentOutOfRangeException()
        {
            var converter = new SimpleDbConverter();
            var unsupportedType = new List<int>();
            converter.ConvertToSimpleDb(unsupportedType.GetType(), unsupportedType);
        }
    }
}