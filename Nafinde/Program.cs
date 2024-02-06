using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Nafinde;

public class Program
{
    public static string BotToken;
    public static TelegramBotClient BotClient;
    public static void Main(string[] args)
    {
        using (StreamReader reader = new StreamReader(PathManager.PathBotToken))
            BotToken = reader.ReadToEnd();
        BotClient = new TelegramBotClient(BotToken);
        BotClient.StartReceiving(Handler.Update, Handler.Error);
        Console.ReadLine();
    }
}