using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XZ.Domain;

namespace XZ.Infrastructure
{
    public abstract class Repository<TEntity, TDbContext> : IRepository<TEntity> where TEntity : Entity, IAggregateRoot where TDbContext : EFContext
    {
        protected virtual TDbContext DbContext { get; private set; }

        public Repository(TDbContext context)
        {
            this.DbContext = context;
        }
        public virtual IUnitOfWork UnitOfWork => DbContext;

        #region 数据库操作 同步
        public bool Remove(TEntity entity)
        {
            this.DbContext.Set<TEntity>().Remove(entity);

            return this.UnitOfWork.SaveEntities();
        }

        public int Remove(Expression<Func<TEntity, bool>> predicate)
        {
            var tmps = this.DbContext.Set<TEntity>().Where(predicate);
            foreach (var item in tmps)
            {
                this.DbContext.Set<TEntity>().Remove(item);
            }

            return this.UnitOfWork.SaveChanges();
        }

        public TEntity Find(params object[] values)
        {
            return this.DbContext.Set<TEntity>().Find(values);
        }

        public int Insert(TEntity entity)
        {
            this.DbContext.Set<TEntity>().Add(entity);

            return this.UnitOfWork.SaveChanges();
        }

        public int Update(TEntity entity, Expression<Func<TEntity, bool>> predicate)
        {
            Type entityType = typeof(TEntity);
            var properties = entityType.GetProperties();

            foreach (var item in this.DbContext.Set<TEntity>().Where(predicate))
            {
                foreach (var propertyInfo in properties)
                {
                    object newValue = entityType.GetProperty(propertyInfo.Name).GetValue(entity, null);

                    propertyInfo.SetValue(item, newValue, null);
                }
            }

            return this.UnitOfWork.SaveChanges();
        }
        public int Update(object anonymous, Expression<Func<TEntity, bool>> predicate)
        {
            var type = anonymous.GetType();
            //仅允许匿名类传值
            if (type.Namespace != null)
                throw new NotSupportedException("仅允许匿名类传值");

            var pis = type.GetProperties();
            var tmps = this.DbContext.Set<TEntity>().Where(predicate);

            foreach (var item in tmps)
            {
                var ipis = item.GetType().GetProperties();

                foreach (var property in pis)
                {
                    var newValue = property.GetValue(anonymous, null);

                    foreach (var ip in ipis)
                    {
                        if (ip.Name == property.Name)
                        {
                            ip.SetValue(item, newValue, null);
                            break;
                        }
                    }
                }
            }

            return this.UnitOfWork.SaveChanges();
        }
        public int Update<TValue>(Expression<Func<TEntity, TValue>> expression, TValue value, Expression<Func<TEntity, bool>> predicate)
        {
            var tmps = this.DbContext.Set<TEntity>().Where(predicate);
            foreach (var item in tmps)
            {
                item.SetValue(expression, value);
            }
            return this.UnitOfWork.SaveChanges();
        }

        public int Update<TValue1, TValue2>(Expression<Func<TEntity, TValue1>> expression1, TValue1 value1, Expression<Func<TEntity, TValue2>> expression2, TValue2 value2, Expression<Func<TEntity, bool>> predicate)
        {
            var tmps = this.DbContext.Set<TEntity>().Where(predicate);
            foreach (var item in tmps)
            {
                item.SetValue(expression1, value1);
                item.SetValue(expression2, value2);
            }

            return this.UnitOfWork.SaveChanges();
        }

        public int Update<TValue1, TValue2, TValue3>(Expression<Func<TEntity, TValue1>> expression1, TValue1 value1, Expression<Func<TEntity, TValue2>> expression2, TValue2 value2, Expression<Func<TEntity, TValue3>> expression3, TValue3 value3, Expression<Func<TEntity, bool>> predicate)
        {
            var tmps = this.DbContext.Set<TEntity>().Where(predicate);
            foreach (var item in tmps)
            {
                item.SetValue(expression1, value1);
                item.SetValue(expression2, value2);
                item.SetValue(expression3, value3);
            }

            return this.UnitOfWork.SaveChanges();
        }

        public int Update<TValue1, TValue2, TValue3, TValue4>(Expression<Func<TEntity, TValue1>> expression1, TValue1 value1, Expression<Func<TEntity, TValue2>> expression2, TValue2 value2, Expression<Func<TEntity, TValue3>> expression3, TValue3 value3, Expression<Func<TEntity, TValue4>> expression4, TValue4 value4, Expression<Func<TEntity, bool>> predicate)
        {
            var tmps = this.DbContext.Set<TEntity>().Where(predicate);
            foreach (var item in tmps)
            {
                item.SetValue(expression1, value1);
                item.SetValue(expression2, value2);
                item.SetValue(expression3, value3);
                item.SetValue(expression4, value4);
            }

            return this.UnitOfWork.SaveChanges();
        }

        public int Update<TValue1, TValue2, TValue3, TValue4, TValue5>(Expression<Func<TEntity, TValue1>> expression1, TValue1 value1, Expression<Func<TEntity, TValue2>> expression2, TValue2 value2, Expression<Func<TEntity, TValue3>> expression3, TValue3 value3, Expression<Func<TEntity, TValue4>> expression4, TValue4 value4, Expression<Func<TEntity, TValue5>> expression5, TValue5 value5, Expression<Func<TEntity, bool>> predicate)
        {
            var tmps = this.DbContext.Set<TEntity>().Where(predicate);
            foreach (var item in tmps)
            {
                item.SetValue(expression1, value1);
                item.SetValue(expression2, value2);
                item.SetValue(expression3, value3);
                item.SetValue(expression4, value4);
                item.SetValue(expression5, value5);
            }
            return this.UnitOfWork.SaveChanges();
        }

