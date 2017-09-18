using System.ComponentModel;
using System.Linq;
using System.Reflection;
using UOKOFramework.Attributes;

namespace UOKOFramework.Extensions
{
    /// <summary>
    /// MemberInfo的扩展方法
    /// </summary>
    public static class MemberInfoExtension
    {
        /// <summary>
        /// 获取DescriptionAttribute的Description属性
        /// </summary>
        /// <see cref="DescriptionAttribute"/>
        /// <param name="memberInfo">MemberInfo</param>
        /// <param name="inherit">是否包含继承的Attribute</param>
        /// <returns></returns>
        public static string GetDescription(this MemberInfo memberInfo, bool inherit = false)
        {
            var descriptionAttribute = memberInfo?.GetCustomAttribute<DescriptionAttribute>(inherit);
            if (descriptionAttribute == null)
            {
                return string.Empty;
            }
            return descriptionAttribute.Description;
        }

        /// <summary>
        /// 获取GroupAttribute的Name属性
        /// </summary>
        /// <see cref="GroupAttribute"/>
        /// <param name="memberInfo">MemberInfo</param>
        /// <param name="inherit">是否包含继承的Attribute</param>
        /// <returns></returns>
        public static string GetGroupName(this MemberInfo memberInfo, bool inherit = false)
        {
            var groupAttribute = memberInfo?.GetCustomAttribute<GroupAttribute>(inherit);
            if (groupAttribute == null)
            {
                return string.Empty;
            }
            return groupAttribute.Name;
        }
    }
}
