// Данное Пространство имен предоставляет базовые классы и базовые функциональные возможности языка C#.
using System;

// Пространство имен, содержащее интерфейсы и классы для работы со списками данных.
using System.Collections.Generic;

// Это пространство имен содержит классы для работы с асинхронными задачами.
using System.Threading.Tasks;

// Пространство имен, которое содержит классы для работы с Telegram API.
using Telegram.Bot;

// Пространство имен, содержащее типы данных, котороые используются
// для взаимодействия с API Telegram Bot, такие как Update, Message и тд.
using Telegram.Bot.Types;

// Пространство имен для создания и отправки различных разметок сообщений в Telegram,например, ReplyKeyboardMarkup.
using Telegram.Bot.Types.ReplyMarkups;

// Это пространство имен содержит расширения для работы с Telegram API
using Telegram.Bot.Extensions;

// Пространство имен для создания и управления Telegram ботами

// Пространство имен для работы с XML-документами
using System.Xml.Linq;

// Пространство имен для работы с форматом JSON
using Newtonsoft.Json.Linq;

// Пространство имен для взаимодействия с неуправляемым кодом
using System.Runtime.InteropServices;

// Пространство имен для работы с файлами и потоками данных
using System.IO;

// Пространство имен для работы с потоками данных и выполнения асинхронных операций
using System.Threading;


//Объявление пространства имен, в котором находится программа
namespace ConsoleApp1

