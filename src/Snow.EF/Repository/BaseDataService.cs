using AutoMapper;
using Mehdime.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snow.EF.Repository
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public class BaseDataService<TDto, TEntity> : IBaseDataService<TDto,TEntity> where TDto : class where TEntity : class
    {

        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IRepository<TEntity> _baseDal;
        private readonly IMapper _mapper;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContextScopeFactory"></param>
        /// <param name="baseDal"></param>
        public BaseDataService(IDbContextScopeFactory dbContextScopeFactory, IRepository<TEntity> baseDal)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _baseDal = baseDal;

            //DTO和Entity模型的相互映射
            _mapper=new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TDto, TEntity>();
                cfg.CreateMap<TEntity,TDto>();
            }).CreateMapper();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public TDto Add(TDto dto)
        {
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                var result = _baseDal.Add(_mapper.Map<TEntity>(dto));
                dbContextScope.SaveChanges();
                return _mapper.Map<TDto>(result);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        public IEnumerable<TDto> BatchAdd(List<TDto> datas)
        {
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                var result = _baseDal.BatchAdd(datas.Select(_mapper.Map<TEntity>).ToList());
                dbContextScope.SaveChanges();
                return result.Select(_mapper.Map<TDto>);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void Delete(object id)
        {
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                var entity = _baseDal.Get(id);
                _baseDal.Delete(entity);
                dbContextScope.SaveChanges();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TDto Get(object id)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                var entity = _baseDal.Get(id);
                return _mapper.Map<TDto>(entity);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TDto> GetAll()
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                var result = _baseDal.Find();
                return result.Select(_mapper.Map<TDto>).ToList();
            }
        }
    }
}
