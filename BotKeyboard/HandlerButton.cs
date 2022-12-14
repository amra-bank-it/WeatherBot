using NLog;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using WeatherBot.Adapter;
using WeatherBot.Connection;

namespace WeatherBot.BotKeyboard
{
    public static class HandlerButton
    {
        public static async Task respButton(ITelegramBotClient botClient, Message message, CallbackQuery callbackQuery)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WeatherApsny"].ConnectionString);

            Logger logger = LogManager.GetCurrentClassLogger();

            ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton[]{"Гагра" , "Гудаута"},
                new KeyboardButton[]{"Пицунда" , "Афон" },
                new KeyboardButton[]{"Сухум" , "Очамчира" },
                new KeyboardButton[]{"Ткуарчал" , "Гал" }
            });

            string url = "";
            string city = "";
            string temp = "";
            string feels = "";
            string humidity = "";

            try
            {

                switch (message.Text)
                {

                    case "Гагра":
                        {
                            url = "https://prognoz3.ru/abkhazia/gagra/ghagra";
                            Parse.GetCode(url, ref city, ref temp, ref feels, ref humidity);
                            SQLQuery(message, city, temp, feels, humidity, con);

                            await botClient.SendTextMessageAsync(message.Chat.Id, city + "\nТемпература: " + temp + "°С" + "\n" + feels + "\n" + humidity + "\n", replyMarkup: keyboard);
                            break;
                        }
                    case "Гудаута":
                        {
                            url = "https://prognoz3.ru/abkhazia/gudauta/gudauta";
                            Parse.GetCode(url, ref city, ref temp, ref feels, ref humidity);
                            SQLQuery(message, city, temp, feels, humidity, con);

                            await botClient.SendTextMessageAsync(message.Chat.Id, city + "\nТемпература: " + temp + "°С" + "\n" + feels + "\n" + humidity + "\n", replyMarkup: keyboard);
                            break;
                        }
                    case "Пицунда":
                        {
                            url = "https://prognoz3.ru/abkhazia/gagra/pitsunda";
                            Parse.GetCode(url, ref city, ref temp, ref feels, ref humidity);
                            SQLQuery(message, city, temp, feels, humidity, con);

                            await botClient.SendTextMessageAsync(message.Chat.Id, city + "\nТемпература: " + temp + "°С" + "\n" + feels + "\n" + humidity + "\n", replyMarkup: keyboard);
                            break;
                        }
                    case "Афон":
                        {
                            url = "https://prognoz3.ru/abkhazia/gudauta/novyj-afon";
                            Parse.GetCode(url, ref city, ref temp, ref feels, ref humidity);
                            SQLQuery(message, city, temp, feels, humidity, con);

                            await botClient.SendTextMessageAsync(message.Chat.Id, city + "\nТемпература: " + temp + "°С" + "\n" + feels + "\n" + humidity + "\n", replyMarkup: keyboard);
                            break;
                        }
                    case "Сухум":
                        {
                            url = "https://prognoz3.ru/abkhazia/sukhumi/suhum";
                            Parse.GetCode(url, ref city, ref temp, ref feels, ref humidity);
                            SQLQuery(message, city, temp, feels, humidity, con);

                            await botClient.SendTextMessageAsync(message.Chat.Id, city + "\nТемпература: " + temp + "°С" + "\n" + feels + "\n" + humidity + "\n", replyMarkup: keyboard);
                            break;
                        }
                    case "Очамчира":
                        {
                            url = "https://prognoz3.ru/abkhazia/ochamchyrsky/ochamchyra";
                            Parse.GetCode(url, ref city, ref temp, ref feels, ref humidity);
                            SQLQuery(message, city, temp, feels, humidity, con);

                            await botClient.SendTextMessageAsync(message.Chat.Id, city + "\nТемпература: " + temp + "°С" + "\n" + feels + "\n" + humidity + "\n", replyMarkup: keyboard);
                            break;
                        }
                    case "Ткуарчал":
                        {
                            url = "https://prognoz3.ru/abkhazia/tkuarchal/tkuarchal";
                            Parse.GetCode(url, ref city, ref temp, ref feels, ref humidity);
                            SQLQuery(message, city, temp, feels, humidity, con);

                            await botClient.SendTextMessageAsync(message.Chat.Id, city + "\nТемпература: " + temp + "°С" + "\n" + feels + "\n" + humidity + "\n", replyMarkup: keyboard);
                            break;
                        }
                    case "Гал":
                        {
                            url = "https://prognoz3.ru/abkhazia/gali/galle";
                            Parse.GetCode(url, ref city, ref temp, ref feels, ref humidity);
                            SQLQuery(message, city, temp, feels, humidity, con);

                            await botClient.SendTextMessageAsync(message.Chat.Id, city + "\nТемпература: " + temp + "°С" + "\n" + feels + "\n" + humidity + "\n", replyMarkup: keyboard);
                            break;
                        }

                }

                con.Close();
            }
            catch (Exception err)
            {
                logger.Error(err);
                Console.WriteLine($"Возникла ошибка {err.ToString()}");
            }

        }

        private static void SQLQuery(Message message, string city, string temp, string feels, string humidity, SqlConnection con)
        {
            Logger logger = LogManager.GetCurrentClassLogger();
            try
            {
               
                con.Open();
                string queryUser = $"INSERT INTO WeatherApsny.dbo.Weather (UserId) VALUES({message.Chat.Id})";
                SqlCommand command = new SqlCommand(queryUser, con);
                string queryUpdate = $"UPDATE WeatherApsny.dbo.Weather SET City='" + city + "',Temp='" + temp.Replace("°", "") +
                    "',Feels='" + feels.Replace("°C", "") + "',Humidity='" + humidity + "' WHERE UserId='" + message.Chat.Id + "'";
                SqlCommand sqlCommand = new SqlCommand(queryUpdate, con);
                
                sqlCommand.ExecuteNonQuery();
                logger.Info("Сделали запись в базу");

            }
            catch (SqlException err)
            {
                logger.Error(err);
                Console.WriteLine("Произошла ошибка при передаче данных в базу. Код ошибки: " + err.ToString());
            }
        }
    }
}