using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

using Amazon;
using Amazon.SimpleDB;
using Amazon.SimpleDB.Model;

using irobyx.SimpleDb.Interfaces;

namespace irobyx.SimpleDb
{
    public class SimpleDbDomainConfiguration : ISimpleDbDomainConfiguration
    {
        private readonly AmazonSimpleDB _service;

        private readonly ISimpleDbConfiguration _config;

        public SimpleDbDomainConfiguration(ISimpleDbConfiguration config)
        {
            _config = config;
            _service = AWSClientFactory.CreateAmazonSimpleDBClient
                (config.SimpleDbAccessKey, config.SimpleDbSecretKey, config.AmazonSimpleDbConfig);
        }

        public void CreateDomain(string domainName)
        {
            var createDomain = new CreateDomainRequest().WithDomainName(domainName);
            var response = _service.CreateDomain(createDomain);
        }

        public void DeleteDomain(string domainName)
        {
            var request = new DeleteDomainRequest().WithDomainName(domainName);
            var response = _service.DeleteDomain(request);
        }

        public IEnumerable<string> GetAvailableDomains()
        {
            var returnValue = new List<string>();
            var response = _service.ListDomains(new ListDomainsRequest());
            if (response.IsSetListDomainsResult())
            {
                ListDomainsResult listDomainsResult = response.ListDomainsResult;
                foreach (string domain in listDomainsResult.DomainName)
                {
                    returnValue.Add(domain);
                    Debug.WriteLine(domain);
                }
            }
            return returnValue;
        }

        public void InitializeApplicationDomains(Assembly assembly)
        {
            IEnumerable<string> existingDomains = this.GetAvailableDomains();
            foreach (var type in assembly.GetTypes())
            {
                if (type.IsSubclassOf(typeof(DbEntity)) && !type.IsAbstract)
                {
                    var theDomainName = _config.GetDomainName(type);
                    if (!existingDomains.Any(o => o == theDomainName))
                    {
                        this.CreateDomain(theDomainName);
                    }
                }
            }
        } 
    }
}