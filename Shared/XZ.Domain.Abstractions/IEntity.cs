using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace XZ.Domain
{
    public interface IEntity
    {
        bool TrySetValue<TValue>(string propertyName, TValue value);
        void SetValue<TSource, TValue>(Expression<Func<TSource, TValue>> expression, TValue value);
        bool TryGetValue<TValue>(string propertyName, out TValue value);
        TValue GetValue<TSource, TValue>(Expression<Func<TSource, TValue>> expression);
        bool Equals(object obj);
        int GetHashCode();
        string ToString();
    }
}
