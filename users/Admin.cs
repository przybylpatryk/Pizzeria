using System;
using System.Text.RegularExpressions;
using System.IO;
using Pizzeria.interfaces;
using Pizzeria.menus;
using Pizzeria.database;

namespace Pizzeria.users
{
    public class Admin : User, IWorkerManagment
    {

        public AdminMenu adminMenu { get; set; }
        public Database DB { get; set; }

        //konstruktor klasy Admin
        public Admin(string username, string password) : base(username, password, User.Role.Admin)
        {
            adminMenu = new AdminMenu();
            DB = new Database();
        }

        //zatrudnia pracownika
        public void HireWorker()
        {
            Console.Clear();
            Console.WriteLine("///////////////////////////////////////////");
            Console.WriteLine("      Prosimy o zarejestrowanie nowego pracownika      ");
            Console.WriteLine("///////////////////////////////////////////");
            Console.WriteLine("         Wpisz 'esc' by anulować         ");
            Console.WriteLine("///////////////////////////////////////////");
            Console.WriteLine();
            Console.Write("Podaj nazwe nowego pracownika: ");

            //sprawdzanie czy użytkownik nie klika poprostu entera, lub czy też nazwa nie jest zajęta
            string? username = Console.ReadLine();
            while (string.IsNullOrEmpty(username) || isnotfree(username))
            {
                if (username.ToLower() == "esc")
                {
                    Console.WriteLine("Anulowano dodawanie pracownika!");
                    Thread.Sleep(1500);
                    adminMenu.Menu(this);
                    return;
                }
                Console.Write("Pusto, lub ta nazwa jest już zajęta, spróbuj ponownie: ");
                username = Console.ReadLine();
            }
            Console.Write("\nHasło nowego pracownika: ");

            //sprawdzanie czy użytkownik nie klika poprostu entera, lub czy też hasło nie jest za słabe
            string? password = Console.ReadLine();
            while (string.IsNullOrEmpty(password) || notmatches(password))
            {
                if (password.ToLower() == "esc")
                {
                    Console.WriteLine("Anulowano dodawanie pracownika!");
                    Thread.Sleep(1500);
                    adminMenu.Menu(this);
                    return;
                }
                Console.Write("Pusto, lub za słabe hasło, spróbuj ponownie: ");
                password = Console.ReadLine();
            }

            DB.HireWorker(username, password);
            Console.WriteLine("Dodano pracownika pomyślnie!");
            Thread.Sleep(1500);
            adminMenu.Menu(this);
        }

        //zwalnia pracownika
        public void FireWorker()
        {
            Console.Clear();
            Console.WriteLine("///////////////////////////////////////////");
            Console.WriteLine("         Prosimy o usunięcie pracownika       ");
            Console.WriteLine("///////////////////////////////////////////");
            Console.WriteLine("         Wpisz 'esc' by anulować         ");
            Console.WriteLine("///////////////////////////////////////////");
            Console.WriteLine();
            Console.Write("Podaj pracownika do zwolnienia: ");

            //sprawdzanie czy użytkownik nie klika poprostu entera, lub czy też nazwa nie jest zajęta
            string? username = Console.ReadLine();
            while (string.IsNullOrEmpty(username) || isusername(username))
            {
                if (username.ToLower() == "esc")
                {
                    Console.WriteLine("Anulowano usuwanie pracownika!");
                    Thread.Sleep(1500);
                    adminMenu.Menu(this);
                    return;
                }
                Console.Write("Pusto, lub nie ma takiego użytkownika: ");
                username = Console.ReadLine();
            }
            Console.Write("\nHasło zwalnianego pracownika: ");

            //sprawdzanie czy użytkownik nie klika poprostu entera, lub czy też hasło nie jest za słabe
            string? password = Console.ReadLine();
            while (string.IsNullOrEmpty(password) || ispassword(username, password))
            {
                if (password.ToLower() == "esc")
                {
                    Console.WriteLine("Anulowano usuwanie pracownika!");
                    Thread.Sleep(1500);
                    adminMenu.Menu(this);
                    return;
                }
                Console.Write("Pusto, lub użytkownik nie ma takiego hasła: ");
                password = Console.ReadLine();
            }

            DB.FireWorker(username, password);
            Console.WriteLine("Usunięto pracownika pomyślnie!");
            Console.WriteLine("Życzymy powodzenia w przyszłej karierze!");
            Thread.Sleep(1500);
            adminMenu.Menu(this);
        }

