using System.Collections.Generic;
using System;

namespace irobyx.SimpleDb.IntegrationTests.Samples
{
    public class TestProduct: DbEntity
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime AvailableFrom { get; set; }
        public bool Discontinued { get; set; }
        public ItemStatus ItemStatus { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}