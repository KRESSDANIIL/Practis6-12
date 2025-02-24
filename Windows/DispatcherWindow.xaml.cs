using Microsoft.Win32;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using Org.BouncyCastle.Utilities;
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

namespace Practis6_12.Windows
{
    /// <summary>
    /// Логика взаимодействия для DispatcherWindow.xaml
    /// </summary>
    public partial class DispatcherWindow : Window
    {
        DB ctx = new DB();
        public ObservableCollection<DriverDisplayModel> Drivers { get; set; } = new ObservableCollection<DriverDisplayModel>();

        public ObservableCollection<OrderDisplayModel> Orders { get; set; } = new ObservableCollection<OrderDisplayModel>();

        public ObservableCollection<ShiftDisplayModel> Shifts { get; set; } = new ObservableCollection<ShiftDisplayModel>();

        string display_model_photo_path;

        int UserId;

        public DispatcherWindow(int user_id)
        {
            InitializeComponent();

            LoadDrivers();
            LoadOrders();
            LoadShifts();
            DataContext = this;

            UserId = user_id;

        }


        public void LoadDrivers()
        {
            Drivers.Clear();

            //string query = @"SELECT *  FROM drivers;";

            string query = @"SELECT 
                d.driver_id,
                u.full_name,
                u.phone,
                u.email,
                u.photo,
                u.employment_contract_scan,
                d.rating,
                d.driver_type,
                c.model,
                c.plate_number,
                u.login
                FROM drivers d
                JOIN users u ON d.driver_id = u.user_id
                JOIN cars c ON d.driver_id = c.driver_id Where u.status = 1;";




            using (MySqlDataReader reader = ctx.ExecuteReader(query))
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        var drivers = new DriverDisplayModel
                        {
                            UserId = reader.GetInt32(0),
                            FullName = reader.GetString(1),
                            Phone = reader.GetString(2),
                            Email = reader.GetString(3),
                            Photo = reader.IsDBNull(4) ? null : (byte[])reader[4],
                            EmploymentContractScan = reader.IsDBNull(5) ? null : (byte[])reader[5],
                            Rating = reader.GetFloat(6),
                            Driver_type = reader.GetString(7),
                            CarModel = reader.GetString(8),
                            CarNumber = reader.GetString(9),
                            Login = reader.GetString(10)

                        };
                        Drivers.Add(drivers);
                    }
                }
                else Debug.WriteLine("deadEnd");

            }
        }

        public void LoadOrders()
        {
            Orders.Clear();

            //string query = @"SELECT *  FROM drivers;";

            string query = @"SELECT * FROM orders";




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

        public void LoadShifts()
        {
            Shifts.Clear();

            //string query = @"SELECT *  FROM drivers;";

            string query = @"SELECT
            s.shift_id,
            s.user_id,
            s.start_time,
            s.end_time,
            s.photo,
            u.full_name
            FROM shifts s
            JOIN users u ON s.user_id = u.user_id;";



            using (MySqlDataReader reader = ctx.ExecuteReader(query))
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        var shifts = new ShiftDisplayModel
                        {
                            Shift_id = reader.GetInt32(0),
                            User_id = reader.GetInt32(1),
                            start_time = reader.GetDateTime(2),
                            End_time = reader.GetDateTime(3),
                            Photo = reader.IsDBNull(4) ? null : (byte[])reader[4],
                            FullName = reader.GetString(5),

                        };
                        Shifts.Add(shifts);
                    }
                }
                else Debug.WriteLine("deadEnd");

            }
        }


        private void Button_Click_Drivers(object sender, RoutedEventArgs e)
        {
            LoadDrivers();
        }

        private void Button_Click_Shifts(object sender, RoutedEventArgs e)
        {
            LoadShifts();
        }

        private void Button_Click_Orders(object sender, RoutedEventArgs e)
        {
            LoadOrders();
        }

        private void SelectDispatcherButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is ShiftDisplayModel dispatcher)
            {
                if (dispatcher.Photo == null)
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                    if (openFileDialog.ShowDialog() == true)
                    {
                        BitmapImage bitmap = new BitmapImage(new Uri(openFileDialog.FileName));
                         display_model_photo_path = openFileDialog.FileName;
                    }

                    string query = @"
        UPDATE shifts
        SET 
            photo = @Photo
        WHERE 
            shift_id = @Shift_id";

                    byte[] Photo = ImageConverter.ConvertImageToByteArray(display_model_photo_path);

                    

                    MySqlParameter[] parameters = {
                            new MySqlParameter("@Photo", Photo),
                            new MySqlParameter("@Shift_id", dispatcher.Shift_id)
                };
                    Debug.WriteLine(dispatcher.Shift_id);
                    Debug.WriteLine(display_model_photo_path);
                    int insert = ctx.ExecuteNonQuery(query, parameters);
                    if (insert > 0)
                        MessageBox.Show("Данные успешно занесены!", "Действие", MessageBoxButton.OK, MessageBoxImage.Information);
                    else
                        MessageBox.Show("Ошибка", "Действие", MessageBoxButton.OK, MessageBoxImage.Error);
                    LoadShifts();
                }
                else MessageBox.Show("Уже имеет фото", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }   
        }

        private void Button_Click_Creat_Orders(object sender, RoutedEventArgs e)
        {
            OrderCreatWindow orderCreatWindow = new OrderCreatWindow(UserId);
            orderCreatWindow.Owner = this;
            orderCreatWindow.Show();
        }
    }

}

