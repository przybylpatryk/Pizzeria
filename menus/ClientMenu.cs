using System;
using System.Threading;
using Pizzeria.database;
using Pizzeria.users;

namespace Pizzeria.menus
{
    public class ClientMenu
    {
        public delegate void EventHandler(Client client);
        public event EventHandler OnCreatedOrder;


        //metoda wyświetlająca menu dla klienta
        public void Menu(Client client = null)
        {
            OnCreatedOrder += OnCreatedOrderHandler;

            Console.Clear();
            Console.WriteLine("///////////////////////////////////////////");
            Console.WriteLine("         Witamy w pizzerii!      ");
            Console.WriteLine("///////////////////////////////////////////");
            Console.WriteLine();
            Console.WriteLine("1. Zamów pizzę");
            Console.WriteLine("2. Odbierz pizze");
            Console.WriteLine("3. Zobacz zamówienia");
            Console.WriteLine("4. Dodaj recenzję");
            Console.WriteLine("5. Zobacz recenzje");
            Console.WriteLine("6. Wyloguj");
            Console.WriteLine("7. Wyjdź");
            Console.WriteLine();
            Console.Write("Twój wybór: ");

            //sprawdza czy użytkownik nie dał nic
            string? choice = Console.ReadLine();
            while (string.IsNullOrEmpty(choice))
            {
                Console.Write("Pusto!, spróbuj ponownie: ");
                choice = Console.ReadLine();
            }
            
            switch (choice)
            {
                case "1":
                    OnCreatedOrder?.Invoke(client);
                    client.CreateOrder();
                    break;
                case "2":
                    client.GetOrderForC();
                    break;
                case "3":
                    client.OrderPreview();
                    break;
                case "4":
                    client.AddReview();
                    break;
                case "5":
                    client.GetReviews();
                    break;
                case "6":
                    Console.WriteLine("Wylogowywanie...");
                    Thread.Sleep(1500);
                    LoggingMenu LM = new LoggingMenu();
                    LM.Menu();
                    break;
                case "7":
                    Console.WriteLine("Wychodzenie z programu...");
                    Thread.Sleep(1500);
                    Environment.Exit(0);
                default:
                    Console.WriteLine("Niepoprawny wybór, spróbuj ponownie.");
                    Thread.Sleep(1500);
                    Menu();
                    break;
            }
        }

        public static void OnCreatedOrderHandler(Client client)
        {
            string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "logs.txt");
            string logMessage = $"{DateTime.Now}: Klient {client.Username} złożył zamówienie!";
            File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
        }
    }
}