namespace Snow
{
    /// <summary>
    /// 范围
    /// </summary>
    /// <typeparam name="T">类型参数</typeparam>
    public class Range<T>
    {
        /// <summary>
        /// 开始
        /// </summary>
        public T Begin { set; get; }

        /// <summary>
        /// 结束
        /// </summary>
        public T End { set; get; }
    }
}
