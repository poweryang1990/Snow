using System;

namespace UOKOFramework.Extensions
{
    /// <summary>
    /// Int的扩展方法
    /// </summary>
    public static class IntExtension
    {
        /// <summary>
        /// 转换为Guid
        /// </summary>
        /// <param name="number">int32的数字</param>
        /// <returns>Guid</returns>
        public static Guid ToGuid(this int number)
        {
            return new Guid(number, 0, 0, new byte[8]);
        }
    }

}
