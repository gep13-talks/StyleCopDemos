using System;

using NUnit.Framework;

using irobyx.SimpleDb.UnitTests.Samples;

namespace irobyx.SimpleDb.UnitTests
{
    [TestFixture]
    public class DbEntityTest
    {
        [Test]
        public void TestEquality()
        {
            var productOne = new TestProduct();
            productOne.Id = Guid.NewGuid();
            productOne.Name = "One";

            var productTwo = new TestProduct();
            productTwo.Id = Guid.NewGuid();
            productOne.Name = "Two";
            
            var productThree = productOne;

            var productFour = new TestProduct();
            productFour.Id = productTwo.Id;
            productFour.Name = "Four";

            TestProduct productNull = null; 

            //ref
            Assert.IsTrue(productOne.Equals(productThree));
            //id
            Assert.IsTrue(productTwo.Equals(productFour));

            Assert.IsFalse(productOne.Equals(productTwo));
            Assert.IsFalse(productTwo.Equals(productThree));
            
            Assert.IsFalse(productOne.Equals(productNull));
            
            Assert.IsTrue(productTwo == productFour);
            Assert.IsTrue(productOne != productTwo);
            
        }
    }
}