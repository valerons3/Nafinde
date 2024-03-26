using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nafinde;

public static class MessageConst
{
    public const string Start = "/start";
    public const string Help = "/help";
    public const string Subscribe = "Подписаться";
    public const string Unsubscribe = "Отписаться";

    public const string AlreadySubscribed = "Ты уже подписан(а) на рассылку! Для управления подпиской набери команду \"/help\"";

    public const string SuccessfullySubscribed = "Ты успешно подписался(ась) на рассылку! " +
        "Жди каждый день смешную шутку рофл в 10 часов. Для управления подпиской набери команду \"/help\"";

    public const string AlreadyUnsubscribed = "Ты уже отписан(а) от рассылки. Для управления подпиской набери команду \"/help\"";

    public const string SuccessfullyUnsubscribed = "Ты успешно отписался(ась) от рассылки. " +
        "Для управления подпиской набери команду \"/help\"";

    public const string Greetings = "Привет! Это бот который умеет отправлять шутки рофлы, " +
        "чтобы подписаться на рассылку нажми на кнопку \"Подписаться\"";

    public const string UnsubscribedNow = "Сейчас ты не подписан(а) на рассылку, но можешь это сделать тыкнув на кнопку ниже";
    public const string SubscribedNow = "Ты уже подписан(а) на рассылку, можешь отписаться тыкнув на кнопку ниже";
}