using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
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
    /// Логика взаимодействия для ShiftCreatWindow.xaml
    /// </summary>
    public partial class ShiftCreatWindow : Window
    {
        DB ctx = new DB();
        int User_Id;

        DateTime StartDate;
        DateTime EndDate;
        public ShiftCreatWindow()
        {
            InitializeComponent();
        }

        private void CreateShift(object sender, RoutedEventArgs e)
        {
           string query = @"SELECT user_id 
                    FROM users 
                    WHERE 
                full_name = @Full_name";

            MySqlParameter[] parameters = {
            new MySqlParameter("@Full_name", FullNameTextBox.Text)
            };


            using (MySqlDataReader reader = ctx.ExecuteReader(query, parameters))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        User_Id = reader.GetInt32(0);

                    }
                }
            }


            query = "INSERT INTO shifts (`user_id`, `start_time`, `end_time` ) " +
                        "VALUES (@User_id, @Start_time, @End_time)";

            if (StartDatePicker.SelectedDate.HasValue && StartTimePicker.SelectedItem is ComboBoxItem timeItem)
            {
                string timeString = timeItem.Content.ToString();
                DateTime selectedDate = StartDatePicker.SelectedDate.Value;
                StartDate = DateTime.ParseExact($"{selectedDate:yyyy-MM-dd} {timeString}", "yyyy-MM-dd HH:mm", null);

            }
            else MessageBox.Show("Ошибка", "Действие", MessageBoxButton.OK, MessageBoxImage.Error);
            if (EndDatePicker.SelectedDate.HasValue && EndTimePicker.SelectedItem is ComboBoxItem timeItem2)
            {
                string timeString = timeItem2.Content.ToString();
                DateTime selectedDate = StartDatePicker.SelectedDate.Value;
                EndDate = DateTime.ParseExact($"{selectedDate:yyyy-MM-dd} {timeString}", "yyyy-MM-dd HH:mm", null);

            }
            else MessageBox.Show("Ошибка", "Действие", MessageBoxButton.OK, MessageBoxImage.Error);

            Debug.WriteLine(User_Id);
            MySqlParameter[] parameters2 = {
                            new MySqlParameter("@User_id",  User_Id),
                            new MySqlParameter("@Start_time", StartDate),
                            new MySqlParameter("@End_time", EndDate )
            };

            try
            {
                int insert = ctx.ExecuteNonQuery(query, parameters2);
                if (insert > 0)
                    MessageBox.Show("Данные успешно занесены!", "Действие", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("Ошибка", "Действие", MessageBoxButton.OK, MessageBoxImage.Error);
            } catch (Exception ex)
            {
                
                    MessageBox.Show("Ошибка загрузки : " + ex.Message);
                
            }
        }
    }
}
