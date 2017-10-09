using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Snow.Attributes;

namespace Snow.Extensions
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
            return memberInfo
                ?.GetCustomAttribute<DescriptionAttribute>(inherit)
                ?.Description;
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
            return memberInfo
                ?.GetCustomAttribute<GroupAttribute>(inherit)
                ?.Name;
        }
    }
}
