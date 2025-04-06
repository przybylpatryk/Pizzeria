using System;
using System.Text.RegularExpressions;
using Pizzeria.users;
using Pizzeria.database;
namespace Pizzeria.menus;

public class LoggingMenu
{
    public event Action<User> UserCreated;

    public Database DB { get; set; }
    public LoggingMenu()
    {
        //tworzenie bazy danych
        DB = new Database();
    }
    public void Menu()
    {
        Console.Clear();
        Console.WriteLine("///////////////////////////////////////////");
        Console.WriteLine("         Witamy w pizzerii!      ");
        Console.WriteLine("///////////////////////////////////////////");
        Console.WriteLine();
        Console.WriteLine("1. Bywalec? Zaloguj się");
        Console.WriteLine("2. Nowy klient? Zarejestruj się");
        Console.WriteLine("3. Pracujesz tu? Zaloguj się i pracuj");
        Console.WriteLine("4. Admin? Proszę tędy");
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

        //sprawdzanie przypadków
        switch (choice)
        {
            case "1":
                ClientLogin();
                break;
            case "2":
                Register();
                break;
            case "3":
                WorkerLogin();
                break;
            case "4":
                AdminLogin();
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

	public void ClientLogin()
	{
        //wizuanie ładnie wyświetla
        Console.Clear();
        Console.WriteLine("///////////////////////////////////////////");
        Console.WriteLine("         Prosimy o zalogowanie      ");
        Console.WriteLine("///////////////////////////////////////////");
        Console.WriteLine();
        Console.Write("Nazwa użytkownika: ");

        //sprawdzanie czy użytkownik nie klika poprostu entera
        string? username = Console.ReadLine();
        while (string.IsNullOrEmpty(username))
        {
            Console.Write("Pusto!, spróbuj ponownie: ");
            username = Console.ReadLine();
        }
        Console.Write("\nHasło: ");

        //sprawdzanie czy użytkownik nie klika poprostu entera
        string? password = Console.ReadLine();
        while (string.IsNullOrEmpty(password))
        {
            Console.Write("Pusto!, spróbuj ponownie: ");
            password = Console.ReadLine();
        }

        //pobiera wszystkich klientów z bazy i porównywanie hasła i loginu
        List<Client> clients = DB.GetClients();

        if (clients.Any(c => c.Username == username && c.Password == password))
        {
            Console.WriteLine("Zalogowano pomyślnie!");
            Thread.Sleep(1500);
            Client client = new Client(username, password);
            client.clientMenu.Menu(client);
        }
        else
        {
            Console.WriteLine("Niepoprawne dane logowania!");
            Thread.Sleep(1500);
            Menu();
        }
    }

    public void WorkerLogin()
    {
        //wizuanie ładnie wyświetla
        Console.Clear();
        Console.WriteLine("///////////////////////////////////////////");
        Console.WriteLine("         Prosimy o zalogowanie      ");
        Console.WriteLine("///////////////////////////////////////////");
        Console.WriteLine();
        Console.Write("Nazwa użytkownika: ");

        //sprawdzanie czy użytkownik nie klika poprostu entera
        string? username = Console.ReadLine();
        while (string.IsNullOrEmpty(username))
        {
            Console.Write("Pusto!, spróbuj ponownie: ");
            username = Console.ReadLine();
        }
        Console.Write("\nHasło: ");

        //sprawdzanie czy użytkownik nie klika poprostu entera
        string? password = Console.ReadLine();
        while (string.IsNullOrEmpty(password))
        {
            Console.Write("Pusto!, spróbuj ponownie: ");
            password = Console.ReadLine();
        }

        //pobiera wszystkich klientów z bazy i porównywanie hasła i loginu
        List<Worker> workers = DB.GetWorkers();

        if (workers.Any(w => w.Username == username && w.Password == password))
        {
            Console.WriteLine("Zalogowano pomyślnie!");
            Thread.Sleep(1500);
            Worker worker = new Worker(username, password);
            worker.workerMenu.Menu(worker);
        }
        else
        {
            Console.WriteLine("Niepoprawne dane logowania!");
            Thread.Sleep(1500);
            Menu();
        }
    }

    public void AdminLogin()
    {
        //wizuanie ładnie wyświetla
        Console.Clear();
        Console.WriteLine("///////////////////////////////////////////");
        Console.WriteLine("         Prosimy o zalogowanie      ");
        Console.WriteLine("///////////////////////////////////////////");
        Console.WriteLine();
        Console.Write("Nazwa użytkownika: ");

        //sprawdzanie czy użytkownik nie klika poprostu entera
        string? username = Console.ReadLine();
        while (string.IsNullOrEmpty(username))
        {
            Console.Write("Pusto!, spróbuj ponownie: ");
            username = Console.ReadLine();
        }
        Console.Write("\nHasło: ");

        //sprawdzanie czy użytkownik nie klika poprostu entera
        string? password = Console.ReadLine();
        while (string.IsNullOrEmpty(password))
        {
            Console.Write("Pusto!, spróbuj ponownie: ");
            password = Console.ReadLine();
        }

        //pobiera wszystkich klientów z bazy i porównywanie hasła i loginu
        List<Admin> admins = DB.GetAdmins();

        if (admins.Any(a => a.Username == username && a.Password == password))
        {
            Console.WriteLine("Zalogowano pomyślnie!");
            Thread.Sleep(1500);
            Admin admin = new Admin(username, password);
            admin.adminMenu.Menu(admin);
        }
        else
        {
            Console.WriteLine("Niepoprawne dane logowania!");
            Thread.Sleep(1500);
            Menu();
        }
    }

    public void Register()
    {
        //wizuanie ładnie wyświetla
        Console.Clear();
        Console.WriteLine("///////////////////////////////////////////");
        Console.WriteLine("         Prosimy o zarejestrowanie się      ");
        Console.WriteLine("///////////////////////////////////////////");
        Console.WriteLine();
        Console.Write("Podaj nazwe użytkownika: ");

        //sprawdzanie czy użytkownik nie klika poprostu entera, lub czy też nazwa nie jest zajęta
        string? username = Console.ReadLine();
        while (string.IsNullOrEmpty(username) || isnotfree(username))
        {
            Console.Write("Pusto, lub ta nazwa jest już zajęta, spróbuj ponownie: ");
            username = Console.ReadLine();
        }
        Console.Write("\nHasło: ");

        //sprawdzanie czy użytkownik nie klika poprostu entera, lub czy też hasło nie jest za słabe
        string? password = Console.ReadLine();
        while (string.IsNullOrEmpty(password) || notmatches(password))
        {
            Console.Write("Pusto, lub za słabe hasło, spróbuj ponownie: ");
            password = Console.ReadLine();
        }

        DB.AddClient(username, password);
        Console.WriteLine("Zajerestrowano pomyślnie!");
        Thread.Sleep(1500);
        Client client = new Client(username, password);
        client.clientMenu.Menu(client);

        UserCreated += LogCreate; // Subskrybuj zdarzenie
        UserCreated?.Invoke(client);

    }

    public static bool notmatches(string password)
    {
        Regex regex = new Regex(@"^(?=.*\d)(?=.*[A-Z])(?=.*[!@#$%^&*()_+={}\[\]:;\'<>,.?/\\|`~-]).{5,}$");
        if(regex.IsMatch(password))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public static bool isnotfree(string username)
    {
        Database db = new Database();
        List<Client> clients = db.GetClients();
        List<Worker> workers = db.GetWorkers();
        List<Admin> admins = db.GetAdmins();
        if (clients.Any(c => c.Username == username) || workers.Any(w => w.Username == username) || admins.Any(a => a.Username == username))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //funkcja wywoływana przez zdarzenie do zapisu do logów
    public static void LogCreate(User user)
    {
        string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "logs.txt");
        string logMessage = $"{DateTime.Now}: Nowy użytkownik z nazwą {user.Username} został stworzony z rolą: {user.UserRole}";
        File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
    }
}
