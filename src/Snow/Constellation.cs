using System.ComponentModel;

namespace Snow
{
    /// <summary>
    /// 星座
    /// </summary>
    public enum Constellation
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        Unknown = 0,

        /// <summary>
        /// 白羊座[3月21日-4月19日]
        /// </summary>
        [Description("白羊座")]
        Aries = 1,

        /// <summary>
        /// 金牛座[4月20日-5月20日]
        /// </summary>
        [Description("金牛座")]
        Taurus = 2,

        /// <summary>
        /// 双子座[5月21日-6月21日]
        /// </summary>
        [Description("双子座")]
        Gemini = 3,

        /// <summary>
        /// 巨蟹座[6月22日-7月22日]
        /// </summary>
        [Description("巨蟹座")]
        Cancer = 4,

        /// <summary>
        /// 狮子座[7月23日-8月22日]
        /// </summary>
        [Description("狮子座")]
        Leo = 5,

        /// <summary>
        /// 处女座[8月23日-9月22日]
        /// </summary>
        [Description("处女座")]
        Virgo = 6,

        /// <summary>
        /// 天秤座[9月23日-10月23日]
        /// </summary>
        [Description("天秤座")]
        Libra = 7,

        /// <summary>
        /// 天蝎座[10月24日-11月21日]
        /// </summary>
        [Description("天蝎座")]
        Scorpio = 8,

        /// <summary>
        /// 射手座[11月22日-12月21日]
        /// </summary>
        [Description("射手座")]
        Sagittarius = 9,

        /// <summary>
        /// 摩羯座[12月22日-1月19日]
        /// </summary>
        [Description("摩羯座")]
        Capricorn = 10,

        /// <summary>
        /// 水瓶座[1月20日-2月18日]
        /// </summary>
        [Description("水瓶座")]
        Aquarius = 11,

        /// <summary>
        /// 双鱼座[2月19日-3月20日]
        /// </summary>
        [Description("双鱼座")]
        Pisces = 12
    }
}
