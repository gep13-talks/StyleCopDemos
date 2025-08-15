using System.Collections.Generic;
using System.Reflection;

namespace irobyx.SimpleDb.Interfaces
{
    public interface ISimpleDbDomainConfiguration
    { 
        void CreateDomain(string domainName);
        void DeleteDomain(string domainName);
        IEnumerable<string> GetAvailableDomains();
        void InitializeApplicationDomains(Assembly assembly);
    }
}