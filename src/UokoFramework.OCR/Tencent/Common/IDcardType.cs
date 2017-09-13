using System;
using System.Collections.Generic;
using System.Text;

namespace UokoFramework.OCR.Tencent.Common
{
    /// <summary>
    /// 身份证正反面
    /// </summary>
    public enum IDcardType
    {
        /// <summary>
        /// 正面:为身份证有照片的一面
        /// </summary>
        Front = 0,

        /// <summary>
        /// 反面:为身份证有国徽的一面
        /// </summary>
        Reverse = 1
    }
}
