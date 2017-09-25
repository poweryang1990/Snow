using System;
using System.Collections.Generic;
using System.Linq;

namespace UOKOFramework.Extensions
{
    /// <summary>
    /// 枚举扩展方法
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// 转换为EnumObject
        /// </summary>
        /// <see cref="EnumObject"/>
        /// <param name="value">枚举的值</param>
        /// <returns></returns>
        public static EnumObject ToEnumObject(this Enum value)
        {
            return value;
        }

        /// <summary>
        /// 转换为EnumObject
        /// </summary>
        /// <see cref="EnumObject"/>
        /// <param name="enums"></param>
        /// <returns></returns>
        public static EnumObject[] ToEnumObjects(this IEnumerable<Enum> enums)
        {
            return enums?.Select(item => new EnumObject(item)).ToArray();
        }

        /// <summary>
        /// 获取枚举的DescriptionAttribute，如果没有就返回Enum的名字
        /// </summary>
        /// <param name="value">枚举的值</param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
            return value.ToEnumObject().ToString();
        }
    }
}