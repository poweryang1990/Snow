using UOKOFramework.IPDB._17ipdb;
// ReSharper disable InconsistentNaming

namespace UOKOFramework.IPDB
{
    /// <summary>
    /// IP的定位器
    /// </summary>
    public class IPLocator : IIPLocator
    {
        private IPLocator() { }

        /// <summary>
        /// 默认的IIPLocator
        /// </summary>
        public static IIPLocator Default => new IPLocator();

        /// <summary>
        /// 查找IP的位置信息
        /// </summary>
        /// <param name="ipv4">ipv4</param>
        /// <returns></returns>
        public Location Find(IPv4 ipv4)
        {
            var result = _17IPv4DBHelper.Find(ipv4);
            if (result == null || result.Length == 0)
            {
                return null;
            }

            var location = new Location
            {
                IP = ipv4,
            };
            if (result.Length > 1)
            {
                location.Country = result[0];
            }
            if (result.Length > 2)
            {
                location.State = result[1];
            }
            if (result.Length > 3)
            {
                location.City = result[2];
            }
            return location;
        }
    }
}