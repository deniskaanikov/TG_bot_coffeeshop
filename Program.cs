using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Extensions;
using Microsoft.Bot.Builder;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static ITelegramBotClient botClient;
        
        static void Main(string[] args)
        {
            botClient = new TelegramBotClient("6459137619:AAEExP-mctMCdqoSyo84OZwK9h9kWGBjc0Y");
            botClient.StartReceiving(Update, Error);

            Console.WriteLine("Coffee Bot is running...");
            Console.ReadLine();
        }

        private static Task Error(ITelegramBotClient botClient, Exception exception, CancellationToken token)
        {
            return Task.CompletedTask;
        }

        async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
        
        
            var message = update.Message;

            if (message.Text != null)
            {
                Console.WriteLine($"Received a message from {message.Chat.FirstName}: {message.Text}");

                string[] Shops =
            {
               "Double B Coffee & Tea",
               "Шоколадница",
               "Stars coffee",
               "Surf Coffee",
               "Кофеин",
               "Traveler's Coffee",
               "Моремэй",
               "Кофемания",
               "Coffee Bean",
               "Friends&Family",
               "Пушкинъ",
               "Лукоморье",
               "Fine",
               "Siyai Coffee",
               "One Love Coffee",
               "Cofix"
            };

                string[] Drinks =
                {
                "Американо",
                "Латте",
                "Капучино",
                "Раф",
                "Флэт уайт",
                "Эспрессо",
                "Мокко",
                "Макиато",
                "Глясе",
                "Матча",
                "Мокачино"
            };

                int[,] Prices = new int[16, 11]
                {
                {-1, 350, 350, 400, 300, -1, -1, -1, -1, -1, -1},
                {150, 270, 170, 250, 200, -1, -1, -1, -1, -1, -1},
                {-1, 300, 320, 360, -1, -1, 350, 355, -1, -1, -1},
                {220, 300, 300, 400, -1, -1, -1, -1, -1, -1, -1},
                {60, 95, 95, 170, 150, 85, -1, -1, -1, -1, -1},
                {-1, 350, 250, 400, 400, 300, -1, -1, -1, -1, -1},
                {120, 180, 150, -1, -1, 120, -1, -1, -1, -1, -1},
                {450, 600, 410, 590,-1,-1, 400,-1,-1,-1,-1},
                {210,320 ,320 ,-1 ,280 ,-1 ,160 ,400 ,-1 ,-1 ,-1},
                {120,210 ,210 ,250 ,-1 ,120 ,-1 ,-1 ,-1 ,-1 ,-1},
                {350,490 ,400 ,-1 ,500 ,-1 ,-1 ,-1 ,-1 ,-1 ,-1},
                {200,350 ,320 ,-1 ,-1 ,150 ,-1 ,-1 ,360 ,-1 ,-1},
                {200 ,330 ,300 ,-1,280 ,200, -1 ,230 ,-1 ,-1, 330},
                {-1,370,350,-1,-1,-1,-1,-1,400,315,-1},
                {-1,150,140,180,-1,-70,-1,-1,-1,-1,-1},
                {150,210,150,150,200,-1,-1,-1,-1,-1,-1}
                };

                var reply = "";

                string selectedDrink = "";

                string selectedPriceRange = "";

                int selectedMinPrice = 0;
                int selectedMaxPrice = 0;
 

                message = update.Message;

                if (message.Text == "/start")
                {
                    reply = "Привет! Я бот, который поможет тебе выбрать кофейню. Выбери на клавиатуре свой любимый напиток:";
                    await botClient.SendTextMessageAsync(message.Chat.Id, reply, replyMarkup: GetCoffeeMenu());
                }

                else if (message.Text == "Раф" ^ message.Text == "Флэт уайт" ^ message.Text == "Латте" ^ message.Text == "Эспрессо" ^ message.Text == "Американо" ^ message.Text == "Капучино" ^ message.Text == "Глясе" ^ message.Text == "Макиато" ^ message.Text == "Мокачино" ^ message.Text == "Матча" ^ message.Text == "Мокко")
                {
                    selectedDrink = message.Text;
                    Console.WriteLine($"Выбранный напиток: {selectedDrink}");
                    
                    StreamWriter writer = new StreamWriter("C:\\Users\\daani\\Downloads\\selecteddrink.txt");
                    writer.Write(selectedDrink);
                    writer.Close();
                    reply = "Выбери на клавиатуре диапазон цены:";
                    await botClient.SendTextMessageAsync(update.Message.Chat.Id, reply, replyMarkup: GetPriceRangeMenu());
                }

                else if (message.Text == "<150" ^ message.Text == "160-250" ^ message.Text == "260-350" ^ message.Text == ">350")
                {
                    selectedPriceRange = message.Text;
                    Console.WriteLine($"Выбранный диапазон цен: {selectedPriceRange}");
                    StreamReader reader = new StreamReader("C:\\Users\\daani\\Downloads\\selecteddrink.txt");
                    selectedDrink = reader.ReadToEnd();
                    Console.WriteLine(selectedDrink);
                    
                    reader.Close();
                    if (message.Text == "<150")
                    {
                        selectedMinPrice = 0;
                        selectedMaxPrice = 150;
                        

                        string recommendedCoffeeShop = FindRecommendedCoffeeShop(selectedDrink, Shops, Drinks, Prices, selectedMinPrice, selectedMaxPrice);

                        if (recommendedCoffeeShop == "")
                        {
                            reply = "Мне жаль, но я не могу порекомендовать кофейню на основе выбранных параметров.";
                        }

                        else
                        {
                            reply = $"Я рекомендую тебе посетить кофейню \"{recommendedCoffeeShop}\".";
                        }
                    };
                    if (message.Text == "160-250")
                    {
                        selectedMinPrice = 160;
                        selectedMaxPrice = 250;

                        
                        string recommendedCoffeeShop = FindRecommendedCoffeeShop(selectedDrink, Shops, Drinks, Prices, selectedMinPrice, selectedMaxPrice);

                        if (recommendedCoffeeShop == "")
                        {
                            reply = "Мне жаль, но я не могу порекомендовать кофейню на основе выбранных параметров.";
                        }

                        else
                        {
                            reply = $"Я рекомендую тебе посетить кофейню \"{recommendedCoffeeShop}\".";
                        }
                    };
                    if (message.Text == "260-350")
                    {
                        selectedMinPrice = 260;
                        selectedMaxPrice = 350;
                        string recommendedCoffeeShop = FindRecommendedCoffeeShop(selectedDrink, Shops, Drinks, Prices, selectedMinPrice, selectedMaxPrice);

                        if (recommendedCoffeeShop == "")
                        {
                            reply = "Мне жаль, но я не могу порекомендовать кофейню на основе выбранных параметров.";
                        }

                        else
                        {
                            reply = $"Я рекомендую тебе посетить кофейню \"{recommendedCoffeeShop}\".";
                        }
                    };
                    if (message.Text == ">350")
                    {
                        selectedMinPrice = 350;
                        selectedMaxPrice = 1000;
                        
                        string recommendedCoffeeShop = FindRecommendedCoffeeShop(selectedDrink, Shops, Drinks, Prices, selectedMinPrice, selectedMaxPrice);

                        if (recommendedCoffeeShop == "")
                        {
                            reply = "Мне жаль, но я не могу порекомендовать кофейню на основе выбранных параметров.";
                        }

                        else
                        {
                            reply = $"Я рекомендую тебе посетить кофейню \"{recommendedCoffeeShop}\".";
                        }
                    };

                    

                    
                            
                    
                    
                    await botClient.SendTextMessageAsync(update.Message.Chat.Id, reply);

                } 
                else
                {
                    reply = "Я не могу понять твоё сообщение. Попробуй еще раз.";
                    await botClient.SendTextMessageAsync(update.Message.Chat.Id, reply, replyMarkup: GetCoffeeMenu());
                }

            }
        }



        static ReplyKeyboardMarkup GetCoffeeMenu()
        {
            var buttons = new List<KeyboardButton[]>()
            {
                new[]
                {
                    new KeyboardButton("Раф"),
                    new KeyboardButton("Флэт уайт"),
                    new KeyboardButton("Латте"),
                    new KeyboardButton("Эспрессо"),
                    new KeyboardButton("Американо"),
                },
                new[]
                {
                    new KeyboardButton("Капучино"),
                    new KeyboardButton("Глясе"),
                    new KeyboardButton("Макиато"),
                    new KeyboardButton("Мокачино"),
                    new KeyboardButton("Матча"),
                },
                new[]
                {
                    new KeyboardButton("Мокко"),
                }
            };

            return new ReplyKeyboardMarkup(buttons);
        }

        static ReplyKeyboardMarkup GetPriceRangeMenu()
        {
            var buttons = new List<KeyboardButton[]>()
            {
                new[]
                {
                    new KeyboardButton("<150"),
                    new KeyboardButton("160-250"),
                    new KeyboardButton("260-350"),
                    new KeyboardButton(">350"),
                }
            };

            return new ReplyKeyboardMarkup(buttons);
        }

      


        static string FindRecommendedCoffeeShop(string selectedDrink, string[] Shops, string[] Drinks, int[,] Prices, int MinPrice, int MaxPrice)
        {
            int[] column = new int[16];
            List<int> possiblePrices = new List<int>();
            List<int> IDpossiblePrices = new List<int>();

            
            Console.WriteLine("функция выбора кофе активирована");

            switch (selectedDrink)
            {
                case "Раф":

                    for (int i = 0; i < 11; i++)
                    {
                        column[i] = Prices[i, 3];
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        if (column[i] <= MaxPrice && column[i] >= MinPrice)
                        {
                            possiblePrices.Add(column[i]);
                            IDpossiblePrices.Add(i);
                        }

                    }
                    if (possiblePrices.Count > 0)
                    {

                        int minValue = possiblePrices[0];
                        int IDminValue = IDpossiblePrices[0];

                        for (int i = 1; i < possiblePrices.Count; i++)
                        {
                            if (possiblePrices[i] < minValue)
                            {
                                minValue = possiblePrices[i];
                                IDminValue = IDpossiblePrices[i];
                            }

                        }

                        Console.WriteLine(Shops[IDminValue]);
                        return Shops[IDminValue];
                    }
                    else
                    {

                        return "";
                    }
                    
                case "Флэт_уайт":
                    for (int i = 0; i < 11; i++)
                    {
                        column[i] = Prices[i, 4];
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        if (column[i] <= MaxPrice && column[i] >= MinPrice)
                        {
                            possiblePrices.Add(column[i]);
                            IDpossiblePrices.Add(i);
                        }

                    }
                    if (possiblePrices.Count > 0)
                    {

                        int minValue1 = possiblePrices[0];
                        int IDminValue1 = IDpossiblePrices[0];

                        for (int i = 1; i < possiblePrices.Count; i++)
                        {
                            if (possiblePrices[i] < minValue1)
                            {
                                minValue1 = possiblePrices[i];
                                IDminValue1 = IDpossiblePrices[i];
                            }

                        }

                        Console.WriteLine(Shops[IDminValue1]);
                        return Shops[IDminValue1];
                    }
                    else
                    {

                        return "";
                    }
                case "Латте":
                    for (int i = 0; i < 11; i++)
                    {
                        column[i] = Prices[i, 1];
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        if (column[i] <= MaxPrice && column[i] >= MinPrice)
                        {
                            possiblePrices.Add(column[i]);
                            IDpossiblePrices.Add(i);
                        }

                    }
                    if (possiblePrices.Count > 0)
                    {

                        int minValue2 = possiblePrices[0];
                        int IDminValue2 = IDpossiblePrices[0];

                        for (int i = 1; i < possiblePrices.Count; i++)
                        {
                            if (possiblePrices[i] < minValue2)
                            {
                                minValue2 = possiblePrices[i];
                                IDminValue2 = IDpossiblePrices[i];
                            }

                        }

                        Console.WriteLine(Shops[IDminValue2]);
                        return Shops[IDminValue2];
                    }
                    else
                    {

                        return "";
                    }
                case "Эспрессо":
                    for (int i = 0; i < 11; i++)
                    {
                        column[i] = Prices[i, 5];
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        if (column[i] <= MaxPrice && column[i] >= MinPrice)
                        {
                            possiblePrices.Add(column[i]);
                            IDpossiblePrices.Add(i);
                        }

                    }
                    if (possiblePrices.Count > 0)
                    {

                        int minValue3 = possiblePrices[0];
                        int IDminValue3 = IDpossiblePrices[0];

                        for (int i = 1; i < possiblePrices.Count; i++)
                        {
                            if (possiblePrices[i] < minValue3)
                            {
                                minValue3 = possiblePrices[i];
                                IDminValue3 = IDpossiblePrices[i];
                            }

                        }

                        Console.WriteLine(Shops[IDminValue3]);
                        return Shops[IDminValue3];
                    }
                    else
                    {

                        return "";
                    }

                case "Американо":
                    for (int i = 0; i < 11; i++)
                    {
                        column[i] = Prices[i, 0];
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        if (column[i] <= MaxPrice && column[i] >= MinPrice)
                        {
                            possiblePrices.Add(column[i]);
                            IDpossiblePrices.Add(i);
                        }

                    }
                    if (possiblePrices.Count > 0)
                    {

                        int minValue4 = possiblePrices[0];
                        int IDminValue4 = IDpossiblePrices[0];

                        for (int i = 1; i < possiblePrices.Count; i++)
                        {
                            if (possiblePrices[i] < minValue4)
                            {
                                minValue4 = possiblePrices[i];
                                IDminValue4 = IDpossiblePrices[i];
                            }

                        }

                        Console.WriteLine(Shops[IDminValue4]);
                        return Shops[IDminValue4];
                    }
                    else
                    {

                        return "";
                    }
                case "Капучино":
                    for (int i = 0; i < 11; i++)
                    {
                        column[i] = Prices[i, 2];
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        if (column[i] <= MaxPrice && column[i] >= MinPrice)
                        {
                            possiblePrices.Add(column[i]);
                            IDpossiblePrices.Add(i);
                        }

                    }
                    if (possiblePrices.Count > 0)
                    {

                        int minValue5 = possiblePrices[0];
                        int IDminValue5 = IDpossiblePrices[0];

                        for (int i = 1; i < possiblePrices.Count; i++)
                        {
                            if (possiblePrices[i] < minValue5)
                            {
                                minValue5 = possiblePrices[i];
                                IDminValue5 = IDpossiblePrices[i];
                            }

                        }

                        Console.WriteLine(Shops[IDminValue5]);
                        return Shops[IDminValue5];
                    }
                    else
                    {

                        return "";
                    }
                case "Глясе":
                    for (int i = 0; i < 11; i++)
                    {
                        column[i] = Prices[i, 8];
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        if (column[i] <= MaxPrice && column[i] >= MinPrice)
                        {
                            possiblePrices.Add(column[i]);
                            IDpossiblePrices.Add(i);
                        }

                    }
                    if (possiblePrices.Count > 0)
                    {

                        int minValue6 = possiblePrices[0];
                        int IDminValue6 = IDpossiblePrices[0];

                        for (int i = 1; i < possiblePrices.Count; i++)
                        {
                            if (possiblePrices[i] < minValue6)
                            {
                                minValue6 = possiblePrices[i];
                                IDminValue6 = IDpossiblePrices[i];
                            }

                        }

                        Console.WriteLine(Shops[IDminValue6]);
                        return Shops[IDminValue6];
                    }
                    else
                    {

                        return "";
                    }
                case "Макиато":

                    
                    for (int i = 0; i < 11; i++)
                    {
                        column[i] = Prices[i, 7];
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        if (column[i] <= MaxPrice && column[i] >= MinPrice)
                        {
                            possiblePrices.Add(column[i]);
                            IDpossiblePrices.Add(i);
                        }

                    }
                        if (possiblePrices.Count > 0)
                            {
                            
                            int minValue7 = possiblePrices[0];
                            int IDminValue7 = IDpossiblePrices[0];
                        
                            for (int i = 1; i < possiblePrices.Count; i++)
                            {
                                if (possiblePrices[i] < minValue7)
                                {
                                    minValue7 = possiblePrices[i];
                                    IDminValue7 = IDpossiblePrices[i];
                                }
                                
                            }
                            
                            Console.WriteLine(Shops[IDminValue7]);
                            return Shops[IDminValue7];
                        }
                        else
                        {
                        
                            return "";
                        }
                        
                case "Мокачино":
                    for (int i = 0; i < 11; i++)
                    {
                        column[i] = Prices[i, 10];
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        if (column[i] <= MaxPrice && column[i] >= MinPrice)
                        {
                            possiblePrices.Add(column[i]);
                            IDpossiblePrices.Add(i);
                        }

                    }
                    if (possiblePrices.Count > 0)
                    {

                        int minValue8 = possiblePrices[0];
                        int IDminValue8 = IDpossiblePrices[0];

                        for (int i = 1; i < possiblePrices.Count; i++)
                        {
                            if (possiblePrices[i] < minValue8)
                            {
                                minValue8 = possiblePrices[i];
                                IDminValue8 = IDpossiblePrices[i];
                            }

                        }

                        Console.WriteLine(Shops[IDminValue8]);
                        return Shops[IDminValue8];
                    }
                    else
                    {

                        return "";
                    }
                case "Матча":
                    for (int i = 0; i < 11; i++)
                    {
                        column[i] = Prices[i, 9];
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        if (column[i] <= MaxPrice && column[i] >= MinPrice)
                        {
                            possiblePrices.Add(column[i]);
                            IDpossiblePrices.Add(i);
                        }

                    }
                    if (possiblePrices.Count > 0)
                    {

                        int minValue9 = possiblePrices[0];
                        int IDminValue9 = IDpossiblePrices[0];

                        for (int i = 1; i < possiblePrices.Count; i++)
                        {
                            if (possiblePrices[i] < minValue9)
                            {
                                minValue9 = possiblePrices[i];
                                IDminValue9 = IDpossiblePrices[i];
                            }

                        }

                        Console.WriteLine(Shops[IDminValue9]);
                        return Shops[IDminValue9];
                    }
                    else
                    {

                        return "";
                    }
                case "Мокко":
                    for (int i = 0; i < 11; i++)
                    {
                        column[i] = Prices[i, 6];
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        if (column[i] <= MaxPrice && column[i] >= MinPrice)
                        {
                            possiblePrices.Add(column[i]);
                            IDpossiblePrices.Add(i);
                        }

                    }
                    if (possiblePrices.Count > 0)
                    {

                        int minValue10 = possiblePrices[0];
                        int IDminValue10 = IDpossiblePrices[0];

                        for (int i = 1; i < possiblePrices.Count; i++)
                        {
                            if (possiblePrices[i] < minValue10)
                            {
                                minValue10 = possiblePrices[i];
                                IDminValue10 = IDpossiblePrices[i];
                            }

                        }

                        Console.WriteLine(Shops[IDminValue10]);
                        return Shops[IDminValue10];
                    }
                    else
                    {

                        return "";
                    }

                default:
                    return "";
                    
            }
            
            
        }
    }
}



