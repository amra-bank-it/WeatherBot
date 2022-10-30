using Telegram.Bot;

namespace WeatherBot.Exceptions
{
    public class Errors
    {
        public static async Task respErr(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine("Возникла ошибка!" + exception.Message);
        }

    }
}
