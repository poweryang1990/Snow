using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Reflection;
namespace Snow.EF.Extensions

{
    /// <summary>
    /// 上下文扩展辅助操作类
    /// </summary>
    public static class DbContextExtensions
    {
        /// <summary>
        /// 仅对webapi请求的接口的按需修改拓展方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbContext"></param>
        /// <param name="entity">初始化了更新主键的实体</param>
        /// <param name="propertyExpression">要更新的字段集合</param>
        /// <returns>返回受影响行数</returns>
        public static void Update<T>(this DbContext dbContext, T entity, Expression<Func<T, object>> propertyExpression) where T : class
        {
            dbContext.Configuration.ValidateOnSaveEnabled = false;//按需修改明确知道修改的字段
            var entry = dbContext.Entry(entity);
            entry.State = EntityState.Unchanged;
            ReadOnlyCollection<MemberInfo> memberInfos = ((dynamic)propertyExpression.Body).Members;
            foreach (MemberInfo memberInfo in memberInfos)
            {
                dbContext.Entry(entity).Property(memberInfo.Name).IsModified = true;
            }
        }
       
    }
}