        #endregion

        #region 数据库操作 异步 

        public virtual Task<bool> RemoveAsync(TEntity entity)
        {
            return Task.FromResult(Remove(entity));
        }

        public async Task<int> RemoveAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            var tmps = this.DbContext.Set<TEntity>().Where(predicate);
            foreach (var item in tmps)
            {
                await Task.FromResult(this.DbContext.Set<TEntity>().Remove(item));
            }

            return await this.UnitOfWork.SaveChangesAsync();
        }

        public async Task<TEntity> FindAsync(params object[] values)
        {
            return await this.DbContext.Set<TEntity>().FindAsync(values);
        }
        public async Task<int> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await this.DbContext.Set<TEntity>().AddAsync(entity);

            return await this.UnitOfWork.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            Type entityType = typeof(TEntity);
            var properties = entityType.GetProperties();

            foreach (var item in this.DbContext.Set<TEntity>().Where(predicate))
            {
                foreach (var propertyInfo in properties)
                {
                    object newValue = entityType.GetProperty(propertyInfo.Name).GetValue(entity, null);

                    propertyInfo.SetValue(item, newValue, null);
                }
            }

            return await this.UnitOfWork.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(object anonymous, Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            var type = anonymous.GetType();
            //仅允许匿名类传值
            if (type.Namespace != null)
                throw new NotSupportedException("仅允许匿名类传值");

            var pis = type.GetProperties();
            var tmps = this.DbContext.Set<TEntity>().Where(predicate);

            foreach (var item in tmps)
            {
                var ipis = item.GetType().GetProperties();

                foreach (var property in pis)
                {
                    var newValue = property.GetValue(anonymous, null);

                    foreach (var ip in ipis)
                    {
                        if (ip.Name == property.Name)
                        {
                            ip.SetValue(item, newValue, null);
                            break;
                        }
                    }
                }
            }

            return await this.UnitOfWork.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync<TValue>(Expression<Func<TEntity, TValue>> expression, TValue value, Expression<Func<TEntity, bool>> predicate)
        {
            var tmps = this.DbContext.Set<TEntity>().Where(predicate);
            foreach (var item in tmps)
            {
                item.SetValue(expression, value);
            }
            return await this.UnitOfWork.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync<TValue1, TValue2>(Expression<Func<TEntity, TValue1>> expression1, TValue1 value1, Expression<Func<TEntity, TValue2>> expression2, TValue2 value2, Expression<Func<TEntity, bool>> predicate)
        {
            var tmps = this.DbContext.Set<TEntity>().Where(predicate);
            foreach (var item in tmps)
            {
                item.SetValue(expression1, value1);
                item.SetValue(expression2, value2);
            }

            return await this.UnitOfWork.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync<TValue1, TValue2, TValue3>(Expression<Func<TEntity, TValue1>> expression1, TValue1 value1, Expression<Func<TEntity, TValue2>> expression2, TValue2 value2, Expression<Func<TEntity, TValue3>> expression3, TValue3 value3, Expression<Func<TEntity, bool>> predicate)
        {
            var tmps = this.DbContext.Set<TEntity>().Where(predicate);
            foreach (var item in tmps)
            {
                item.SetValue(expression1, value1);
                item.SetValue(expression2, value2);
                item.SetValue(expression3, value3);
            }

            return await this.UnitOfWork.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync<TValue1, TValue2, TValue3, TValue4>(Expression<Func<TEntity, TValue1>> expression1, TValue1 value1, Expression<Func<TEntity, TValue2>> expression2, TValue2 value2, Expression<Func<TEntity, TValue3>> expression3, TValue3 value3, Expression<Func<TEntity, TValue4>> expression4, TValue4 value4, Expression<Func<TEntity, bool>> predicate)
        {
            var tmps = this.DbContext.Set<TEntity>().Where(predicate);
            foreach (var item in tmps)
            {
                item.SetValue(expression1, value1);
                item.SetValue(expression2, value2);
                item.SetValue(expression3, value3);
                item.SetValue(expression4, value4);
            }

            return await this.UnitOfWork.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync<TValue1, TValue2, TValue3, TValue4, TValue5>(Expression<Func<TEntity, TValue1>> expression1, TValue1 value1, Expression<Func<TEntity, TValue2>> expression2, TValue2 value2, Expression<Func<TEntity, TValue3>> expression3, TValue3 value3, Expression<Func<TEntity, TValue4>> expression4, TValue4 value4, Expression<Func<TEntity, TValue5>> expression5, TValue5 value5, Expression<Func<TEntity, bool>> predicate)
        {
            var tmps = this.DbContext.Set<TEntity>().Where(predicate);
            foreach (var item in tmps)
            {
                item.SetValue(expression1, value1);
                item.SetValue(expression2, value2);
                item.SetValue(expression3, value3);
                item.SetValue(expression4, value4);
                item.SetValue(expression5, value5);
            }
            return await this.UnitOfWork.SaveChangesAsync();
        }

        #endregion
    }
}
