using System;
using System.Collections.Generic;
using System.Reflection;
using Amazon.SimpleDB.Model;
using irobyx.SimpleDb.Interfaces;
using Attribute = Amazon.SimpleDB.Model.Attribute;

namespace irobyx.SimpleDb
{
    public class SimpleDbMapper<T> : ISimpleDbMapper<T> where T : DbEntity
    {
        private readonly ISimpleDbConverter _simpleDbConverter = new SimpleDbConverter();

        public IEnumerable<ReplaceableAttribute> MapEntityToAttributes(T entity)
        {
            var info = typeof(T).GetProperties();
            var returnValue = new List<ReplaceableAttribute>();
            foreach (var propertyInfo in info)
            {
                if (propertyInfo.Name != entity.GetPropertyName(o => o.Id))
                {
                    var attrib = GetAttribute(propertyInfo, entity);
                    returnValue.Add(attrib);
                }
            }
            return returnValue;
        }

        public T MapAttributesToEntity(IEnumerable<Attribute> attributes)
        {
            var entity = Activator.CreateInstance<T>();
            foreach (var attribute in attributes)
            {
                var theType = entity.GetType();
                var property = theType.GetProperty(attribute.Name);
                if (property != null)
                    property.SetValue(entity, _simpleDbConverter.ConvertFromSimpleDb(property.PropertyType, attribute.Value), null);
            }
            return entity;
        }

        private ReplaceableAttribute GetAttribute(PropertyInfo propertyInfo, T entity)
        {
            var attrib = new ReplaceableAttribute();
            attrib.Name = propertyInfo.Name;
            var type = propertyInfo.PropertyType;
            var value = propertyInfo.GetValue(entity, null);
            attrib.Value = _simpleDbConverter.ConvertToSimpleDb(type, value);
            return attrib;
        }
    }
}