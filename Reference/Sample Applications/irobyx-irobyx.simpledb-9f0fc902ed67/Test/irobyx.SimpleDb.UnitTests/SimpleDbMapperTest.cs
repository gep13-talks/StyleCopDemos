using System;
using System.Collections.Generic;

using NUnit.Framework;
using irobyx.SimpleDb.Interfaces;
using irobyx.SimpleDb.UnitTests.Samples;

namespace irobyx.SimpleDb.UnitTests
{
    [TestFixture]
    public class SimpleDbMapperTest
    {
        [Test]
        public void CanConvertTestProductToSimpleDb()
        {
            ISimpleDbMapper<TestProduct> mapper = new SimpleDbMapper<TestProduct>(); 
            var p = new TestProduct();
            p.AvailableFrom = DateTime.Now;
            p.Discontinued = false;
            p.Id = Guid.NewGuid();
            p.Name = "Sample Product";
            p.Price = 4.5m;
            p.Quantity = 25;
            p.ItemStatus = ItemStatus.Draft;
            var theList = new List<string>();
            theList.Add("blue");
            theList.Add("green");
            theList.Add("red");
            p.Tags = theList;
            var convertedAttributes = mapper.MapEntityToAttributes(p);
            
        }
 
    }
}
