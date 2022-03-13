using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VRCStalker
{
    public class Cache
    {
        public static Cache Instance;

        public int latestIndex = 0;

        public static void Init()
        {
            Instance = new Cache();

            if (!Directory.Exists("Cache"))
            {
                Directory.CreateDirectory("Cache");
            }

            InitImageCache();
        }

        public static void InitImageCache()
        {
            if (!Directory.Exists("Cache/Images"))
            {
                Directory.CreateDirectory("Cache/Images");
            }
        }
    }
}
