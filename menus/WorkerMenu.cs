using System;
using Pizzeria.users;


namespace Pizzeria.menus
{
    public class WorkerMenu
    {
        public delegate void EventHandler(Worker worker);
        public event EventHandler OnTakeOrder;
        public event EventHandler OnMakePizza;
        public event EventHandler OnOrderPreview;
        public event EventHandler OnLogOut;

        //metoda wyświetlająca menu dla pracownika
        public void Menu(Worker worker = null)
        {
            //subskrybuje zdarzenia
            OnTakeOrder += OnTakeOrderHandler;
            OnMakePizza += OnMakePizzaHandler;
            OnOrderPreview += OnOrderPreviewHandler;
            OnLogOut += OnLogOutHandler;

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
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Niepoprawny wybór, spróbuj ponownie.");
                    Thread.Sleep(1500);
                    Menu();
                    break;
            }
        }
        public static void OnTakeOrderHandler(Worker worker)
        {
            string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "logs.txt");
            string logMessage = $"{DateTime.Now}: Pracownik {worker.Username} podjął się zamówienia!";
            File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
        }
        public static void OnMakePizzaHandler(Worker worker)
        {
            string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "logs.txt");
            string logMessage = $"{DateTime.Now}: Pracownik {worker.Username} wykonał zamówienie!";
            File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
        }
        public static void OnOrderPreviewHandler(Worker worker)
        {
            string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "logs.txt");
            string logMessage = $"{DateTime.Now}: Pracownik {worker.Username} obejrzał zamówienie!";
            File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
        }
        public static void OnLogOutHandler(Worker worker)
        {
            string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "logs.txt");
            string logMessage = $"{DateTime.Now}: Pracownik {worker.Username} wylogował się!";
            File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
        }
    }
}