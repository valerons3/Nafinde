using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text.Json;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Nafinde;

public class Program
{
    public static string BotToken;
    public static TelegramBotClient BotClient;
    public static void Main(string[] args)
    {
        using (FileStream fs = new FileStream(PathManager.PathBotToken, FileMode.Open)) {
            BotToken = JsonSerializer.Deserialize<string>(fs);
        }

        BotClient = new TelegramBotClient(BotToken);
        BotClient.StartReceiving(Handler.Update, Handler.Error);
        Console.ReadLine();
    }
}