using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Nafinde
{
    public static class PathManager
    {
        public static string PathSource { get; private set; }
        public static string PathCatsPhoto { get; private set; }
        public static string PathBase { get; private set; }
        public static string LogFile { get; private set; }
        public static string PathBotToken { get; private set; }
        public static string PathUserID { get; private set; }

        static PathManager()
        {
            string currentPath = Assembly.GetExecutingAssembly().Location;
            PathSource = Path.Combine(currentPath.Substring(0, currentPath.Length - 19), @"source/");
            PathCatsPhoto = Path.Combine(PathSource, @"CatsPhoto/");
            PathBase = Path.Combine(PathSource, @"Base.txt");
            LogFile = Path.Combine(PathSource, @"log.txt");
            PathBotToken = Path.Combine(PathSource, @"BotToken.txt");
            PathUserID = Path.Combine(PathSource, @"UserID.txt");
        }
    }
}
