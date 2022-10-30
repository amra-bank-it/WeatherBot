using System;
using WeatherBot.Interface;

namespace WeatherBot

{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                BotSettings botSett = new BotSettings();
                botSett.Start();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.ToString());
                throw new Exception(err.ToString());
            }
            Console.ReadLine();
        }
    }
}