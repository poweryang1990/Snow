using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using UokoFramework.OCR.Common;
using UOKOFramework.Extensions;

namespace UokoFramework.OCR.Tencent.Utils
{
    /// <summary>
    ///  签名与鉴权
    /// </summary>
    public class TencentSign
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
            if (secretId == "" || secretKey == "")
            {
                return "-1";
            }
            var now = DateTime.Now.GetMillisecondsUnixtime() / 1000;
            var plainText = string.Format("a={0}&b={1}&k={2}&t={3}&e={4}", appId, bucketName, secretId, now, expired);

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
