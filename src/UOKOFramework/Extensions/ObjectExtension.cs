using System.Reflection;

namespace UOKOFramework.Extensions
{
    /// <summary>
    /// Object的扩展方法
    /// </summary>
    public static class ObjectExtension
    {
        /// <summary>
        /// 获取字段的值
        /// </summary>
        /// <typeparam name="TFiled">字段的数据类型</typeparam>
        /// <param name="object">this</param>
        /// <param name="name">字段的名字</param>
        /// <param name="bindingFlags"></param>
        /// <returns></returns>
        public static TFiled GetFiledValue<TFiled>(
            this object @object,
            string name,
            BindingFlags bindingFlags = BindingFlags.Instance)
        {
            var objectType = @object.GetType();
            var filed = objectType.GetField(name, bindingFlags);
            if (filed == null)
            {
                return default(TFiled);
            }
            return (TFiled)filed.GetValue(@object);
        }
    }
}
