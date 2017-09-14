﻿using System;

namespace UokoFramework
{
    /// <summary>
    /// 异常
    /// </summary>
    public static class Throws
    {
        /// <summary>
        /// 如果为null，则抛出ArgumentNullException的异常
        /// </summary>
        /// <param name="object">待检查的对象</param>
        /// <param name="paramName">原始参数名</param>
        public static void ArgumentNullException(object @object, string paramName)
        {
            if (@object == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        /// <summary>
        /// 如果数组为null或者长度为0，则抛出ArgumentNullException的异常
        /// </summary>
        /// <param name="array">待检查的数组</param>
        /// <param name="paramName">原始参数名</param>
        public static void ArgumentNullException(Array array, string paramName)
        {
            if (array == null || array.Length == 0)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        /// <summary>
        /// 如果数组为null或者长度为0，则抛出ArgumentNullException的异常
        /// </summary>
        /// <param name="string">待检查的字符串</param>
        /// <param name="paramName">原始参数名</param>
        public static void ArgumentNullException(string @string, string paramName)
        {
            if (string.IsNullOrWhiteSpace(@string))
            {
                throw new ArgumentNullException(paramName);
            }
        }
    }
}
