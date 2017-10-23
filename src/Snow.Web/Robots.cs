using System.Collections.Generic;
using System.Text;

namespace Snow.Web
{
    /// <summary>
    /// robots.txt
    /// </summary>
    public class Robots
    {
        /// <summary>
        /// user-agent指令
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// disallow指令
        /// </summary>
        public List<string> Disallows { get; } = new List<string>();

        /// <summary>
        /// allow指令
        /// </summary>
        public List<string> Allows { get; } = new List<string>();

        /// <summary>
        /// sitemap指令
        /// </summary>
        public string Sitemap { get; set; }

        /// <summary>
        /// 获取robots的文本内容
        /// </summary>
        /// <returns></returns>
        public string GetTxt()
        {
            var robotsBuilder = new StringBuilder();

            robotsBuilder.AppendLine("user-agent:" + this.UserAgent);

            foreach (var disallow in this.Disallows)
            {
                robotsBuilder.AppendLine("disallow:" + disallow);
            }

            foreach (var allow in this.Allows)
            {
                robotsBuilder.AppendLine("allow:" + allow);
            }

            if (this.Sitemap != null)
            {
                robotsBuilder.AppendLine("sitemap:" + this.Sitemap);
            }

            return robotsBuilder.ToString();
        }

        /// <summary>
        /// ToString，获取robots的文本内容
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.GetTxt();
        }

        /// <summary>
        /// 禁止所有的机器人
        /// </summary>
        public static Robots DisapleAll
        {
            get
            {
                var robots = new Robots
                {
                    UserAgent = "*"
                };
                robots.Disallows.Add("/");
                return robots;
            }
        }

        /// <summary>
        /// 允许所有的机器人
        /// </summary>
        public static Robots AllowAll
        {
            get
            {
                var robots = new Robots
                {
                    UserAgent = "*"
                };
                robots.Allows.Add("/");
                return robots;
            }
        }
    }
}
