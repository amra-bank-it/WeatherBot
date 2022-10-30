using System.Configuration;
using System.Data.SqlClient;
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
            con.Open();

            if (message == null)
            {
                await botClient.SendTextMessageAsync(callbackQuery.From.Id, callbackQuery.Data.ToString());

                bool answer = false;
                answer = SingletonDB.respBit(Convert.ToInt32(callbackQuery.From.Id));

                if (answer == false)
                {
                    string queryInsert = $"INSERT INTO WeatherApsny.dbo.Weather(UserId) VALUES({callbackQuery.From.Id})";
                    SqlCommand sqlCommand = new SqlCommand(queryInsert, con);
                    sqlCommand.ExecuteNonQuery();
                }
            }

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
                            Parse.GetCode(url, ref city, ref temp , ref feels , ref humidity);
                            SQLQuery(message, city, temp, feels, humidity, con);

                            await botClient.SendTextMessageAsync(message.Chat.Id, city + "\nТемпература: " + temp + "°С" + "\n" + feels + "\n" + humidity + "\n", replyMarkup: keyboard);
                            break;
                        }
                    case "Гудаута":
                        {
                            url = "https://prognoz3.ru/abkhazia/gudauta/gudauta";
                            Parse.GetCode(url, ref city, ref temp, ref feels, ref humidity);
                            SQLQuery(message, city, temp, feels, humidity, con);

                            await botClient.SendTextMessageAsync(message.Chat.Id, city + "\nТемпература: " + temp + "°С" + "\n" + feels  + "\n" + humidity + "\n", replyMarkup: keyboard);
                            break;
                        }
                    case "Пицунда":
                        {
                            url = "https://prognoz3.ru/abkhazia/gagra/pitsunda";
                            Parse.GetCode(url, ref city, ref temp, ref feels, ref humidity);
                            SQLQuery(message, city, temp, feels, humidity, con);

                            await botClient.SendTextMessageAsync(message.Chat.Id, city + "\nТемпература: " + temp + "°С" + "\n" + feels  + "\n" + humidity + "\n", replyMarkup: keyboard);
                            break;
                        }
                    case "Афон":
                        {
                            url = "https://prognoz3.ru/abkhazia/gudauta/novyj-afon";
                            Parse.GetCode(url, ref city, ref temp, ref feels, ref humidity);
                            SQLQuery(message, city, temp, feels, humidity, con);

                            await botClient.SendTextMessageAsync(message.Chat.Id, city + "\nТемпература: " + temp + "°С" + "\n" + feels  + "\n" + humidity + "\n", replyMarkup: keyboard);
                            break;
                        }
                    case "Сухум":
                        {
                            url = "https://prognoz3.ru/abkhazia/sukhumi/suhum";
                            Parse.GetCode(url, ref city, ref temp, ref feels, ref humidity);
                            SQLQuery(message, city, temp, feels, humidity, con);

                            await botClient.SendTextMessageAsync(message.Chat.Id, city + "\nТемпература: " + temp + "°С" + "\n" + feels  + "\n" + humidity + "\n", replyMarkup: keyboard);
                            break;
                        }
                    case "Очамчира":
                        {
                            url = "https://prognoz3.ru/abkhazia/ochamchyrsky/ochamchyra";
                            Parse.GetCode(url, ref city, ref temp, ref feels, ref humidity);
                            SQLQuery(message, city, temp, feels, humidity, con);

                            await botClient.SendTextMessageAsync(message.Chat.Id, city + "\nТемпература: " + temp + "°С" + "\n" + feels  + "\n" + humidity + "\n", replyMarkup: keyboard);
                            break;
                        }
                    case "Ткуарчал":
                        {
                            url = "https://prognoz3.ru/abkhazia/tkuarchal/tkuarchal";
                            Parse.GetCode(url, ref city, ref temp, ref feels, ref humidity);
                            SQLQuery(message, city, temp, feels, humidity, con);

                            await botClient.SendTextMessageAsync(message.Chat.Id, city + "\nТемпература: " + temp + "°С" + "\n" + feels  + "\n" + humidity + "\n", replyMarkup: keyboard);
                            break;
                        }
                    case "Гал":
                        {
                            url = "https://prognoz3.ru/abkhazia/gali/galle";
                            Parse.GetCode(url, ref city, ref temp, ref feels, ref humidity);
                            SQLQuery(message, city, temp, feels , humidity,con);

                            await botClient.SendTextMessageAsync(message.Chat.Id, city + "\nТемпература: " + temp + "°С" + "\n" + feels  + "\n" + humidity + "\n",replyMarkup: keyboard);
                            break;
                        }
                }

                con.Close();
            }
            catch (Exception err)
            {
                Console.WriteLine($"Возникла ошибка {err.ToString()}");
            }

        }

        private static void SQLQuery(Message message, string city, string temp, string feels, string humidity, SqlConnection con)
        {
            try
            {
                string queryInsert = $"UPDATE WeatherApsny.dbo.Weather SET City='" + city + "',Temp='" + temp.Replace("°", "") + 
                    "',Feels='"+feels.Replace("°", "") +"',Humidity='"+humidity+"' WHERE UserId='" + message.Chat.Id + "'";
                SqlCommand sqlCommand = new SqlCommand(queryInsert, con);
                sqlCommand.ExecuteNonQuery();

            }
            catch (SqlException err)
            {
                Console.WriteLine("Произошла ошибка при передаче данных в базу. Код ошибки: " + err.ToString());
            }
        }
    }
}