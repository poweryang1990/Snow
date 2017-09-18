using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;


namespace UokoFramework.Web.Utils
{
    /// <summary>
    /// 图片helper
    /// </summary>
    public class ImageHelper
    {
        /// <summary>
        /// image url to base64
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public String ConvertImageURLToBase64(String url)
        {
            StringBuilder _sb = new StringBuilder();
            Byte[] _byte = this.GetImage(url);
            _sb.Append(Convert.ToBase64String(_byte, 0, _byte.Length));
            return _sb.ToString();
        }

        private byte[] GetImage(string url)
        {
            Stream stream = null;
            byte[] buf;

            try
            {
                WebProxy myProxy = new WebProxy();
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

                HttpWebResponse response = (HttpWebResponse)req.GetResponse();
                stream = response.GetResponseStream();

                using (BinaryReader br = new BinaryReader(stream))
                {
                    int len = (int)(response.ContentLength);
                    buf = br.ReadBytes(len);
                    br.Close();
                }

                stream.Close();
                response.Close();
            }
            catch (Exception exp)
            {
                buf = null;
            }

            return (buf);
        }
    }
}