// Объявление класса Program
{
    class Program
    {
        //Объявление статического объекта botClient, представляющего собой экземпляр класса TelegramBotClient
        static ITelegramBotClient botClient;

        //Объявление метода Main, который является точкой входа в программу
        static void Main(string[] args)
        {
            //Создание экземпляра класса TelegramBotClient с использованием токена для доступа к API телеграм бота
            botClient = new TelegramBotClient("6459137619:AAEExP-mctMCdqoSyo84OZwK9h9kWGBjc0Y");
            //Запуск процесса приема обновлений от Telegram с помощью метода StartReceiving.
            //В качестве аргументов передаются методы Update и Error,
            //которые будут вызываться при получении обновлений или возникновении ошибок или получении сообщения
            botClient.StartReceiving(Update, Error);

            //Вывод сообщения в консоль о запуске бота и ожидание нажатия клавиши в консоли,
            //чтобы программа не завершилась сразу после запуска
            Console.WriteLine("Coffee Bot is running...");
            Console.ReadLine();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //функция на с++ (то что выше - static void Main...)
        /*int main(int argc, char* argv[])
        {
                botClient = new TelegramBotClient("6459137619:AAEExP-mctMCdqoSyo84OZwK9h9kWGBjc0Y");
                botClient.StartReceiving(Update, Error);

                cout << "Coffee Bot is running..." << endl;
                cin.get();
                return 0;
        }*/
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //Объявление метода Error, который будет вызываться при возникновении ошибки при обработке обновлений.
        private static Task Error(ITelegramBotClient botClient, Exception exception, CancellationToken token)
        {
            return Task.CompletedTask;
        }

        //Обявление метода Update, который будет вызываться при получении нового обновления от Telegram.
        async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {


            var message = update.Message;

            if (message.Text != null)
            {
                Console.WriteLine($"Received a message from {message.Chat.FirstName}: {message.Text}");

                //Инициализация массива строк Shops, содержащего названия кофеен.
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
                //Инициализация массива строк Drinks, содержащего названия напитков.
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
                //Инициализация двумерного массива Prices, содержащего цены на напитки в различных кофеях.
                //В двумерном массиве отражается соответствие цены на конкретное кофе в конкретной кофейне.
                //По строкам отражается название кофеен, по столбцам - название кофе. На пересечении столбцов и строк отражены цены.
                //Если значение = -1, значит в кофейне отсутствует вид определенного кофе.
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

                //Объявление переменной reply для хранения ответа бота
                var reply = "";

                //Объявление строковой переменной для хранения выбранного напитка
                string selectedDrink = "";

                //Объявление строковой переменной для хранения ценового диапазона, выбранного пользователем
                string selectedPriceRange = "";

                //Объявление целочисленной переменной для хранения минимальной/максимальной цены в дальнейшем
                int selectedMinPrice = 0;
                int selectedMaxPrice = 0;

                // Функция, которая обновляет сообщение от Telegram
                message = update.Message;
                // Условная конструкция, которая проверяет, равен ли текст сообщения "/start".
                if (message.Text == "/start")
                {
                    reply = "Привет! Я бот, который поможет тебе выбрать кофейню. Выбери на клавиатуре свой любимый напиток:";
                    //Вызывается асинхронный метод `SendTextMessageAsync` у объекта `botClient`,
                    //который является экземпляром класса TelegramBotClient.
                    //Этот метод отправляет текстовое сообщение пользователю через Telegram Bot API.
                    //`update.Message.Chat.Id` - идентификатор чата, куда будет отправлено сообщение, он извлекается из объекта `update.Message`.
                    //`reply` - текст сообщения, который будет отправлен пользователю.
                    //replyMarkup: GetCoffeeMenu() - клавиатурное меню для выбора напитка, которое отображается у пользователя.
                    //Далее происходит ожидание ответа от пользователя
                    await botClient.SendTextMessageAsync(message.Chat.Id, reply, replyMarkup: GetCoffeeMenu());
                }

                // После выбора пользователем напитка начинается проверка условия: если текст сообщения равен одному из списка напитков.
                else if (message.Text == "Раф" ^ message.Text == "Флэт уайт" ^ message.Text == "Латте" ^ message.Text == "Эспрессо" ^ message.Text == "Американо" ^ message.Text == "Капучино" ^ message.Text == "Глясе" ^ message.Text == "Макиато" ^ message.Text == "Мокачино" ^ message.Text == "Матча" ^ message.Text == "Мокко")
                {
                    // Выбранный пользователем напиток присваивается переменной selectedDrink.
                    selectedDrink = message.Text;
                    // Выводится в консоль информация о выбранном напитке.
                    Console.WriteLine($"Выбранный напиток: {selectedDrink}");

                    //Создается экземпляр `StreamWriter` для записи выбранного напитка в файл "selecteddrink.txt".
                    StreamWriter writer = new StreamWriter("C:\\Users\\daani\\Downloads\\selecteddrink.txt");
                    //Выбранный напиток записывается в файл.
                    writer.Write(selectedDrink);
                    //Закрывается поток записи
                    writer.Close();
                    //Присвоение переменной reply указанного ниже текста для дальнейшего отображения его пользователю
                    reply = "Выбери на клавиатуре диапазон цены:";
                    //Вызывается асинхронный метод `SendTextMessageAsync` у объекта `botClient`,
                    //который является экземпляром класса TelegramBotClient.
                    //Этот метод отправляет текстовое сообщение пользователю через Telegram Bot API.
                    //`update.Message.Chat.Id` - идентификатор чата, куда будет отправлено сообщение, он извлекается из объекта `update.Message`.
                    //`reply` - текст сообщения, который будет отправлен пользователю.
                    //replyMarkup: GetPriceRangeMenu() - клавиатурное меню для выбора ценового диапазона, которое отображается у пользователя
                    //Далее происходит ожидание ответа от пользователя
                    await botClient.SendTextMessageAsync(update.Message.Chat.Id, reply, replyMarkup: GetPriceRangeMenu());
                }

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //условный оператор на с++, представленный выше
                /*if (message.Text == "Раф" || message.Text == "Флэт уайт" || message.Text == "Латте" || message.Text == "Эспрессо" || message.Text == "Американо" || message.Text == "Капучино" || message.Text == "Глясе" || message.Text == "Макиато" || message.Text == "Мокачино" || message.Text == "Матча" || message.Text == "Мокко")
                {
                        selectedDrink = message.Text;
                        cout << "Выбранный напиток: " << selectedDrink << endl;

                        ofstream writer("C:\\Users\\daani\\Downloads\\selecteddrink.txt");
                        writer << selectedDrink;
                        writer.close();
                        reply = "Выбери на клавиатуре диапазон цены:";
                        botClient.SendTextMessageAsync(update.Message.Chat.Id, reply, replyMarkup: GetPriceRangeMenu());
                }*/
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                //После выбора пользователем желаемого диапазона цен начинается проверка на соответствие текста сообщения (от пользователя)
                //одному из списка ценовых диапазонов.
                else if (message.Text == "<150" ^ message.Text == "160-250" ^ message.Text == "260-350" ^ message.Text == ">350")
                {
                    //Выбранный пользователем диапазон цен присваивается переменной selectedPriceRange
                    selectedPriceRange = message.Text;
                    //Вывод выбранного ценового диапазона в консоль.
                    Console.WriteLine($"Выбранный диапазон цен: {selectedPriceRange}");
                    //Создается экземпляр `StreamReader` для чтения файла "selecteddrink.txt".
                    StreamReader reader = new StreamReader("C:\\Users\\daani\\Downloads\\selecteddrink.txt");
                    //Чтение данных из файла, которые были ранее записаны в него, и присвоение их переменной selectedDrink
                    //для дальнейшего использования
                    selectedDrink = reader.ReadToEnd();
                    //Вывод selectedDrink в консоль
                    Console.WriteLine(selectedDrink);

                    //Закрытие потока чтения файла
                    reader.Close();

                    //Следующие условные конструкции предлагают пользователю опредленную кофейню,
                    //основываясь на выбранном напитке и диапазоне цен

                    //Если пользователь выбрал диапазон цен "<150"
                    if (message.Text == "<150")
                    {
                        //Присваиваем переменным selectedMinPrice и selectedMaxPrice значения,
                        //соответствующие условию (message.Text == "<150") для того, чтобы остальные ценовые диапазоны исключить из проверки
                        selectedMinPrice = 0;
                        selectedMaxPrice = 150;

                        //Вызов функции FindRecommendedCoffeeShop с указанными параметрами,
                        //которая возвращает рекомендованную кофейню на основе выбранных параметров
                        //Параметры: selectedDrink - переменная, содержащая выбранный пользователем напиток,
                        //который ранее был записан в файл selecteddrink.txt";
                        //Shops - строковый массив данных всех кофеен, объявленных ранее
                        //Drinks - строковый массив данных всех напитков, объявленных ранее
                        //Prices массив данных всех цен, объявленных ранее
                        //selectedMinPrice, selectedMaxPrice - минимальное и максимальное значение цены из выбранного пользователем диапазона

                        //Присвоение результата переменной recommendedCoffeeShop.
                        string recommendedCoffeeShop = FindRecommendedCoffeeShop(selectedDrink, Shops, Drinks, Prices, selectedMinPrice, selectedMaxPrice);

                        //Начинается проверка (через вызов функции recommendedCoffeeShop),
                        //является ли рекомендованная кофейня пустой строкой,
                        //что означает, что не была найдена подходящая кофейня.
                        if (recommendedCoffeeShop == "")
                        {
                            //reply присваивается ответное текстовое сообщение, уведомляющее пользователя о невозможности найти подходящую кофейню.
                            reply = "Мне жаль, но я не могу порекомендовать кофейню на основе выбранных параметров.";
                        }

                        else
                        {
                            //reply присваивается ответное текстовое сообщение с рекомендацией посетить определенную кофейню.
                            reply = $"Я рекомендую тебе посетить кофейню \"{recommendedCoffeeShop}\".";
                        }
                    };

                    //Эта условная конструкция аналогична предыдущей за исключением отличающегося выбранного пользователем диапазона цен
                    if (message.Text == "160-250")
                    {
                        //Присваиваем переменным selectedMinPrice и selectedMaxPrice значения, 
                        //соответствующие условию (message.Text == "160-250") для того, чтобы остальные ценовые диапазоны исключить из проверки
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

                    //Эта условная конструкция аналогична предыдущей за исключением отличающегося выбранного пользователем диапазона цен
                    if (message.Text == "260-350")
                    {
                        //Присваиваем переменным selectedMinPrice и selectedMaxPrice значения,
                        //соответствующие условию (message.Text == "260-350") для того, чтобы остальные ценовые диапазоны исключить из проверки
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
                    //Эта условная конструкция аналогична предыдущей за исключением отличающегося выбранного пользователем диапазона цен
                    if (message.Text == ">350")
                    {
                        //Присваиваем переменным selectedMinPrice и selectedMaxPrice значения,
                        //соответствующие условию (message.Text == ">350") для того, чтобы остальные ценовые диапазоны исключить из проверки
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



                    //Отправка текстового сообщения пользователю с рекомендацией кофейни.
                    await botClient.SendTextMessageAsync(update.Message.Chat.Id, reply);

                    //В случае, если текстовое сообщение не соответствует предыдущим условиям (не "/start", не выбор напитка,
                    //не выбор диапазона цен),выполняется блок `else`.

                }
                else
                {
                    //reply присваивается сообщение о том, что бот не может понять сообщение пользователя.
                    reply = "Я не могу понять твоё сообщение. Попробуй еще раз.";
                    //Затем через асинхронный метод `SendTextMessageAsync` пользователю отправляется текстовое сообщение с просьбой попробовать еще раз,
                    //включая клавиатуру с меню выбора напитков (replyMarkup: GetCoffeeMenu()).
                    await botClient.SendTextMessageAsync(update.Message.Chat.Id, reply, replyMarkup: GetCoffeeMenu());
                }

            }
        }
        //Ниже представленный участок кода создает и возвращает пользовательское меню с кнопками для выбора напитков.
        //Каждая кнопка представляет определенный напиток, и эти кнопки далее собираются в список,
        //который затем используется для создания объекта `ReplyKeyboardMarkup`, представляющего меню.

        //`static ReplyKeyboardMarkup GetCoffeeMenu()` - Объявляет статический метод `GetCoffeeMenu`,
        //который возвращает объект типа `ReplyKeyboardMarkup`. Эта функция используется для создания меню выбора напитков.
        static ReplyKeyboardMarkup GetCoffeeMenu()
        {
            //Создание нового списка, представляющего элементы клавиатуры.
            var buttons = new List<KeyboardButton[]>()
            {
                //Инициализируем элементы списка как массивы `KeyboardButton[]`
                new[]
                {
                    //Создаем кнопки для каждого вида кофе
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

            //Возвращаем новый экземпляр `ReplyKeyboardMarkup`, который представляет клавиатуру с кнопками для выбора напитков. 
            return new ReplyKeyboardMarkup(buttons);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //на с++
        /*ReplyKeyboardMarkup GetCoffeeMenu()
        {
                std::vector<std::vector<KeyboardButton>> buttons = {
                {
                        KeyboardButton("Раф"),
                        KeyboardButton("Флэт уайт"),
                        KeyboardButton("Латте"),
                        KeyboardButton("Эспрессо"),
                        KeyboardButton("Американо")
                },
                {
                        KeyboardButton("Капучино"),
                        KeyboardButton("Глясе"),
                        KeyboardButton("Макиато"),
                        KeyboardButton("Мокачино"),
                        KeyboardButton("Матча")
                },
                {
                        KeyboardButton("Мокко")
                }
            };

            return ReplyKeyboardMarkup(buttons);
        }*/
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        //Приведенный ниже участок кода создает и возвращает пользовательское меню с кнопками для выбора диапазона цен.
        //Каждая кнопка представляет определенный диапазон цен,и эти кнопки собираются в список,
        //который затем используется для создания объекта `ReplyKeyboardMarkup`, представляющего меню.


        //`static ReplyKeyboardMarkup GetPriceRangeMenu()` - Объявляет статический метод `GetPriceRangeMenu`,
        //который возвращает объект типа `ReplyKeyboardMarkup`.
        //Эта функция используется для создания меню выбора диапазона цен.
        static ReplyKeyboardMarkup GetPriceRangeMenu()
        {
            //Создание нового списка, представляющего элементы клавиатуры.
            var buttons = new List<KeyboardButton[]>()
            {
                //Инициализируем элементы списка как массивы `KeyboardButton[]`
                new[]
                {
                    new KeyboardButton("<150"),
                    new KeyboardButton("160-250"),
                    new KeyboardButton("260-350"),
                    new KeyboardButton(">350"),
                }
            };

            //Возвращаем новый экземпляр `ReplyKeyboardMarkup`, который представляет клавиатуру с кнопками для выбора диапазона цен.
            return new ReplyKeyboardMarkup(buttons);
        }



        //В ниже представленной части кода происходит объявление функции `FindRecommendedCoffeeShop`.
        //Здесь инициализируются необходимые переменные и списки для последующего выполнения логики программного кода по выбору
        //рекомендуемой кофейни на основе выбранного напитка и диапазона цен.

        //Статический метод `FindRecommendedCoffeeShop` возвращает объект типа `string` и принимает следующие параметры:
        // `selectedDrink` - выбранный пользователем напиток,
        // `Shops` - массив строк, представляющий список кофеен,
        // `Drinks` - массив строк, представляющий список напитков,
        // `Prices` - двумерный массив целых чисел, представляющий цены на напитки в различных кофейнях,
        // `MinPrice` - минимальная цена из выбранного ценового диапазона
        // `MaxPrice` - максимальная цена из выбранного ценового диапазона

        static string FindRecommendedCoffeeShop(string selectedDrink, string[] Shops, string[] Drinks, int[,] Prices, int MinPrice, int MaxPrice)
        {
            //Создается новый массив целых чисел column размерностью 16.
            //Этот массив будет использоваться для хранения цен на выбранный напиток.
            int[] column = new int[16];

            //Создается новый список значений `possiblePrices` типа `int`. Он будет использоваться для хранения цен,
            //попадающих в выбранный диапазон цен.
            List<int> possiblePrices = new List<int>();

            //Создаем новый список значений `IDpossiblePrices` типа `int`. Он будет использоваться для хранения индексов цен,
            //соответствующих выбранному диапазону цен.
            List<int> IDpossiblePrices = new List<int>();

            //Вывод сообщения в консоль, указывающего на активацию этой функции выбора кофе.
            Console.WriteLine("функция выбора кофе активирована");

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //на с++ (код на c# ниже)
            /*switch (selectedDrink)
            {
                case "Раф":

                    for (int i = 0; i < 11; i++)
                    {
                        column[i] = Prices[i][3];
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        if (column[i] <= MaxPrice && column[i] >= MinPrice)
                            {
                            possiblePrices.push_back(column[i]);
                            IDpossiblePrices.push_back(i);
                        }
                    }
                    if (possiblePrices.size() > 0)
                    {
                        int minValue = possiblePrices[0];
                        int IDminValue = IDpossiblePrices[0];

                        for (int i = 1; i < possiblePrices.size(); i++)
                        {
                            if (possiblePrices[i] < minValue)
                            {
                                minValue = possiblePrices[i];
                                IDminValue = IDpossiblePrices[i];
                            }
                        }

                        cout << Shops[IDminValue] << endl;
                        return Shops[IDminValue];
                     }
                     else
                     {
                        return "";
                     }
            }*/
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 

            //Следующий участок кода находит кофейню с минимальной ценой среди доступных в выбранном диапазоне цен и возвращает ее
            //в качестве рекомендации, либо пустую строку, если подходящих кофеен не найдено.

            //Ниже начинается выполнение оператора множественного выбора `switch`, который основан на значении переменной `selectedDrink`.
            //В зависимости от выбранного напитка выполняется соответствующий блок кода.
            switch (selectedDrink)
            {
                //Case определяет первый блок кода для случая, когда `selectedDrink` равно "Раф".
                case "Раф":

                    //В этом цикле происходит заполнение массива `column` значениями цен для выбранного напитка.
                    //Prices[i, 3] обращается к цене для i-той кофейни и третьего напитка
                    for (int i = 0; i < 11; i++)
                    {
                        column[i] = Prices[i, 3];
                    }
                    //Этот цикл проверяет, соответствует ли каждая цена в массиве `column` выбранному диапазону цен.
                    for (int i = 0; i < 16; i++)
                    {
                        //Если цена подходит в выбранный диапазон, то она добавляется в список `possiblePrices` (хранящий цены) и
                        //соответствующий индекс добавляется в список `IDpossiblePrices` (хранящий индексы цен, соответствующих диапазону)
                        if (column[i] <= MaxPrice && column[i] >= MinPrice)
                        {
                            possiblePrices.Add(column[i]);
                            IDpossiblePrices.Add(i);
                        }

                    }
                    //Это условие проверяет, содержит ли список `possiblePrices` какие-либо цены.
                    //Если список не пустой, выполняется блок кода в фигурных скобках, иначе выполняется блок после `else`.
                    if (possiblePrices.Count > 0)
                    {

                        //Создаем переменные `minValue` и `IDminValue`, которые инициализируются значением первого элемента
                        //соответствующих списков.
                        int minValue = possiblePrices[0];
                        int IDminValue = IDpossiblePrices[0];

                        //Цикл перебирает оставшиеся элементы в списках `possiblePrices` и `IDpossiblePrices`.
                        for (int i = 1; i < possiblePrices.Count; i++)
                        {
                            //При обнаружении значения цены меньше, чем текущее минимальное значение, оно становится новым
                            //минимальным значением, и соответствующий ID сохраняется в переменной `IDminValue`.
                            if (possiblePrices[i] < minValue)
                            {
                                minValue = possiblePrices[i];
                                IDminValue = IDpossiblePrices[i];
                            }

                        }
                        //Выводится название кофейни с минимальной ценой в консоль, а затем это название возвращается в качестве рекомендации.
                        Console.WriteLine(Shops[IDminValue]);
                        return Shops[IDminValue];
                    }
                    //Если список `possiblePrices` пуст, то возвращается пустая строка.
                    else
                    {

                        return "";
                    }

                //Для всех последующих case принцип аналогичен тому, что описан выше

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
