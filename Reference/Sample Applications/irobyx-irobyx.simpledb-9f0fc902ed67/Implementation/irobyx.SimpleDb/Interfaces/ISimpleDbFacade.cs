using System;
using System.Collections.Generic;
using Amazon.SimpleDB;

namespace irobyx.SimpleDb.Interfaces
{
    public interface ISimpleDbFacade<T> where T : DbEntity 
    {
        string DomainName { get; }
        AmazonSimpleDB Service { get; }
        ISimpleDbMapper<T> SimpleDbMapper { get; }
        
        IEnumerable<T> GetAllItems();
        IEnumerable<T> Query(string selectExpression);
        T GetItemById(Guid id);

        void CreateItem(T entity);
        void UpdateItem(T entity);
        void UpdateItemAttribute(Guid id, string attributeName, string attributeValue);
        void DeleteItem(Guid id);
 
    }
}