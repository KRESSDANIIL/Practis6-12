using MySql.Data.MySqlClient;
using System.Diagnostics;

public class DB
{
    private MySqlConnection connection;

    // ����������� ������ DB
    //public DB(string server, string userId, string password, string database)
    //{
    //    // �������� ������ �����������
    //    MySqlConnectionStringBuilder stringBuilder = new MySqlConnectionStringBuilder
    //    {
    //        Server = server,
    //        UserID = userId,
    //        Password = password,
    //        Database = database,
    //    };

    //    // ������������� �����������
    //    connection = new MySqlConnection(stringBuilder.ToString());
    //}

    public DB()
    {
        // �������� ������ �����������
        MySqlConnectionStringBuilder stringBuilder = new MySqlConnectionStringBuilder
        {
            Server = "localhost",
            UserID = "root",
            Password = "",
            Database = "practics",
        };

        // ������������� �����������
        connection = new MySqlConnection(stringBuilder.ToString());
    }

    // ����� ��� �������� ����������
    public void OpenConnection()
    {
        if (connection.State == System.Data.ConnectionState.Closed)
        {
            connection.Open();
            Console.WriteLine("���������� � ����� ������ �������.");
        }
    }

    // ����� ��� �������� ����������
    public void CloseConnection()
    {
        if (connection.State == System.Data.ConnectionState.Open)
        {
            connection.Close();
            Console.WriteLine("���������� � ����� ������ �������.");
        }
    }

    // ����� ��� ���������� SQL-������� ��� �������� ������ (INSERT, UPDATE, DELETE)
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
            Debug.WriteLine("������ ��� ���������� �������: " + ex.Message);
        }
        finally
        {
            CloseConnection();
        }

        return result;
    }

    // ����� ��� ���������� SQL-������� � ��������� ������ (SELECT)
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
            Debug.WriteLine("������ ��� ���������� �������: " + ex.Message);
        }

        return reader;
    }

    // ����� ��� ��������� ���������� �������� (��������, COUNT, SUM)
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
            Console.WriteLine("������ ��� ���������� �������: " + ex.Message);
        }
        finally
        {
            CloseConnection();
        }

        return result;
    }
}