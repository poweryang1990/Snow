namespace UOKOFramework
{
    /// <summary>
    /// 系统运行的环境类型
    /// </summary>
    public enum EnvironmentType
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// 本地开发环境
        /// </summary>
        Local = 1,
        /// <summary>
        /// 测试环境
        /// </summary>
        Test = 2,
        /// <summary>
        /// 预发布环境
        /// </summary>
        Pre = 3,
        /// <summary>
        /// 线上环境
        /// </summary>
        Online = 4,
    }
}
