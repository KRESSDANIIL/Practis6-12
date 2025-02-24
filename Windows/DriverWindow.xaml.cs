using MySql.Data.MySqlClient;
using Practis6_12.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZstdSharp.Unsafe;

namespace Practis6_12.Windows
{
    /// <summary>
    /// Логика взаимодействия для DriverWindow.xaml
    /// </summary>
    public partial class DriverWindow : Window
    {
        DB ctx = new DB();
        int User_id;
        public ObservableCollection<OrderDisplayModel> Orders { get; set; } = new ObservableCollection<OrderDisplayModel>();

        public DriverWindow(int user_id)
        {
            InitializeComponent();

            User_id = user_id;

            DataContext = this;

            LoadOrders();

        }

        public void LoadOrders()
        {
            Orders.Clear();

            //string query = @"SELECT *  FROM drivers;";

            string query = @"SELECT * FROM orders Where status = 1";




            using (MySqlDataReader reader = ctx.ExecuteReader(query))
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        var orders = new OrderDisplayModel
                        {
                            Order_id = reader.GetInt32(0),
                            Dispatcher_id = reader.GetInt32(1),
                            Driver_id = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2),
                            Client_phone = reader.GetString(3),
                            Pickup_location = reader.GetString(4),
                            Dropoff_location = reader.GetString(5),
                            Status = reader.GetString(6),
                            Fare = reader.GetDecimal(7),
                            Discount_applied = reader.GetBoolean(8),
                            Created_at = reader.GetDateTime(9),
                            Completed_at = reader.GetDateTime(10),

                        };
                        Orders.Add(orders);
                    }
                }
                else Debug.WriteLine("deadEnd");

            }
        }

        private void Button_Click_Orders(object sender, RoutedEventArgs e)
        {
            LoadOrders();
        }

        private void SaveCar(object sender, RoutedEventArgs e)
        {
            string query = "SELECT * FROM `cars` WHERE `driver_id` =@Driver_id";

            MySqlParameter[] parameters = {
            new MySqlParameter("@Driver_id", User_id),

            };
            using (MySqlDataReader reader = ctx.ExecuteReader(query, parameters))
            {
                if (reader != null)
                {
                    MessageBox.Show("Ошибка. У вас уже зарегестрированная машина", "Действие", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    query = "INSERT INTO cars (`driver_id`, `model`, `plate_number`, `year`) " +
                            "VALUES (@Driver_id, @Model, @Plate_number, @Year)";



                    MySqlParameter[] parameters2 = {
            new MySqlParameter("@Driver_id", User_id),
            new MySqlParameter("@Model", ModelNameTextBox.Text),
            new MySqlParameter("@Plate_number", PlateNumberTextBox.Text),
            new MySqlParameter("@Year", YearTextBox.Text)
                    };
                    Debug.WriteLine(User_id);

                    int insert = ctx.ExecuteNonQuery(query, parameters2);
                    if (insert > 0)
                    {
                        MessageBox.Show("Данные успешно занесены!", "Действие", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                        MessageBox.Show("Ошибка", "Действие", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void SelectDriverOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is OrderDisplayModel order)
            {
                string query = @"
            UPDATE orders
        SET status =2, driver_id = @Driver_id
        WHERE 
            client_phone = @Client_phone AND created_at = @Created_at ";

                MySqlParameter[] parameters = {
            new MySqlParameter("@Driver_id", User_id),
            new MySqlParameter("@Client_phone", order.Client_phone),
            new MySqlParameter("@Created_at", order.Created_at)
                    };

                int insert = ctx.ExecuteNonQuery(query, parameters);
                TakeOrderWindow takeOrderWindow = new TakeOrderWindow(order);
                takeOrderWindow.Show();
                Close();
               
            }
        }

    }
}
