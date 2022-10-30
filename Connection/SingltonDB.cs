﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace WeatherBot.Connection
{
    internal class SingletonDB
    {
        private static SingletonDB instance;
        private SingletonDB()
        { }

        public static SingletonDB getInstance()
        {
            if (instance == null)
                instance = new SingletonDB();
            return instance;
        }
        public bool getQuery(ITelegramBotClient botClient, Message message)
        {
            bool answer = false;
            if (message.Text.ToLower() == "/start")
            {
                int ch = Convert.ToInt32(message.Chat.Id);
                answer = respBit(ch);
            }
            return answer;
        }
        public static bool respBit(int chatId)
        {           
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WeatherApsny"].ConnectionString);
            con.Open();
            bool answer = false;
            string querySelect = $"SELECT UserId FROM WeatherApsny.dbo.Weather WHERE UserId = {chatId}";

            SqlCommand command = new SqlCommand(querySelect, con);
            SqlDataReader reader = command.ExecuteReader();
            string idfromBase = null;
            while (reader.Read())
            {
                idfromBase = reader[0].ToString();
                Console.WriteLine(answer);
            }
            if (idfromBase == null)
            {
                answer = false;
            }
            else
            {
                answer = true;
            }
            reader.Close();
            con.Close();
            return answer;
        }
    }
}