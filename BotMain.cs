using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using WeatherBot.BotKeyboard;
using WeatherBot.BotMessage;
using WeatherBot.Connection;

namespace WeatherBot
{
    public class BotMain
    {
        public static async Task MainMessage(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            try
            {
                if (update.Type == UpdateType.Message)
                {
                    var msg = update.Message;
                    string userTxt = msg.Text == null ? "" : msg.Text;
                    MessageType type = userTxt == "/start" ? MessageType.Text : msg.Type;
                    var callbackQuery = update.CallbackQuery;

                    switch (msg.Type)
                    {
                        case MessageType.Text:
                            {
                                await HandlerMessage.respMessage(botClient, msg);
                                break;
                            }

                    }

                    await HandlerKeyboard.respKeyboard(botClient, msg);
                    await HandlerButton.respButton(botClient, msg, callbackQuery);
                    SingletonDB.getInstance().getQuery(botClient, msg);
                }
                if (update.Type == UpdateType.CallbackQuery)
                {
                    var msg = update.Message;
                    var callbackQuery = update.CallbackQuery;
                    await HandlerButton.respButton(botClient, msg, callbackQuery);
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.ToString());
            }
        }
    }
}