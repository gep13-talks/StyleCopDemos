using NUnit.Framework;

using irobyx.SimpleDb.Interfaces;

namespace irobyx.SimpleDb.IntegrationTests
{
    [TestFixture]
    public class SimpleDbConfigurationTest
    {
        [Test]
        public void CanLoadConfiguration()
        {
            ISimpleDbConfiguration config = new SimpleDbConfiguration();
            Assert.False(string.IsNullOrWhiteSpace(config.SimpleDbAccessKey));
            Assert.False(string.IsNullOrWhiteSpace(config.SimpleDbSecretKey));
            Assert.False(string.IsNullOrWhiteSpace(config.SimpleDbApplicationName));
            Assert.False(string.IsNullOrWhiteSpace(config.SimpleDbEnvironment));
            Assert.False(string.IsNullOrWhiteSpace(config.AmazonSimpleDbConfig.ServiceURL));
        }
    }
}