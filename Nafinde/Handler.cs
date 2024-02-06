using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using System.Reflection.PortableExecutable;
using System.IO;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;

namespace Nafinde
{
    public static class Handler
    {
        public async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            if (update.Message != null)
            {
                var message = update.Message;
                if (message.Text != null)
                {
                    if (message.Text == "/start")
                    {
                        using (StreamReader reader = new StreamReader(PathManager.PathUserID))
                        {
                            bool inUserIDFile = CheckIDinFile(reader, message.Chat.Id);
                            if (inUserIDFile) 
                            {
                                await botClient.SendTextMessageAsync(message.Chat.Id, "Ты уже подписан(а) на рассылку! Для управления подпиской набери команду \"/help\""); 
                            }
                            else
                            {
                                var inlineKeyboard = new InlineKeyboardMarkup(new[]
                                {
                                    new []
                                    {
                                        InlineKeyboardButton.WithCallbackData("Подписаться", "Подписаться")
                                    }
                                });
                                await botClient.SendTextMessageAsync(message.Chat.Id, "Привет! Это бот который умеет отправлять шутки рофлы, чтобы подписаться на рассылку нажми на кнопку \"Подписаться\"", replyMarkup: inlineKeyboard);
                            }
                        }
                    }
                    if (message.Text == "/help")
                    {
                        bool IDInFile = false;
                        using (StreamReader reader = new StreamReader(PathManager.PathUserID))
                            IDInFile = CheckIDinFile(reader, message.Chat.Id);
                        if (!IDInFile)
                        {
                            var inlineKeyboard = new InlineKeyboardMarkup(new[]
                            {
                                new []
                                    {
                                        InlineKeyboardButton.WithCallbackData("Подписаться", "Подписаться")
                                    }
                            });
                            await botClient.SendTextMessageAsync(message.Chat.Id, "Сейчас ты не подписан(а) на рассылку, но можешь это сделать тыкнув на кнопку ниже", replyMarkup: inlineKeyboard);
                        }
                        else
                        {
                            var inlineKeyboard = new InlineKeyboardMarkup(new[]
                            {
                                new []
                                {
                                    InlineKeyboardButton.WithCallbackData("Отписаться", "Отписаться")
                                }
                            });
                            await botClient.SendTextMessageAsync(message.Chat.Id, "Ты уже подписан(а) на рассылку, можешь отписаться тыкнув на кнопку ниже", replyMarkup: inlineKeyboard);
                        }
                    }
                }
            }
            if (update.Type == UpdateType.CallbackQuery)
            { await HandleCallBackQuery(botClient, update.CallbackQuery); }
        }

        public static async Task HandleCallBackQuery(ITelegramBotClient botClient, CallbackQuery callbackQuery)
        {
            if (callbackQuery.Data == "Подписаться")
            {
                using (StreamReader reader = new StreamReader(PathManager.PathUserID))
                {
                    bool inUserIDFile = CheckIDinFile(reader, callbackQuery.Message.Chat.Id);
                    if (inUserIDFile) 
                    {
                        await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Ты уже подписан(а) на рассылку! Для управления подпиской набери команду \"/help\""); 
                    }
                    else
                    {
                        using (StreamWriter stream = new StreamWriter(PathManager.PathUserID, true))
                            stream.WriteLine(callbackQuery.Message.Chat.Id);
                        await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Ты успешно подписался(ась) на рассылку! Жди каждый день смешную шутку рофл в 10 часов. Для управления подпиской набери команду \"/help\"");
                    }
                }
            }
            if (callbackQuery.Data == "Отписаться")
            {
                bool inUserIDFile = false;
                using (StreamReader reader = new StreamReader(PathManager.PathUserID))
                    inUserIDFile = CheckIDinFile(reader, callbackQuery.Message.Chat.Id);

                if (!inUserIDFile) 
                {
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Ты уже отписан(а) от рассылки. Для управления подпиской набери команду \"/help\""); 
                }
                else
                {
                    List<string> userID = new List<string>();
                    string? ID;
                    using (StreamReader reader = new StreamReader(PathManager.PathUserID))
                    {
                        while ((ID = reader.ReadLine()) != null)
                        {
                            if (ID != callbackQuery.Message.Chat.Id.ToString())
                            { userID.Add(ID); }
                        }
                    }
                    using (StreamWriter write = new StreamWriter(PathManager.PathUserID))
                        write.WriteLine(string.Join(Environment.NewLine, userID));
                    await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Ты успешно отписался(ась) от рассылки. Для управления подпиской набери команду \"/help\"");
                }
            }
        }

        public static bool CheckIDinFile(StreamReader reader, long chatID)
        {
            string ID;
            bool inUserIDFile = false;
            while ((ID = reader.ReadLine()) != null)
            {
                if (ID == chatID.ToString())
                { inUserIDFile = true; }
            }
            return inUserIDFile;
        }

        public static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        { throw new NotImplementedException(); }
    }
}

