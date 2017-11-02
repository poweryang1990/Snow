using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Snow.EF.Repository
{
    /// <summary>
    /// 数据库实体基本操作方法
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity: class 
    {
        /// <summary>
        /// 添加对象
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Add(TEntity entity);
        /// <summary>
        /// 批量添加对象
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="datas"></param>
        /// <returns></returns>
        IEnumerable<TEntity> BatchAdd(List<TEntity> datas);

        /// <summary>
        /// 删除单个实体
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        void Delete(TEntity entity);
        /// <summary>
        /// 删除满足条件的所有实体
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="conditions"></param>
        void Delete(Expression<Func<TEntity, bool>> conditions);
        /// <summary>
        /// 满足条件的实体个数
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        int Count(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Get(object id);
        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="conditions"></param>
        /// <returns></returns>
        TEntity Get(Expression<Func<TEntity, bool>> conditions);
        /// <summary>
        /// 查找
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="conditions"></param>
        /// <returns></returns>
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> conditions = null);

        /// <summary>
        /// 分页查找
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        ///  <param name="conditions">条件</param>
        /// <param name="orderbyLambda">排序</param>
        /// <param name="isAsc">是否是升序</param>
        /// <returns></returns>
        PagedList<TEntity> FindByPage<TKey>(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> conditions, Func<TEntity, TKey> orderbyLambda, bool isAsc=true);
        /// <summary>
        /// 通过Sql查找
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        IEnumerable<TEntity> SqlQuery(string sql);
        /// <summary>
        /// 带参数的Sql查找
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IEnumerable<TEntity> SqlQuery(string sql, params object[] parameters);
        /// <summary>
        /// sql分页查找
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PagedList<TEntity> SqlQuery(string sql, int pageIndex, int pageSize) ;
        /// <summary>
        /// 带参数的算起来分页查找
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        PagedList<TEntity> SqlQueryByPage(string sql, int pageIndex, int pageSize, params object[] parameters);
        /// <summary>
        /// 执行Sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        int ExecuteSqlCommand(string sql);
        /// <summary>
        /// 执行带参数的Sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int ExecuteSqlCommand(string sql,params object[] parameters);
    }
}
