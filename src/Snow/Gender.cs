using System.ComponentModel;

namespace Snow
{
    /// <summary>
    /// 性别
    /// </summary>
    public enum Gender
    {
        /// <summary>
        /// 男性
        /// </summary>
        [Description("男")]
        Male = 0,

        /// <summary>
        /// 女性
        /// </summary>
        [Description("女")]
        Female = 1
    }
}
