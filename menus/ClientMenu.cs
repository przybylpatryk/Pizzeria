using System;
using System.Threading;
using Pizzeria.database;
using Pizzeria.users;

namespace Pizzeria.menus
{
    public class ClientMenu
    {
        public void Menu(Client client = null)
        {
            Console.Clear();
            Console.WriteLine("///////////////////////////////////////////");
            Console.WriteLine("         Witamy w pizzerii!      ");
            Console.WriteLine("///////////////////////////////////////////");
            Console.WriteLine();
            Console.WriteLine("1. Zamów pizzę");
            Console.WriteLine("2. Dodaj recenzję");
            Console.WriteLine("3. Wyjdź");
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
                    client.CreateOrder();
                    break;
                case "2":
                    client.AddReview();
                    break;
                case "3":
                    Console.WriteLine("Wychodzenie z programu...");
                    Thread.Sleep(1500);
                    return;
                default:
                    Console.WriteLine("Niepoprawny wybór, spróbuj ponownie.");
                    Thread.Sleep(1500);
                    Menu();
                    break;
            }
        }
    }
}