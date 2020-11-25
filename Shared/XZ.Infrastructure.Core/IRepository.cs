
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using XZ.Domain;

namespace XZ.Infrastructure
{
    public interface IRepository<TEntity> where TEntity : Entity, IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }

        #region 数据库操作 同步
        TEntity Find(params object[] values);
        int Insert(TEntity entity);
        int Update(TEntity entity, Expression<Func<TEntity, bool>> predicate);
        int Update(object anonymous, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 修改指定条件下的实体属性。
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="expression"></param>
        /// <param name="value"></param>
        /// <param name="predicate"></param>
        /// <returns>受影响的行数</returns>
        int Update<TValue>(Expression<Func<TEntity, TValue>> expression, TValue value, Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 修改指定条件下的实体属性。
        /// </summary>
        /// <typeparam name="TValue1"></typeparam>
        /// <typeparam name="TValue2"></typeparam>
        /// <param name="expression1"></param>
        /// <param name="value1"></param>
        /// <param name="expression2"></param>
        /// <param name="value2"></param>
        /// <param name="predicate"></param>
        /// <returns>受影响的行数</returns>
        int Update<TValue1, TValue2>(Expression<Func<TEntity, TValue1>> expression1, TValue1 value1, Expression<Func<TEntity, TValue2>> expression2, TValue2 value2, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 修改指定条件下的实体属性。
        /// </summary>
        /// <typeparam name="TValue1"></typeparam>
        /// <typeparam name="TValue2"></typeparam>
        /// <typeparam name="TValue3"></typeparam>
        /// <param name="expression1"></param>
        /// <param name="value1"></param>
        /// <param name="expression2"></param>
        /// <param name="value2"></param>
        /// <param name="expression3"></param>
        /// <param name="value3"></param>
        /// <param name="predicate"></param>
        /// <returns>受影响的行数</returns>
        int Update<TValue1, TValue2, TValue3>(Expression<Func<TEntity, TValue1>> expression1, TValue1 value1, Expression<Func<TEntity, TValue2>> expression2, TValue2 value2, Expression<Func<TEntity, TValue3>> expression3, TValue3 value3, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 修改指定条件下的实体属性。
        /// </summary>
        /// <typeparam name="TValue1"></typeparam>
        /// <typeparam name="TValue2"></typeparam>
        /// <typeparam name="TValue3"></typeparam>
        /// <typeparam name="TValue4"></typeparam>
        /// <param name="expression1"></param>
        /// <param name="value1"></param>
        /// <param name="expression2"></param>
        /// <param name="value2"></param>
        /// <param name="expression3"></param>
        /// <param name="value3"></param>
        /// <param name="expression4"></param>
        /// <param name="value4"></param>
        /// <param name="predicate"></param>
        /// <returns>受影响的行数</returns>
        int Update<TValue1, TValue2, TValue3, TValue4>(Expression<Func<TEntity, TValue1>> expression1, TValue1 value1, Expression<Func<TEntity, TValue2>> expression2, TValue2 value2, Expression<Func<TEntity, TValue3>> expression3, TValue3 value3, Expression<Func<TEntity, TValue4>> expression4, TValue4 value4, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 修改指定条件下的实体属性。
        /// </summary>
        /// <typeparam name="TValue1"></typeparam>
        /// <typeparam name="TValue2"></typeparam>
        /// <typeparam name="TValue3"></typeparam>
        /// <typeparam name="TValue4"></typeparam>
        /// <typeparam name="TValue5"></typeparam>
        /// <param name="expression1"></param>
        /// <param name="value1"></param>
        /// <param name="expression2"></param>
        /// <param name="value2"></param>
        /// <param name="expression3"></param>
        /// <param name="value3"></param>
        /// <param name="expression4"></param>
        /// <param name="value4"></param>
        /// <param name="expression5"></param>
        /// <param name="value5"></param>
        /// <param name="predicate"></param>
        /// <returns>受影响的行数</returns>
        int Update<TValue1, TValue2, TValue3, TValue4, TValue5>(Expression<Func<TEntity, TValue1>> expression1, TValue1 value1, Expression<Func<TEntity, TValue2>> expression2, TValue2 value2, Expression<Func<TEntity, TValue3>> expression3, TValue3 value3, Expression<Func<TEntity, TValue4>> expression4, TValue4 value4, Expression<Func<TEntity, TValue5>> expression5, TValue5 value5, Expression<Func<TEntity, bool>> predicate);

        bool Remove(TEntity entity);
        int Remove(Expression<Func<TEntity, bool>> predicate);
        #endregion

        #region 数据库操作 异步
        Task<TEntity> FindAsync(params object[] values);
        Task<int> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
        /// <summary>
        /// 使用指定的实体更新指定的数据库内容。
        /// </summary>
        /// <param name="entity">指定一个实体。</param>
        /// <param name="predicate">筛选条件。</param>
        /// <returns>返回影响的行数。</returns>
        Task<int> UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// 修改指定条件下的实体属性。
        /// </summary>
        /// <param name="anonymous">匿名类的属性对应关系</param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(object anonymous, Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// 修改指定条件下的实体属性。
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="expression"></param>
        /// <param name="value"></param>
        /// <param name="predicate"></param>
        /// <returns>受影响的行数</returns>
        Task<int> UpdateAsync<TValue>(Expression<Func<TEntity, TValue>> expression, TValue value, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 修改指定条件下的实体属性。
        /// </summary>
        /// <typeparam name="TValue1"></typeparam>
        /// <typeparam name="TValue2"></typeparam>
        /// <param name="expression1"></param>
        /// <param name="value1"></param>
        /// <param name="expression2"></param>
        /// <param name="value2"></param>
        /// <param name="predicate"></param>
        /// <returns>受影响的行数</returns>
        Task<int> UpdateAsync<TValue1, TValue2>(Expression<Func<TEntity, TValue1>> expression1, TValue1 value1, Expression<Func<TEntity, TValue2>> expression2, TValue2 value2, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 修改指定条件下的实体属性。
        /// </summary>
        /// <typeparam name="TValue1"></typeparam>
        /// <typeparam name="TValue2"></typeparam>
        /// <typeparam name="TValue3"></typeparam>
        /// <param name="expression1"></param>
        /// <param name="value1"></param>
        /// <param name="expression2"></param>
        /// <param name="value2"></param>
        /// <param name="expression3"></param>
        /// <param name="value3"></param>
        /// <param name="predicate"></param>
        /// <returns>受影响的行数</returns>
        Task<int> UpdateAsync<TValue1, TValue2, TValue3>(Expression<Func<TEntity, TValue1>> expression1, TValue1 value1, Expression<Func<TEntity, TValue2>> expression2, TValue2 value2, Expression<Func<TEntity, TValue3>> expression3, TValue3 value3, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 修改指定条件下的实体属性。
        /// </summary>
        /// <typeparam name="TValue1"></typeparam>
        /// <typeparam name="TValue2"></typeparam>
        /// <typeparam name="TValue3"></typeparam>
        /// <typeparam name="TValue4"></typeparam>
        /// <param name="expression1"></param>
        /// <param name="value1"></param>
        /// <param name="expression2"></param>
        /// <param name="value2"></param>
        /// <param name="expression3"></param>
        /// <param name="value3"></param>
        /// <param name="expression4"></param>
        /// <param name="value4"></param>
        /// <param name="predicate"></param>
        /// <returns>受影响的行数</returns>
        Task<int> UpdateAsync<TValue1, TValue2, TValue3, TValue4>(Expression<Func<TEntity, TValue1>> expression1, TValue1 value1, Expression<Func<TEntity, TValue2>> expression2, TValue2 value2, Expression<Func<TEntity, TValue3>> expression3, TValue3 value3, Expression<Func<TEntity, TValue4>> expression4, TValue4 value4, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 修改指定条件下的实体属性。
        /// </summary>
        /// <typeparam name="TValue1"></typeparam>
        /// <typeparam name="TValue2"></typeparam>
        /// <typeparam name="TValue3"></typeparam>
        /// <typeparam name="TValue4"></typeparam>
        /// <typeparam name="TValue5"></typeparam>
        /// <param name="expression1"></param>
        /// <param name="value1"></param>
        /// <param name="expression2"></param>
        /// <param name="value2"></param>
        /// <param name="expression3"></param>
        /// <param name="value3"></param>
        /// <param name="expression4"></param>
        /// <param name="value4"></param>
        /// <param name="expression5"></param>
        /// <param name="value5"></param>
        /// <param name="predicate"></param>
        /// <returns>受影响的行数</returns>
        Task<int> UpdateAsync<TValue1, TValue2, TValue3, TValue4, TValue5>(Expression<Func<TEntity, TValue1>> expression1, TValue1 value1, Expression<Func<TEntity, TValue2>> expression2, TValue2 value2, Expression<Func<TEntity, TValue3>> expression3, TValue3 value3, Expression<Func<TEntity, TValue4>> expression4, TValue4 value4, Expression<Func<TEntity, TValue5>> expression5, TValue5 value5, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 使用指定的实体更新指定的数据库内容。
        /// </summary>
        /// <param name="entity">指定一个实体。</param>
        /// <param name="predicate">筛选条件。</param>
        /// <returns>返回影响的行数。</returns>
        Task<bool> RemoveAsync(TEntity entity);
        Task<int> RemoveAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        #endregion
    }
}
