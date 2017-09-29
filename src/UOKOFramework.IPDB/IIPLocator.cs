// ReSharper disable InconsistentNaming

namespace UOKOFramework.IPDB
{
    /// <summary>
    /// IP的定位器
    /// </summary>
    public interface IIPLocator
    {
        /// <summary>
        /// 查找IP的位置信息
        /// </summary>
        /// <param name="ipv4">ipv4</param>
        /// <returns></returns>
        Location Find(IPv4 ipv4);
    }
}
