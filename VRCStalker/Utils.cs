using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace VRCStalker
{
    public static class Utils
    {

        /// <summary>
        /// Caches an image into the local cache folder (THIS IS A WORK IN PROGRESS)
        /// </summary>
        /// <param name="url">URL to download image from</param>
        /// <returns></returns>
        public static string CacheImage(string url)
        {
            WebClient client = new();
            client.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.99 Safari/537.36 OPR/83.0.4254.46");

            Cache.Instance.latestIndex += 1;

            string newPath = $"{Environment.CurrentDirectory}\\Cache\\Images\\{Cache.Instance.latestIndex}.png";

            client.DownloadFile(new Uri(url), newPath);

            return newPath;
        }
    }
}
