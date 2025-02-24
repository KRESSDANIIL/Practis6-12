using MySql.Data.MySqlClient;
using System.Diagnostics;

public class DB
{
    private MySqlConnection connection;

    // Конструктор класса DB
    //public DB(string server, string userId, string password, string database)
    //{
    //    // Создание строки подключения
    //    MySqlConnectionStringBuilder stringBuilder = new MySqlConnectionStringBuilder
    //    {
    //        Server = server,
    //        UserID = userId,
    //        Password = password,
    //        Database = database,
    //    };

    //    // Инициализация подключения
    //    connection = new MySqlConnection(stringBuilder.ToString());
    //}

    public DB()
    {
        // Создание строки подключения
        MySqlConnectionStringBuilder stringBuilder = new MySqlConnectionStringBuilder
        {
            Server = "localhost",
            UserID = "root",
            Password = "",
            Database = "practics",
        };

        // Инициализация подключения
        connection = new MySqlConnection(stringBuilder.ToString());
    }

    // Метод для открытия соединения
    public void OpenConnection()
    {
        if (connection.State == System.Data.ConnectionState.Closed)
        {
            connection.Open();
            Console.WriteLine("Соединение с базой данных открыто.");
        }
    }

    // Метод для закрытия соединения
    public void CloseConnection()
    {
        if (connection.State == System.Data.ConnectionState.Open)
        {
            connection.Close();
            Console.WriteLine("Соединение с базой данных закрыто.");
        }
    }

    // Метод для выполнения SQL-запроса без возврата данных (INSERT, UPDATE, DELETE)
    public int ExecuteNonQuery(string query, MySqlParameter[] parameters = null)
    {
        int result = 0;

        try
        {
            OpenConnection();

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                result = command.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Ошибка при выполнении запроса: " + ex.Message);
        }
        finally
        {
            CloseConnection();
        }

        return result;
    }

    // Метод для выполнения SQL-запроса с возвратом данных (SELECT)
    public MySqlDataReader ExecuteReader(string query, MySqlParameter[] parameters = null)
    {
        MySqlDataReader reader = null;

        try
        {
            OpenConnection();

            MySqlCommand command = new MySqlCommand(query, connection);

            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }

            reader = command.ExecuteReader();
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Ошибка при выполнении запроса: " + ex.Message);
        }

        return reader;
    }

    // Метод для получения скалярного значения (например, COUNT, SUM)
    public object ExecuteScalar(string query, MySqlParameter[] parameters = null)
    {
        object result = null;

        try
        {
            OpenConnection();

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                result = command.ExecuteScalar();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка при выполнении запроса: " + ex.Message);
        }
        finally
        {
            CloseConnection();
        }

        return result;
    }
}