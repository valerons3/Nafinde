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

namespace Nafinde;

public static class Handler
{
    public async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
    {
        var messageText = update?.Message?.Text;
        if (messageText != null)
        {
            long userChatID = update.Message.Chat.Id;
            switch (messageText)
            {
                case (MessageConst.Start):
                    {
                        Start(botClient, userChatID);
                        return;
                    }

                case (MessageConst.Help):
                    {
                        Help(botClient, userChatID);
                        return;
                    }
            }
        }

        if (update.Type == UpdateType.CallbackQuery)
        { await HandleCallBackQuery(botClient, update.CallbackQuery); }
    }

    public static async Task HandleCallBackQuery(ITelegramBotClient botClient, CallbackQuery callbackQuery)
    {
        long chatID = callbackQuery.Message.Chat.Id;
        switch (callbackQuery.Data)
        {
            case MessageConst.Subscribe:
                if (await Provider.CheckID(chatID))
                {
                    await botClient.SendTextMessageAsync(chatID, MessageConst.AlreadySubscribed);
                    return;
                }
                Provider.WriteID(chatID);
                await botClient.SendTextMessageAsync(chatID, MessageConst.SuccessfullySubscribed);
                return;

            case MessageConst.Unsubscribe:
                if (!await Provider.CheckID(chatID))
                {
                    await botClient.SendTextMessageAsync(chatID, MessageConst.AlreadyUnsubscribed);
                    return;
                }
                Provider.RemoveID(chatID);
                await botClient.SendTextMessageAsync(chatID, MessageConst.SuccessfullyUnsubscribed);
                return;
        }
    }

    private static async Task Start(ITelegramBotClient botClient, long userChatID)
    {
        if (await Provider.CheckID(userChatID))
        {
            await botClient.SendTextMessageAsync(userChatID, MessageConst.AlreadySubscribed);
            return;
        }
        var inlineKeyboardSubscribe = new InlineKeyboardMarkup(new[]
        {
        new []
        {
            InlineKeyboardButton.WithCallbackData(MessageConst.Subscribe, MessageConst.Subscribe)
        }
        });
        await botClient.SendTextMessageAsync(userChatID, MessageConst.Greetings, replyMarkup: inlineKeyboardSubscribe);
    }
    private static async Task Help(ITelegramBotClient botClient, long userChatID)
    {
        if (!await Provider.CheckID(userChatID))
        {
            var inlineKeyboardSubscribe = new InlineKeyboardMarkup(new[]
            {
            new []
            {
                InlineKeyboardButton.WithCallbackData(MessageConst.Subscribe, MessageConst.Subscribe)
            }
            });
            await botClient.SendTextMessageAsync(userChatID, MessageConst.UnsubscribedNow, replyMarkup: inlineKeyboardSubscribe);
            return;
        }
        var inlineKeyboardUnsubscribe = new InlineKeyboardMarkup(new[]
        {
        new []
        {
            InlineKeyboardButton.WithCallbackData(MessageConst.Unsubscribe, MessageConst.Unsubscribe)
        }
        });
        await botClient.SendTextMessageAsync(userChatID, MessageConst.SubscribedNow, replyMarkup: inlineKeyboardUnsubscribe);
    }

    public static Task Error(ITelegramBotClient arg1, Exception exception, CancellationToken arg3)
    {
        DateTime currentTime = DateTime.Now;
        using (StreamWriter stream = new StreamWriter(PathManager.LogFile, true))
            stream.WriteLine($"[{currentTime.ToString("dd-MM-yyyy")}] {exception.Message}");
        return Task.CompletedTask;
    }
}