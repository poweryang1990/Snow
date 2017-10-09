namespace Snow
{
    /// <summary>
    /// 分页
    /// </summary>
    public class Paged
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Paged()
        {
            Page = 1;
            PageSize = 10;
        }

        /// <summary>
        /// 当前页
        /// </summary>
        public int Page { set; get; }

        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize { set; get; }
    }
}