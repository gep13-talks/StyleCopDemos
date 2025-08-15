using System;

namespace irobyx.SimpleDb
{
    using System.Collections.Specialized;
    using System.Configuration;
    using Amazon.SimpleDB;
    using Interfaces;

    public sealed class SimpleDbConfiguration : ISimpleDbConfiguration
    {
        public string SimpleDbAccessKey { get; private set;}
        public string SimpleDbSecretKey { get; private set; }
        public string SimpleDbApplicationName { get; private set; }
        public string SimpleDbEnvironment { get; private set; }
        public AmazonSimpleDBConfig AmazonSimpleDbConfig { get; private set; }

        public SimpleDbConfiguration()
        {
            NameValueCollection appConfig = ConfigurationManager.AppSettings;
            this.SimpleDbAccessKey = appConfig[Constants.SimpleDbAccessKey];
            this.SimpleDbSecretKey = appConfig[Constants.SimpleDbSecretKey];
            this.SimpleDbApplicationName = appConfig[Constants.SimpleDbApplicationName];
            this.SimpleDbEnvironment = appConfig[Constants.SimpleDbEnvironment];
            this.AmazonSimpleDbConfig = new AmazonSimpleDBConfig().WithServiceURL(appConfig[Constants.SimpleDbRegion]);
        }

        public string GetDomainName(Type type)
        {
            var theDomainName = string.Concat(this.SimpleDbEnvironment, "_", this.SimpleDbApplicationName, "_", type.Name);
            return theDomainName;
        }

    }
}