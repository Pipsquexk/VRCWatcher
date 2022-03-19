using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using Newtonsoft.Json;
using System.Net;

namespace VRCStalker
{
    public class FileManager
    {
        private WebClient webClient = null;

        private static readonly object locker = new();

        private static FileManager instance = null;
        public static FileManager Instance
        {
            get
            {
                lock (locker)
                {
                    instance ??= new();
                    return instance;
                }
            }
        }

        public static string DataDir
        {
            get
            {
                return $"{Environment.CurrentDirectory}\\Data";
            }
        }

        public static string CacheDir
        {
            get
            {
                return $"{DataDir}\\Cache";
            }
        }

        public static string ImageCacheDir
        {
            get
            {
                return $"{DataDir}\\Cache\\Images";
            }
        }

        public static string LogsDir
        {
            get
            {
                return $"{DataDir}\\Logs";
            }
        }

        private int latestCacheIndex = 0;

        public Config config = null;

        private FileManager() => Init();

        private void Init()
        {
            if (!Directory.Exists(DataDir)) Directory.CreateDirectory(DataDir);
            InitCache();
            InitConfig();
            webClient = new();
        }

        private void InitCache()
        {
            Logger.Instance.Log("Initializing Cache...", ConsoleColor.Blue);
            if (!Directory.Exists(CacheDir)) Directory.CreateDirectory(CacheDir);
            if (!Directory.Exists(ImageCacheDir)) Directory.CreateDirectory(ImageCacheDir);
            Logger.Instance.Log("Initialized Cache!", ConsoleColor.Green);
        }

        private void InitConfig()
        {
            Logger.Instance.Log("Initializing Config...", ConsoleColor.Blue);
            if (!File.Exists($"{DataDir}\\Config.json"))
            {
                config = new(false, false);
                string configJson = JsonConvert.SerializeObject(config);
                File.WriteAllText($"{DataDir}\\Config.json", configJson);
            }
            else
            {
                string jsonStr = File.ReadAllText($"{DataDir}\\Config.json");
                config = JsonConvert.DeserializeObject<Config>(jsonStr);
            }
            Logger.Instance.Log("Initialized Config!", ConsoleColor.Green);
        }

        /// <summary>
        /// Caches an image into the local cache folder (THIS IS A WORK IN PROGRESS)
        /// </summary>
        /// <param name="url">URL to download image from</param>
        /// <returns></returns>
        public string CacheImage(string url)
        {
            latestCacheIndex += 1;

            string newPath = $"{CacheDir}\\Images\\{latestCacheIndex}.png";
            webClient.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.99 Safari/537.36 OPR/83.0.4254.46");
            webClient.DownloadFile(new Uri(url), newPath);

            return newPath;
        }

        /// <summary>
        /// Saves current application configuration to the Config.json file
        /// </summary>
        public void SaveConfig()
        {
            string confJson = JsonConvert.SerializeObject(config);

            File.WriteAllText("Data/Config.json", confJson);
        }
    }
}
