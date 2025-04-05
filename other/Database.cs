using System;
using MySqlConnector;
using Pizzeria.users;
namespace Pizzeria.database
{
    public class Database
    {
        //dane do łączenia sie z bazą
        private readonly string connectionString = "Server=localhost;Database=pizzeria;User=root;Password=;";

        public MySqlConnection Conn { get; set; }

        //przy tworzeniu nowej bazy automatycznie sie łączy i otwiera
        public Database()
        {
            try
            {
                Conn = new MySqlConnection(connectionString);
                Conn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd połączenia z bazą: {ex.Message}");
            }
        }

        //tutaj będą metody związane z bazami danych
        public List<Worker> GetWorkers()
        {
            List<Worker> workers = new List<Worker>();

            string query = "SELECT * FROM pracownicy";
            MySqlCommand result = new MySqlCommand(query, Conn);
            MySqlDataReader row = result.ExecuteReader();
            while (row.Read())
            {
                Worker worker = new Worker(row["Nazwa"].ToString(), row["Haslo"].ToString());
                worker.UserRole = User.Role.Worker;
                workers.Add(worker);
            }
            row.Close();
            return workers;
        }

        public List<Client> GetClients()
        {
            List<Client> clients = new List<Client>();

            string query = "SELECT * FROM klienci";
            MySqlCommand result = new MySqlCommand(query, Conn);
            MySqlDataReader row = result.ExecuteReader();
            while (row.Read())
            {
                Client client = new Client(row["Nazwa"].ToString(), row["Haslo"].ToString());
                client.UserRole = User.Role.Client;
                clients.Add(client);
            }
            row.Close();
            return clients;
        }

        public List<Admin> GetAdmins()
        {
            List<Admin> admins = new List<Admin>();

            string query = "SELECT * FROM admini";
            MySqlCommand result = new MySqlCommand(query, Conn);
            MySqlDataReader row = result.ExecuteReader();
            while (row.Read())
            {
                Admin admin = new Admin(row["Nazwa"].ToString(), row["Haslo"].ToString());
                admin.UserRole = User.Role.Admin;
                admins.Add(admin);
            }
            row.Close();
            return admins;
        }

        public void AddClient(string username, string password)
        {
            string query = $"INSERT INTO klienci (ID, Nazwa, Haslo) VALUES (NULL, '{username}', '{password}')";
            MySqlCommand result = new MySqlCommand(query, Conn);
            result.ExecuteNonQuery();
        }
    }
}