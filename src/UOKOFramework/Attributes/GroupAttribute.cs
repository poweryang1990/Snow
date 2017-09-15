using System;

namespace UOKOFramework.Attributes
{
    /// <summary>
    /// 组Attribute
    /// </summary>
    public class GroupAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">组名</param>
        public GroupAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// 获取组名
        /// </summary>
        public string Name { get; }
    }
}
