using MySql.Data.MySqlClient;
using Practis6_12.Models;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для OrderCreatWindow.xaml
    /// </summary>
    public partial class OrderCreatWindow : Window
    {
        DB ctx = new DB();

        int UserId;

        public OrderCreatWindow(int userId)
        {
            InitializeComponent();
            UserId = userId;
        }




        private void CreateOrder(object sender, RoutedEventArgs e)
        {
            if (Client_Phone.Text != null && Client_Phone.Text != null && Pickup_Location.Text != null && Dropoff_Location.Text != null && Fare.Text !=null && Discount_Applied.Text != null) {
                string query = "INSERT INTO orders (`dispatcher_id`, `client_phone`, `pickup_location`, `dropoff_location` , `fare`, `discount_applied` ) " +
                            "VALUES (@Dispatcher_id, @Client_phone, @Pickup_location, @Dropoff_Location @Fare, @Discount_applied)";



                MySqlParameter[] parameters = {
            new MySqlParameter("@Dispatcher_id", UserId),
            new MySqlParameter("@Client_phone", Client_Phone.Text),
            new MySqlParameter("@Pickup_location", Pickup_Location.Text),
            new MySqlParameter("@Dropoff_Location", Dropoff_Location.Text),
            new MySqlParameter("@Fare" , Fare.Text),
            new MySqlParameter("@Discount_applied" , Discount_Applied.Text)

            };
                Debug.WriteLine(UserId);

                int insert = ctx.ExecuteNonQuery(query, parameters);
                if (insert > 0)
                {
                    MessageBox.Show("Данные успешно занесены!", "Действие", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
                else
                    MessageBox.Show("Ошибка", "Действие", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else MessageBox.Show("Ошибка нет данных в полях", "Действие", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
