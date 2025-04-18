using System;
using Pizzeria.users;

namespace Pizzeria.menus
{
    public class AdminMenu()
    {
        public delegate void EventHandler(Admin admin);
        public event EventHandler OnHireWorker;
        public event EventHandler OnFireWorker;
        public event EventHandler OnIncreaseSalary;
        public event EventHandler OnDecreaseSalary;
        public event EventHandler OnLogOut;

        //metoda wyświetlająca menu dla admina
        public void Menu(Admin admin = null)
        {
            //subskrybuje zdarzenia
            OnHireWorker += OnHireWorkerHandler;
            OnFireWorker += OnFireWorkerHandler;
            OnIncreaseSalary += OnIncreaseSalaryHandler;
            OnDecreaseSalary += OnDecreaseSalaryHandler;
            OnLogOut += OnLogOutHandler;

            Console.Clear();
            Console.WriteLine("///////////////////////////////////////////");
            Console.WriteLine("         Witamy w pizzerii!      ");
            Console.WriteLine("///////////////////////////////////////////");
            Console.WriteLine();
            Console.WriteLine("1. Dodaj pracownika");
            Console.WriteLine("2. Zwolnij pracownika");
            Console.WriteLine("3. Podnieś pensje");
            Console.WriteLine("4. Obniż pensje");
            Console.WriteLine("5. Wyloguj");
            Console.WriteLine("6. Wyjdź");
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
                    OnHireWorker?.Invoke(admin);
                    admin.HireWorker();
                    break;
                case "2":
                    OnFireWorker?.Invoke(admin);
                    admin.FireWorker();
                    break;
                case "3":
                    OnIncreaseSalary?.Invoke(admin);
                    admin.IncreaseSalary();
                    break;
                case "4":
                    OnDecreaseSalary?.Invoke(admin);
                    admin.DecreaseSalary();
                    break;
                case "5":
                    OnLogOut?.Invoke(admin);
                    Console.WriteLine("Wylogowywanie...");
                    Thread.Sleep(1500);
                    LoggingMenu LM = new LoggingMenu();
                    LM.Menu();
                    break;
                case "6":
                    Console.WriteLine("Wychodzenie z programu...");
                    Thread.Sleep(1500);
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Niepoprawny wybór, spróbuj ponownie.");
                    Thread.Sleep(1500);
                    Menu(admin);
                    break;
            }
        }

        //obsługa zdarzeń
        public static void OnHireWorkerHandler(Admin admin)
        {
            string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "logs.txt");
            string logMessage = $"{DateTime.Now}: Admin {admin.Username} zatrudnił pracownika!";
            File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
        }
        public static void OnFireWorkerHandler(Admin admin)
        {
            string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "logs.txt");
            string logMessage = $"{DateTime.Now}: Admin {admin.Username} zwolnił pracownika!";
            File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
        }
        public static void OnIncreaseSalaryHandler(Admin admin)
        {
            string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "logs.txt");
            string logMessage = $"{DateTime.Now}: Admin {admin.Username} zwiększył pensję pracownika!";
            File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
        }
        public static void OnDecreaseSalaryHandler(Admin admin)
        {
            string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "logs.txt");
            string logMessage = $"{DateTime.Now}: Admin {admin.Username} zmniejszył pensję pracownika!";
            File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
        }
        public static void OnLogOutHandler(Admin admin)
        {
            string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "logs.txt");
            string logMessage = $"{DateTime.Now}: Admin {admin.Username} wylogował się!";
            File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
        }
    }
}
