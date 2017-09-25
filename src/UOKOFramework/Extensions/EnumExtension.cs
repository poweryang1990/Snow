using System;
using System.Linq;
using System.Reflection;

namespace UOKOFramework.Extensions
{
    /// <summary>
    /// 枚举扩展方法
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// 获取枚举的DescriptionAttribute，如果没有就返回ToString的值
        /// </summary>
        /// <param name="value">枚举的值</param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
            var filed = GetFieldInfo(value);
            var description = filed.GetDescription();
            if (description == string.Empty)
            {
                description = value.ToString();
            }
            return description;
        }

        /// <summary>
        /// 获取枚举的Attribute
        /// </summary>
        /// <typeparam name="T">Attribute类型参数</typeparam>
        /// <param name="value">枚举的值</param>
        /// <returns>指定的Attribute</returns>
        public static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            return GetFieldInfo(value).GetCustomAttribute<T>(false);
        }

        /// <summary>
        /// 通过Flags枚举的值values获取对应的单独枚举集合
        /// </summary>
        /// <see cref="FlagsAttribute"/>
        /// <param name="flagsEnum"></param>
        /// <returns></returns>
        public static TFlagsEnum[] GetFlagEnums<TFlagsEnum>(this Enum flagsEnum)
        {
            var enumType = typeof(TFlagsEnum);
            var thisType = flagsEnum.GetType();
            if (enumType != thisType)
            {
                throw new ArgumentException($"[{enumType.FullName}]和[{thisType.FullName}]类别不匹配。");
            }

            if (thisType.GetCustomAttribute<FlagsAttribute>() == null)
            {
                throw new ArgumentException($"[{thisType.FullName}]不是Flags枚举。");
            }

            var currentValue = Convert.ToInt32(flagsEnum);

            return Enum.GetValues(thisType)
                .Cast<int>()
                .Where(value => (value & currentValue) == value)
                .Cast<TFlagsEnum>()
                .ToArray();
        }

        #region 私有成员

        private static FieldInfo GetFieldInfo(Enum value)
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            return type.GetField(name);
        }

        #endregion
    }
}