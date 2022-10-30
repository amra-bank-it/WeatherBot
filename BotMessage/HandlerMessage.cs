using NLog;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace WeatherBot.BotMessage
{
    internal class HandlerMessage
    {
        public static async Task respMessage(ITelegramBotClient botClient, Message message)
        {
            Logger logger = LogManager.GetCurrentClassLogger();

            if (message.Text.ToLower() == "/start")
            {
                logger.Info("Начали работу");

                ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup(new[]
                {
                new KeyboardButton[]{"Гагра" , "Гудаута"},
                new KeyboardButton[]{ "Пицунда", "Афон" },
                new KeyboardButton[]{ "Сухум", "Очамчира" },
                new KeyboardButton[]{ "Ткуарчал", "Гал" }
                });

                await botClient.SendTextMessageAsync(message.Chat, "Здравствуйте.Вас приветствует метео-бот\nВыберите город." , replyMarkup: keyboard);
                return;
            }
        }
    }
}
