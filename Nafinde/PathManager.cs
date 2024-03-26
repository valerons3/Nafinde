using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Nafinde;

public static class PathManager
{
    public static string LogFile { get; private set; }
    public static string PathBotToken { get; private set; }
    public static string PathUserID { get; private set; }

    static PathManager()
    {
        LogFile = "/root/bots/nafinde/source/log.txt";
        PathBotToken = "/root/bots/nafinde/source/BotToken.json";
        PathUserID = "/root/bots/nafinde/source/UsersID.json";
    }
}