        //zwiększa pensje pracownika
        public void IncreaseSalary()
        {
            Console.Clear();
            Console.WriteLine("///////////////////////////////////////////");
            Console.WriteLine("      Prosimy o podanie informacji owego pracownika      ");
            Console.WriteLine("///////////////////////////////////////////");
            Console.WriteLine("         Wpisz 'esc' by anulować         ");
            Console.WriteLine("///////////////////////////////////////////");
            Console.WriteLine();
            Console.Write("Podaj pracownika: ");

            //sprawdzanie czy użytkownik nie klika poprostu entera, lub czy też nazwa nie jest zajęta
            string? username = Console.ReadLine();
            while (string.IsNullOrEmpty(username) || isusername(username))
            {
                if (username.ToLower() == "esc")
                {
                    Console.WriteLine("Anulowano zwiększenia pensji!");
                    Thread.Sleep(1500);
                    adminMenu.Menu(this);
                    return;
                }
                Console.Write("\nPusto, lub nie ma takiego użytkownika: ");
                username = Console.ReadLine();
            }
            Console.Write("\nHasło pracownika: ");

            //sprawdzanie czy użytkownik nie klika poprostu entera, lub czy też hasło nie jest za słabe
            string? password = Console.ReadLine();
            while (string.IsNullOrEmpty(password) || ispassword(username, password))
            {
                if (password.ToLower() == "esc")
                {
                    Console.WriteLine("Anulowano zwiększania pensji!");
                    Thread.Sleep(1500);
                    adminMenu.Menu(this);
                    return;
                }
                Console.Write("\nPusto, lub użytkownik nie ma takiego hasła: ");
                password = Console.ReadLine();
            }

            int salary;
            while (true)
            {
                Console.Write("\nJak bardzo pensja się zwiększa?: ");
                string? input = Console.ReadLine();

                if (input != null && input.ToLower() == "esc")
                {
                    Console.WriteLine("Anulowano zwiększania pensji!");
                    Thread.Sleep(1500);
                    adminMenu.Menu(this);
                    return;
                }

                if (int.TryParse(input, out salary))
                {
                    break; //wychodzi z pętli jak jest faktyczny udany parse
                }

                Console.WriteLine("Nieprawidłowa wartość, spróbuj ponownie.");
            }
            DB.IncreaseSalary(username, password, salary);
            Console.WriteLine($"Zwiększono pensje pracownika {username}!");
            Thread.Sleep(1500);
            adminMenu.Menu(this);
        }

        //zmniejsza pensje pracownika
        public void DecreaseSalary()
        {
            Console.Clear();
            Console.WriteLine("///////////////////////////////////////////");
            Console.WriteLine("      Prosimy o podanie informacji owego pracownika      ");
            Console.WriteLine("///////////////////////////////////////////");
            Console.WriteLine("         Wpisz 'esc' by anulować         ");
            Console.WriteLine("///////////////////////////////////////////");
            Console.WriteLine();
            Console.Write("Podaj pracownika: ");

            //sprawdzanie czy użytkownik nie klika poprostu entera, lub czy też nazwa nie jest zajęta
            string? username = Console.ReadLine();
            while (string.IsNullOrEmpty(username) || isusername(username))
            {
                if (username.ToLower() == "esc")
                {
                    Console.WriteLine("Anulowano zmniejszania pensji!");
                    Thread.Sleep(1500);
                    adminMenu.Menu(this);
                    return;
                }
                Console.Write("\nPusto, lub nie ma takiego użytkownika: ");
                username = Console.ReadLine();
            }
            Console.Write("\nHasło pracownika: ");

            //sprawdzanie czy użytkownik nie klika poprostu entera, lub czy też hasło nie jest za słabe
            string? password = Console.ReadLine();
            while (string.IsNullOrEmpty(password) || ispassword(username, password))
            {
                if (password.ToLower() == "esc")
                {
                    Console.WriteLine("Anulowano zmiejszania pensji!");
                    Thread.Sleep(1500);
                    adminMenu.Menu(this);
                    return;
                }
                Console.Write("\nPusto, lub użytkownik nie ma takiego hasła: ");
                password = Console.ReadLine();
            }

            int salary;
            while (true)
            {
                Console.Write("\nJak bardzo pensja się zmniejsza?: ");
                string? input = Console.ReadLine();

                if (input != null && input.ToLower() == "esc")
                {
                    Console.WriteLine("Anulowano zmiejszania pensji!");
                    Thread.Sleep(1500);
                    adminMenu.Menu(this);
                    return;
                }

                if (int.TryParse(input, out salary))
                {
                    break; //wychodzi z pętli jak jest faktyczny udany parse
                }

                Console.WriteLine("Nieprawidłowa wartość, spróbuj ponownie.");
            }
            DB.DecreaseSalary(username, password, salary);
            Console.WriteLine($"Zmniejszono pensje pracownika {username}!");
            Thread.Sleep(1500);
            adminMenu.Menu(this);
        }

        //sprawdza czy nazwa użytkownika nie jest zajęta
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

        //sprawdza czy hasło nie pasuje do Regex
        public static bool notmatches(string password)
        {
            Regex regex = new Regex(@"^(?=.*\d)(?=.*[A-Z])(?=.*[!@#$%^&*()_+={}\[\]:;\'<>,.?/\\|`~-]).{5,}$");
            if (regex.IsMatch(password))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //sprawdza czy użytkownik istnieje
        public static bool isusername(string username)
        {
            Database db = new Database();
            List<Client> clients = db.GetClients();
            List<Worker> workers = db.GetWorkers();
            List<Admin> admins = db.GetAdmins();
            if (clients.Any(c => c.Username == username) || workers.Any(w => w.Username == username) || admins.Any(a => a.Username == username))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //sprawdza czy hasło jest poprawne
        public static bool ispassword(string username, string password)
        {
            Database db = new Database();
            List<Client> clients = db.GetClients();
            List<Worker> workers = db.GetWorkers();
            List<Admin> admins = db.GetAdmins();
            if (clients.Any(c => c.Username == username && c.Password == password) || workers.Any(w => w.Username == username && w.Password == password) || admins.Any(a => a.Username == username && a.Password == password))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}