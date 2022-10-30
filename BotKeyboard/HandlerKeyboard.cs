using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace WeatherBot.BotKeyboard
{
    public static class HandlerKeyboard
    {
        public static async Task respKeyboard(ITelegramBotClient botClient, Message message)
        {
            if (message.Text == "/button")
            {
                ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup(new[]
                {
                new KeyboardButton[]{"Гагра" , "Гудаута"},
                new KeyboardButton[]{ "Пицунда", "Афон" },
                new KeyboardButton[]{ "Сухум", "Очамчира" },
                new KeyboardButton[]{ "Ткуарчал", "Гал" }
                });

                await botClient.SendTextMessageAsync(message.Chat, text: "...", replyMarkup: keyboard);
                return;
            }
        }
    }
}
