using System;

namespace Snow.EF.Repository
{
    /// <summary>
    /// 基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class BaseEntity<TKey>
    {
        /// <summary>
        /// 主键
        /// </summary>
        public TKey Id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreatedDate { get; set; }
        /// <summary>
        /// 最后被修改时间
        /// </summary>
        public DateTime? LastMotifiedDate { get; set; }
    }
}