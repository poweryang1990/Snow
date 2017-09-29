// ReSharper disable InconsistentNaming
namespace UOKOFramework.OCR
{
    /// <summary>
    /// 身份证信息
    /// </summary>
    public class IDCard
    {
        /// <summary>
        /// 身份证号
        /// </summary>
        public string Id { get; internal set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Gender? Gender { get; internal set; }

        /// <summary>
        /// 民族
        /// </summary>
        public string Nation { get; internal set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public string Birth { get; internal set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; internal set; }

        /// <summary>
        /// 发证机关
        /// </summary>
        public string Authority { get; internal set; }

        /// <summary>
        /// 证件开始时间
        /// </summary>
        public string StartDate { get; internal set; }

        /// <summary>
        /// 证件结束时间
        /// </summary>
        public string EndDate { get; internal set; }
    }
}