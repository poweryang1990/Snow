using System.Reflection;

namespace Snow.Extensions
{
    /// <summary>
    /// object的扩展方法
    /// </summary>
    public static class ObjectExtension
    {
        /// <summary>
        /// 获取当前对象的类型所属的程序集
        /// </summary>
        /// <param name="currentObject">this</param>
        /// <returns>当前对象的类型所属的程序集</returns>
        public static Assembly GetAssembly(this object currentObject)
        {
            return currentObject.GetType().Assembly;
        }
    }
}
