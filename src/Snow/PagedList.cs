using System;
using System.Collections.Generic;

namespace Snow
{
    /// <summary>
    /// 分页的列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedList<T> : Paged
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public PagedList()
        {
            Items = new List<T>();
        }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalCount { set; get; }

        /// <summary>
        /// 记录列表
        /// </summary>
        public IList<T> Items { set; get; }

        /// <summary>
        /// 页数
        /// </summary>
        public int PageCount
        {
            get
            {
                if (TotalCount < 0)
                {
                    throw new ArgumentException(nameof(TotalCount));
                }
                if (PageSize <= 0)
                {
                    throw new ArgumentException(nameof(PageSize));
                }
                return (TotalCount + PageSize - 1) / PageSize;
            }
        }
    }

}
