using System;

using Amazon.SimpleDB;

namespace irobyx.SimpleDb.Interfaces
{
    public interface ISimpleDbConfiguration
    {
        string SimpleDbAccessKey { get; }
        string SimpleDbSecretKey { get; }
        string SimpleDbApplicationName { get; }
        string SimpleDbEnvironment { get; }
        AmazonSimpleDBConfig AmazonSimpleDbConfig { get; }
        string GetDomainName(Type type);
    }
}