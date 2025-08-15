using System.Collections.Generic;
using Amazon.SimpleDB.Model;

namespace irobyx.SimpleDb.Interfaces
{
    public interface ISimpleDbMapper<T> where T: DbEntity
    {
        IEnumerable<ReplaceableAttribute> MapEntityToAttributes(T entity);
        T MapAttributesToEntity(IEnumerable<Attribute> attributes);
    }
}