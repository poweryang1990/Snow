using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace UokoFramework.OCR.Tencent.Common
{
    /// <summary>
    ///  签名与鉴权
    /// </summary>
    public class Sign
    {
        /// <summary>
        /// 多次有效签名
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="secretId"></param>
        /// <param name="secretKey"></param>
        /// <param name="expired"></param>
        /// <param name="bucketName"></param>
        /// <returns></returns>
        public static string DetectionSignature(int appId, string secretId, string secretKey, long expired, string bucketName)
        {
            var rand = new Random();
            var rdm = rand.Next(Int32.MaxValue);
            var now = DateTime.Now.ToUnixTime() / 1000;
            var plainText = string.Format("a={0}&b={1}&k={2}&e={3}&t={4}&r={5}&u={6}", appId, bucketName, secretId, expired, now, rdm, "0");

            using (HMACSHA1 mac = new HMACSHA1(Encoding.UTF8.GetBytes(secretKey)))
            {
                var hash = mac.ComputeHash(Encoding.UTF8.GetBytes(plainText));
                var pText = Encoding.UTF8.GetBytes(plainText);
                var all = new byte[hash.Length + pText.Length];
                Array.Copy(hash, 0, all, 0, hash.Length);
                Array.Copy(pText, 0, all, hash.Length, pText.Length);
                return Convert.ToBase64String(all);
            }
        }
    }
}
