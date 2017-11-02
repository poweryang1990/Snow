using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Mehdime.Entity;

namespace Snow.EF.Repository
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TDbContext"></typeparam>
    public abstract class RepositoryBase<TEntity, TDbContext> : IRepository<TEntity> where TEntity : class where TDbContext : DbContext
    {
        private readonly IAmbientDbContextLocator _contextLocator;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctxLocator"></param>
        protected RepositoryBase(IAmbientDbContextLocator ctxLocator)
        {
            if (ctxLocator == null) throw new ArgumentNullException(nameof(ctxLocator));
            _contextLocator = ctxLocator;
        }
        /// <summary>
        /// 
        /// </summary>
        protected TDbContext DBContext => _contextLocator.Get<TDbContext>();
        /// <summary>
        /// 
        /// </summary>
        protected DbSet<TEntity> DbSet => DBContext.Set<TEntity>();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TEntity Add(TEntity entity)
        {
            return DbSet.Add(entity);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> BatchAdd(List<TEntity> datas)
        {
            foreach (var entity in datas)
            {
                yield return DbSet.Add(entity);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conditions"></param>
        public void Delete(Expression<Func<TEntity, bool>> conditions)
        {
            var list = Find(conditions);
            foreach (var item in list)
            {
                Delete(item);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(TEntity entity)
        {
            var entry = DBContext.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            entry.State = EntityState.Deleted;
            DbSet.Remove(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int ExecuteSqlCommand(string sql)
        {
            return DBContext.Database.ExecuteSqlCommand(sql);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return DBContext.Database.ExecuteSqlCommand(sql, parameters);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> conditions = null)
        {
            if (conditions != null)
            {
                return DbSet.Where(conditions).ToList();
            }
            return DbSet.ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="conditions"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public PagedList<TEntity> FindByPage<TKey>(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> conditions, Func<TEntity, TKey> orderbyLambda, bool isAsc = true)
        {
            var query = conditions == null ? DbSet : DbSet.Where(conditions);

           var  pageResult= new PagedList<TEntity>()
           {
               Page = pageIndex,
               PageSize = pageSize,
               TotalCount = query.Count()
           };
            if (isAsc)
            {
                pageResult.Items = query.OrderBy(orderbyLambda)
                    .AsQueryable()
                    .Skip(() => (pageIndex - 1) * pageSize)
                    .Take(() => pageSize)
                    .ToList();
            }
            else
            {
                pageResult.Items = query.OrderByDescending(orderbyLambda)
                    .AsQueryable()
                    .Skip(() => (pageIndex - 1) * pageSize)
                    .Take(() => pageSize)
                    .ToList();
            }
            return pageResult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public TEntity Get(Expression<Func<TEntity, bool>> conditions)
        {
            return DbSet.FirstOrDefault(conditions);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity Get(object id)
        {
            return DbSet.Find(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> SqlQuery(string sql)
        {
            return DBContext.Database.SqlQuery<TEntity>(sql).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> SqlQuery(string sql, params object[] parameters)
        {
            return DBContext.Database.SqlQuery<TEntity>(sql, parameters).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagedList<TEntity> SqlQuery(string sql, int pageIndex, int pageSize)
        {
            var query = DBContext.Database.SqlQuery<TEntity>(sql);
            return new PagedList<TEntity>()
            {
                Page = pageIndex,
                PageSize = pageSize,
                Items = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList(),
                TotalCount = query.Count()
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public PagedList<TEntity> SqlQueryByPage(string sql, int pageIndex, int pageSize, params object[] parameters)
        {
            var query = DBContext.Database.SqlQuery<TEntity>(sql, parameters);
            return new PagedList<TEntity>()
            {
                Page= pageIndex,
                PageSize = pageSize,
                Items = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList(),
                TotalCount = query.Count()
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public int Count(Expression<Func<TEntity, bool>> expression)
        {
            return DbSet.Count(expression);
        }
    }
}
