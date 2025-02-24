using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using Org.BouncyCastle.Asn1.X509;
using Practis6_12.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для TakeOrderWindow.xaml
    /// </summary>
    public partial class TakeOrderWindow : Window
    {
        DB ctx = new DB();
        public ObservableCollection<OrderDisplayModel> Orders { get; set; } = new ObservableCollection<OrderDisplayModel>();

        public TakeOrderWindow(OrderDisplayModel order)
        {

            DataContext = this;
            
            InitializeComponent();

            ClientPhone.Text = order.Client_phone;
            StartLocation.Text = order.Pickup_location;
            EndLocation.Text = order.Dropoff_location;

        }



        private void EndOrder(object sender, RoutedEventArgs e)
        {
            string query = @"
        UPDATE orders SET status =4 , completed_at = current_timestamp() WHERE pickup_location = @Pickup_location and client_phone = @Client_phone and dropoff_location = @Dropoff_location ";

            MySqlParameter[] parameters = {
                new MySqlParameter("@Pickup_location", StartLocation.Text),
            new MySqlParameter("@Client_phone", ClientPhone.Text),
            new MySqlParameter("@Dropoff_location", EndLocation.Text)
                    };

            int insert = ctx.ExecuteNonQuery(query, parameters);
            Close();


        }
    }
}
