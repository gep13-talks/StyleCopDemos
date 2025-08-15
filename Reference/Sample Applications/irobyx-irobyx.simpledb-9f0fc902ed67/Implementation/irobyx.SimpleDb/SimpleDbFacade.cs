using System;
using System.Collections.Generic;
using System.Linq;

using Amazon;
using Amazon.SimpleDB;
using Amazon.SimpleDB.Model;

using irobyx.SimpleDb.Interfaces;

namespace irobyx.SimpleDb
{
    public class SimpleDbFacade<T> : ISimpleDbFacade<T> where T : DbEntity
    {
        public AmazonSimpleDB Service { get; private set; }
        public string DomainName { get; private set; }
        public ISimpleDbMapper<T> SimpleDbMapper { get; private set; }

        public SimpleDbFacade(ISimpleDbConfiguration config, ISimpleDbMapper<T> simpleDbMappper)
        {
            this.SimpleDbMapper = simpleDbMappper;
            this.Service = AWSClientFactory.CreateAmazonSimpleDBClient
             (config.SimpleDbAccessKey, config.SimpleDbSecretKey, config.AmazonSimpleDbConfig);
            this.DomainName = config.GetDomainName(typeof(T));
        }
        
        public IEnumerable<T> GetAllItems()
        {
            string selectExpression = "select * from " + DomainName;
            var list = new List<T>();
            SelectRequest selectRequestAction = new SelectRequest().WithSelectExpression(selectExpression).WithConsistentRead(true);
            SelectResponse selectResponse = Service.Select(selectRequestAction);
            if (selectResponse.IsSetSelectResult())
            {
                foreach (var item in selectResponse.SelectResult.Item)
                { 
                    var T = this.SimpleDbMapper.MapAttributesToEntity(item.Attribute);
                    T.Id = Guid.Parse(item.Name);
                    list.Add(T);
                }
            }
            return list;
        }

        public IEnumerable<T> Query(string selectExpression)
        {
            var list = new List<T>();
            SelectRequest selectRequestAction = new SelectRequest().WithSelectExpression(selectExpression).WithConsistentRead(true);
            SelectResponse selectResponse = Service.Select(selectRequestAction);
            if (selectResponse.IsSetSelectResult())
            {
                foreach (var item in selectResponse.SelectResult.Item)
                {
                    var T = this.SimpleDbMapper.MapAttributesToEntity(item.Attribute);
                    T.Id = Guid.Parse(item.Name);
                    list.Add(T);
                }
            }
            return list;
        }

        public T GetItemById(Guid id)
        {
            var req = new GetAttributesRequest().WithDomainName(DomainName).WithItemName(id.ToString()).WithConsistentRead(true);
            var response = Service.GetAttributes(req);
            if (response.IsSetGetAttributesResult() && response.GetAttributesResult.Attribute.Any())
            {
                var T = this.SimpleDbMapper.MapAttributesToEntity(response.GetAttributesResult.Attribute);
                T.Id = id; 
                return T;
            }
            return null;
        }

        public void CreateItem(T entity)
        {
            if (entity.Id == Guid.Empty)
                entity.Id = Guid.NewGuid();
            var request = new PutAttributesRequest().WithDomainName(this.DomainName).WithItemName(entity.Id.ToString());
            request.Attribute = this.SimpleDbMapper.MapEntityToAttributes(entity).ToList(); ;
            var response = this.Service.PutAttributes(request);
        }

        public void UpdateItem(T entity)
        {
            var request = new PutAttributesRequest().WithDomainName(DomainName).WithItemName(entity.Id.ToString());
            request.Attribute = this.SimpleDbMapper.MapEntityToAttributes(entity).ToList();
            request.Attribute.ForEach(o => o.WithReplace(true));
            var response = this.Service.PutAttributes(request);
        }

        public void UpdateItemAttribute(Guid id, string attributeName, string attributeValue)
        {
            var replaceableAttribute = new ReplaceableAttribute().WithName(attributeName).WithValue(attributeValue).WithReplace(true);
            var request = new PutAttributesRequest().WithDomainName(DomainName).WithItemName(id.ToString()).WithAttribute(replaceableAttribute);
            var response = this.Service.PutAttributes(request);
        }

        public void DeleteItem(Guid id)
        {
            var request = new DeleteAttributesRequest().WithDomainName(DomainName).WithItemName(id.ToString());
            var response = this.Service.DeleteAttributes(request);
        } 
    }
}

