using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using WeatherBot.Exceptions;

namespace WeatherBot.Interface
{
    internal class BotSettings
    {
        public ITelegramBotClient bot;

        public BotSettings()
        {
            bot = new TelegramBotClient("5575267518:AAHIR0QIuew1lnvOaNAGgltJwLcFTOQEq4k");
        }

        public void Start()
        {            
            Console.WriteLine("Бот запущен " + bot.GetMeAsync().Result.FirstName);            
            var evt = new AutoResetEvent(false);
            var cts = new CancellationTokenSource();
            var cancellationtoken = cts.Token;
            var receiverOptions = new ReceiverOptions()
            {
                AllowedUpdates = { }
            };
            bot.StartReceiving(BotMain.MainMessage, Errors.respErr, receiverOptions, cancellationtoken);
        }
    }
}