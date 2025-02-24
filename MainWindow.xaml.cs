using System.Data;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using MySqlX.XDevAPI.Common;
using Practis6_12.Windows;
namespace Practis6_12
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DB ctx = new DB();
        public MainWindow()
        {
            InitializeComponent();

            ////string password = "Operatorov-AdminTest1";
            //string password = "Taxistov";
            //string password_hash = PasswordHasher.HashPassword(password);

            //string query = "INSERT INTO `users` (`full_name`, `login`, `phone`, `email`, `password_hash`, `role`, `status`, `created_at`) " +
            //             "VALUES (@FullName, @Login, @Phone, @Email, @PasswordHash, @Role, @Status, CURRENT_TIMESTAMP())";

            //MySqlParameter[] parameters = {
            //new MySqlParameter("@FullName", "Таксистов Быстр Иванович"),
            //new MySqlParameter("@Login", "Taxistov"),
            //new MySqlParameter("@Phone", "+71771236574"),
            //new MySqlParameter("@Email", "Taxistov@gmail.com"),
            //new MySqlParameter("@PasswordHash", password_hash),
            //new MySqlParameter("@Role", "Driver"),
            //new MySqlParameter("@Status", "active")
            //};

            //int insert = ctx.ExecuteNonQuery(query, parameters);

            //if (insert > 0)
            //    Debug.WriteLine("Пользователь успешно добавлен!");
            //else
            //    Debug.WriteLine("Ошибка при добавлении пользователя.");
        }

        private void Autorization_Click(object sender, RoutedEventArgs e)
        {

            string login = Login.Text;
            string password = Password.Password;
            string role;
            int user_id;
            string password_hash = PasswordHasher.HashPassword(password);

            string query = "SELECT role,user_id FROM users WHERE login = @Login  AND password_hash = @PasswordHash";

            MySqlParameter[] parameters = {
            new MySqlParameter("@Login", login),
            new MySqlParameter("@PasswordHash", password_hash),
            };

            using (MySqlDataReader reader = ctx.ExecuteReader(query, parameters))
            {
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        Debug.WriteLine("Success");
                        reader.Read();
                        role = reader.GetString("role");
                        user_id = reader.GetInt16("user_id");

                        //while (reader.Read()) // reader.Read() возвращает true и переходит к следующему ряду.
                        //{
                        //    name = reader.GetString("Name");
                        //    email = reader.GetString("Email");
                        //}

                        switch (role)
                        {

                            case "Admin":
                                   
                                {
                                    Debug.WriteLine("Admin");

                                    AdminWindow adminWindow = new AdminWindow();
                                    adminWindow.Show();
                                    Close();
                                    break;
                                }
                            case "Dispatcher":
                                {
                                    DispatcherWindow dispatcherWindow = new DispatcherWindow(user_id);
                                    dispatcherWindow.Show();
                                    Close();
                                    Debug.WriteLine("Dispatcher");
                                    break;
                                }
                            case "Driver":
                                {
                                    DriverWindow driverWindow = new DriverWindow(user_id);
                                    driverWindow.Show();
                                    Close();
                                    Debug.WriteLine("Driver");
                                    break;
                                }
                        }

                    }
                    else MessageBox.Show("Неправильный логин или пароль");
                }

            }
            Debug.WriteLine("End");

        }


    }
}