using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace XZ.Domain
{
    public abstract class Entity : IEntity
    {
        #region 领域事件
        private List<IDomainEvent> _domainEvents;

        //将当前的 _domainEvents 赋值给DomainEvents
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

        /// <summary>
        /// 添加领域事件
        /// </summary>
        /// <param name="itemEvent"></param>
        public void AddDomainEvent(IDomainEvent itemEvent)
        {
            _domainEvents = _domainEvents ?? new List<IDomainEvent>();
            _domainEvents.Add(itemEvent);
        }

        /// <summary>
        /// 移除领域事件
        /// </summary>
        /// <param name="itemEvent"></param>
        public void RemoveDomainEvent(IDomainEvent itemEvent)
        {
            _domainEvents?.Remove(itemEvent);
        }

        /// <summary>
        /// 清除所有领域事件
        /// </summary>
        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
        #endregion

        #region 实现仓储的公用类
        /// <summary>
        /// 设置指定名称的属性值。
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TrySetValue<TValue>(string propertyName, TValue value)
        {
            PropertyInfo propertyInfo = this.GetType().GetProperty(propertyName);

            if (propertyInfo != null)
            {
                propertyInfo.SetValue(this, value, null);

                return true;
            }

            return false;
        }

        /// <summary>
        /// 为指定的属性设置值(反射)
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="expression"></param>
        /// <param name="value"></param>
        public virtual void SetValue<TSource, TValue>(Expression<Func<TSource, TValue>> expression, TValue value)
        {
            if (this == null || expression == null)
                return;

            if (expression.Body.NodeType != ExpressionType.MemberAccess)
            {
                throw new InvalidOperationException("指定的lambda表达式无效(expression)");
            }

            PropertyInfo propertyInfo = ((MemberExpression)expression.Body).Member as PropertyInfo;
            if (propertyInfo == null)
            {
                throw new InvalidOperationException("指定的lambda表达式无效(expression)");
            }
            if (!propertyInfo.CanWrite)
            {
                throw new InvalidOperationException("指定的属性设定为只读");
            }

            propertyInfo.SetValue(this, value, null);
        }

        /// <summary>
        /// 获取指定名称的属性值。
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue<TValue>(string propertyName, out TValue value)
        {
            PropertyInfo propertyInfo = this.GetType().GetProperty(propertyName);

            if (propertyInfo != null)
            {
                value = (TValue)propertyInfo.GetValue(this, null);

                return true;
            }

            value = default(TValue);
            return false;
        }

        /// <summary>
        /// 获取指定属性的值(反射)
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="expression"></param>
        /// <param name="value"></param>
        public virtual TValue GetValue<TSource, TValue>(Expression<Func<TSource, TValue>> expression)
        {
            if (this != null && expression != null)
            {
                if (expression.Body.NodeType != ExpressionType.MemberAccess)
                {
                    throw new InvalidOperationException("指定的lambda表达式无效(expression)");
                }

                PropertyInfo propertyInfo = ((MemberExpression)expression.Body).Member as PropertyInfo;
                if (propertyInfo == null)
                {
                    throw new InvalidOperationException("指定的lambda表达式无效(expression)");
                }
                if (!propertyInfo.CanRead)
                {
                    throw new InvalidOperationException("指定的属性设定为只写");
                }

                return (TValue)propertyInfo.GetValue(this, null);
            }

            return default(TValue);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string ToString()
        {
            return base.ToString();
        }

        #endregion
    }
}
