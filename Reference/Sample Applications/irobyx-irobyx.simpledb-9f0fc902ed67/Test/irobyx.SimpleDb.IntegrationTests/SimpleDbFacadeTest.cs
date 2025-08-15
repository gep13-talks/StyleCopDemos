using System;
using System.Collections.Generic;

using NUnit.Framework;

using irobyx.SimpleDb.IntegrationTests.Samples;
using irobyx.SimpleDb.Interfaces;

namespace irobyx.SimpleDb.IntegrationTests
{
    [TestFixture]
    public class SimpleDbFacadeTest
    {
        private readonly ISimpleDbConfiguration _simpleDbConfiguration = new SimpleDbConfiguration();
        private ISimpleDbDomainConfiguration _dbDomainConfiguration;
        private string _domainName; 

        [TestFixtureSetUp]
        public void SetupTestDomain()
        {
            _dbDomainConfiguration = new  SimpleDbDomainConfiguration(_simpleDbConfiguration);
            this._domainName = _simpleDbConfiguration.GetDomainName(typeof(TestProduct));
            _dbDomainConfiguration.CreateDomain(this._domainName);
        }

        [TestFixtureTearDown]
        public void DeleteTestDomain()
        {
            _dbDomainConfiguration.DeleteDomain(this._domainName);
        }
        
        [Test]
        public void Create_Read_Update_Delete_TestProduct()
        {
            var testProduct = new TestProduct();
            testProduct.AvailableFrom = DateTime.Now;
            testProduct.Discontinued = false;
            testProduct.Id = Guid.NewGuid();
            testProduct.Name = "Sample Product";
            testProduct.Price = 4.5m;
            testProduct.Quantity = 25;
            testProduct.ItemStatus = ItemStatus.Draft;
            var theList = new List<string>();
            theList.Add("blue");
            theList.Add("green");
            theList.Add("red");
            testProduct.Tags = theList;
            var mapper = new SimpleDbMapper<TestProduct>();
            var test = new SimpleDbFacade<TestProduct>(this._simpleDbConfiguration, mapper);
            test.CreateItem(testProduct);

            var returnedProduct = test.GetItemById(testProduct.Id); 
            Assert.IsNotNull(returnedProduct);
            AssertEqual(testProduct, returnedProduct);

            returnedProduct.Name = "Updated Sample";
            returnedProduct.Price = 5m;
            returnedProduct.Quantity = 60;
            returnedProduct.ItemStatus = ItemStatus.Published;
            returnedProduct.Discontinued = true;
            returnedProduct.AvailableFrom = DateTime.Now.AddDays(-1);
            returnedProduct.Tags = new List<string>();

            test.UpdateItem(returnedProduct);

            var updatedProduct = test.GetItemById(testProduct.Id);
            Assert.IsNotNull(updatedProduct);
            AssertEqual(updatedProduct, returnedProduct);
            
            test.DeleteItem(testProduct.Id);
            
            var deletedProduct = test.GetItemById(testProduct.Id);
            Assert.IsNull(deletedProduct);
        }

        private static void AssertEqual(TestProduct actual, TestProduct expected)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.AvailableFrom.Date, actual.AvailableFrom.Date);
            Assert.AreEqual(expected.AvailableFrom.Hour, actual.AvailableFrom.Hour);
            Assert.AreEqual(expected.AvailableFrom.Minute, actual.AvailableFrom.Minute);
            Assert.AreEqual(expected.Discontinued, actual.Discontinued);
            Assert.AreEqual(expected.ItemStatus, actual.ItemStatus);
            Assert.AreEqual(expected.Price, actual.Price);
            Assert.AreEqual(expected.Quantity, actual.Quantity);
            Assert.AreEqual(expected.Tags, actual.Tags);
        }
    }
}
