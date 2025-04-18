using System;
using Pizzeria.users;


namespace Pizzeria.menus
{
    public class WorkerMenu
    {
        //metoda wyświetlająca menu dla pracownika
        public void Menu(Worker worker = null)
        {
            Console.Clear();
            Console.WriteLine("///////////////////////////////////////////");
            Console.WriteLine("         Witamy w pizzerii!      ");
            Console.WriteLine("///////////////////////////////////////////");
            Console.WriteLine();
            Console.WriteLine("1. Podejmij się zamówienia");
            Console.WriteLine("2. Stwórz pizzę");
            Console.WriteLine("3. Przeglądaj zamówienia");
            Console.WriteLine("4. Wyloguj");
            Console.WriteLine("5. Wyjdź");
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
                    worker.TakeOrder();
                    break;
                case "2":
                    worker.MakePizza();
                    break;
                case "3":
                    worker.OrderPreview();
                    break;
                case "4":
                    Console.WriteLine("Wylogowywanie...");
                    Thread.Sleep(1500);
                    LoggingMenu LM = new LoggingMenu();
                    LM.Menu();
                    break;
                case "5":
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