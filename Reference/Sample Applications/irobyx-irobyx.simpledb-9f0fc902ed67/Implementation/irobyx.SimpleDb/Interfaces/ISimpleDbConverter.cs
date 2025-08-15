using System;

namespace irobyx.SimpleDb.Interfaces
{
    public interface ISimpleDbConverter
    {
        string ConvertToSimpleDb(Type type, object value);
        dynamic ConvertFromSimpleDb(Type type, string value);
    }
}