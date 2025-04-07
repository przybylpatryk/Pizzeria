using System;
using Pizzeria.users;

namespace Pizzeria.menus
{
    public class AdminMenu()
    {
        public void Menu(Admin admin)
        {
            Console.Clear();
            Console.WriteLine("///////////////////////////////////////////");
            Console.WriteLine("         Witamy w pizzerii!      ");
            Console.WriteLine("///////////////////////////////////////////");
            Console.WriteLine();
            Console.WriteLine("1. Dodaj pracownika");
            Console.WriteLine("2. Zwolnij pracownika");
            Console.WriteLine("3. Podnieś pensje");
            Console.WriteLine("4. Wyjdź");
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
                    admin.HireWorker();
                    break;
                case "2":
                    admin.FireWorker();
                    break;
                case "3":
                    admin.IncreaseSalary();
                    break;
                case "4":
                    Console.WriteLine("Wychodzenie z programu...");
                    Thread.Sleep(1500);
                    return;
                default:
                    Console.WriteLine("Niepoprawny wybór, spróbuj ponownie.");
                    Thread.Sleep(1500);
                    Menu(admin);
                    break;
            }
        }
    }
}
