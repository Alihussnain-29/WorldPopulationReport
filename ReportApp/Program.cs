using System;
using MySql.Data.MySqlClient;

class Program
{
    static MySqlConnection connection;

    static void Main(string[] args)
    {
        string connString = "server=localhost;user=root;password=root;database=world";

        connection = new MySqlConnection(connString);

        try
        {
            connection.Open();
            Console.WriteLine("Connected to MySQL successfully.\n");

            bool running = true;

            while (running)
            {
                Console.WriteLine("===== World Population Reporting System =====");
                Console.WriteLine("1. View Top 10 Countries by Population");
                Console.WriteLine("2. View Top 10 Cities by Population");
                Console.WriteLine("3. View Top 10 Capital Cities");
                Console.WriteLine("4. View Top N Countries");
                Console.WriteLine("5. Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        ShowTopCountries();
                        break;

                    case "2":
                        ShowTopCities();
                        break;

                    case "3":
                        ShowTopCapitals();
                        break;

                    case "4":
                        ShowTopNCountries();
                        break;

                    case "5":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid option.\n");
                        break;
                }
            }

            connection.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error connecting to database: " + ex.Message);
        }
    }

    static void ShowTopCountries()
    {
        string query = "SELECT Name, Population FROM country ORDER BY Population DESC LIMIT 10";

        MySqlCommand cmd = new MySqlCommand(query, connection);
        MySqlDataReader reader = cmd.ExecuteReader();

        Console.WriteLine("Top 10 Countries by Population:\n");

        while (reader.Read())
        {
            Console.WriteLine(reader["Name"] + " - " + reader["Population"]);
        }

        Console.WriteLine();
        reader.Close();
    }

    static void ShowTopCities()
    {
        string query = "SELECT Name, Population FROM city ORDER BY Population DESC LIMIT 10";

        MySqlCommand cmd = new MySqlCommand(query, connection);
        MySqlDataReader reader = cmd.ExecuteReader();

        Console.WriteLine("Top 10 Cities by Population:\n");

        while (reader.Read())
        {
            Console.WriteLine(reader["Name"] + " - " + reader["Population"]);
        }

        Console.WriteLine();
        reader.Close();
    }

    static void ShowTopCapitals()
    {
        string query = @"SELECT city.Name, city.Population
                         FROM city
                         JOIN country ON city.ID = country.Capital
                         ORDER BY city.Population DESC
                         LIMIT 10";

        MySqlCommand cmd = new MySqlCommand(query, connection);
        MySqlDataReader reader = cmd.ExecuteReader();

        Console.WriteLine("Top 10 Capital Cities:\n");

        while (reader.Read())
        {
            Console.WriteLine(reader["Name"] + " - " + reader["Population"]);
        }

        Console.WriteLine();
        reader.Close();
    }

    static void ShowTopNCountries()
    {
        Console.Write("Enter number (N): ");
        int n = Convert.ToInt32(Console.ReadLine());

        string query = $"SELECT Name, Population FROM country ORDER BY Population DESC LIMIT {n}";

        MySqlCommand cmd = new MySqlCommand(query, connection);
        MySqlDataReader reader = cmd.ExecuteReader();

        Console.WriteLine($"\nTop {n} Countries by Population:\n");

        while (reader.Read())
        {
            Console.WriteLine(reader["Name"] + " - " + reader["Population"]);
        }

        Console.WriteLine();
        reader.Close();
    }
}
