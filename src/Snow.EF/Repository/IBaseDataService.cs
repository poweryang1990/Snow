using System.Collections.Generic;

namespace Snow.EF.Repository
{
    /// <summary>
    /// 基础数据服务
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBaseDataService<TDto,TEntity> where TDto : class where TEntity : class
    {
        /// <summary>
        /// 添加对象
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="dto"></param>
        /// <returns></returns>
        TDto Add(TDto dto);
        /// <summary>
        /// 批量添加对象
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="datas"></param>
        /// <returns></returns>
        IEnumerable<TDto> BatchAdd(List<TDto> datas);

        /// <summary>
        /// 根据主键ID删除数据
        /// </summary>
        /// <param name="id"></param>
        void Delete(object id);

        /// <summary>
        /// 根据主键获取单个实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TDto Get(object id);
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        IEnumerable<TDto> GetAll();
    }
}