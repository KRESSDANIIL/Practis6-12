using Practis6_12.Models;
using System;
using System.IO;
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
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Windows.Controls.Primitives;
using Org.BouncyCastle.Asn1.X509;
using System.Data;
using System.Windows.Threading;
using Microsoft.Win32;
using System.Reflection.PortableExecutable;

namespace Practis6_12.Windows
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        DB ctx = new DB();

        public ICommand SelectCommand { get; }

        public ObservableCollection<DispatcherDisplayModel> Dispatchers { get; set; } = new ObservableCollection<DispatcherDisplayModel>();

        public ObservableCollection<DriverDisplayModel> Drivers { get; set; } = new ObservableCollection<DriverDisplayModel>();

        public ObservableCollection<OrderDisplayModel> Orders { get; set; } = new ObservableCollection<OrderDisplayModel>();

        public ObservableCollection<ShiftDisplayModel> Shifts { get; set; } = new ObservableCollection<ShiftDisplayModel>();

        internal ObservableCollection<UserDisplayModel> Users { get; set; } = new ObservableCollection<UserDisplayModel> { };

        string user_display_model_photo_path;

        string user_display_model_contract_path;

        string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".bmp", ".gif" };

        public AdminWindow()
        {
            InitializeComponent();

            DataContext = this;

            CorrectDate.Content=DateTime.Now;


            LoadDispatchers();
            LoadDrivers();
            LoadOrders();
            LoadShifts();
        }

      

        public void LoadDispatchers()
        {    
            Dispatchers.Clear();

            string query = @"SELECT 
            user_id,
            full_name,
            login,
            phone,
            email,
            status,
            created_at,
            photo,
            employment_contract_scan
                FROM 
                    users
                WHERE 
                    role = 'Dispatcher';";


            using (MySqlDataReader reader = ctx.ExecuteReader(query))
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        var dispatcher = new DispatcherDisplayModel
                        {
                            UserId = reader.GetInt32(0),
                            FullName = reader.GetString(1),
                            Login = reader.GetString(2),
                            Phone = reader.GetString(3),
                            Email = reader.GetString(4),
                            Status = reader.GetString(5),
                            CreatedAt = reader.GetDateTime(6),
                            Photo = reader.IsDBNull(7) ? null : (byte[])reader[7],
                            EmploymentContractScan = reader.IsDBNull(8) ? null : (byte[])reader[8],
                        };
                        Dispatchers.Add(dispatcher);
                    }
                }
                else Debug.WriteLine("deadEnd");

            //if (insert > 0)
            //    Debug.WriteLine("Пользователь успешно добавлен!");
            //else
            //    Debug.WriteLine("Ошибка при добавлении пользователя.");



            }
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
                JOIN cars c ON d.driver_id = c.driver_id;";




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
                            Completed_at = reader.IsDBNull(10) ? (DateTime?)null : reader.GetDateTime(10)

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

        private void Button_Click_Operators(object sender, RoutedEventArgs e)
        {
            LoadDispatchers();
        }

        private void Button_Click_Drivers(object sender, RoutedEventArgs e)
        {
            LoadDrivers();
        }

        private void SelectDispatcherButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is DispatcherDisplayModel dispatcher)
            {
                EmployeePersonalCardWindow employeePersonalCardWindow = new EmployeePersonalCardWindow(dispatcher);
                employeePersonalCardWindow.Owner = this;
                employeePersonalCardWindow.Show();

                MessageBox.Show($"Вы выбрали: {dispatcher.FullName} ({dispatcher.Login})",
                                "Выбор оператора",
                                MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void SelectDriverButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is DriverDisplayModel driver)
            {
                EmployeeDriverPersonalCardWindow employeePersonalCardWindow = new EmployeeDriverPersonalCardWindow(driver);
                employeePersonalCardWindow.Owner = this;
                employeePersonalCardWindow.Show();

                MessageBox.Show($"Вы выбрали: {driver.FullName})",
                                "Выбор оператора",
                                MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Button_Click_Orders(object sender, RoutedEventArgs e)
        {
            LoadOrders();
        }

        private void Button_Click_Shifts(object sender, RoutedEventArgs e)
        {
            LoadShifts();
        }

        private void Discharge_Click(object sender, RoutedEventArgs e)
        {

            string Report_type = report_type_text_box.Text;


            string Content = content_text_box.Text;



            string query = "INSERT INTO reports (`report_type`, `report_date`, `content`) " +
                        "VALUES (@Report_type, CURRENT_TIMESTAMP(), @Content)";

            MySqlParameter[] parameters = {
            new MySqlParameter("@Report_type", Report_type),
            new MySqlParameter("@Content", Content)
            };

            int insert = ctx.ExecuteNonQuery(query, parameters);
            if (insert > 0)
                MessageBox.Show("Данные успешно занесены!" , "Действие", MessageBoxButton.OK, MessageBoxImage.Information) ;
            else
                MessageBox.Show("Ошибка", "Действие", MessageBoxButton.OK, MessageBoxImage.Error);

        }
        private void CreateEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            string query = "INSERT INTO users (`full_name`, `login`, `phone`, `email`, `password_hash`, `role`, `created_at`, `photo` , `employment_contract_scan` ) " +
                        "VALUES (@Full_name, @Login, @Phone, @Email, @Password_hash, @Role ,CURRENT_TIMESTAMP(), @Photo, @Employment_contract_scan)";


            byte[] Photo = ImageConverter.ConvertImageToByteArray(user_display_model_photo_path);
            byte[] Employment_contract_scan = ImageConverter.ConvertImageToByteArray(user_display_model_contract_path);
            string password = PasswordBox.Password;
            int Driver_id = 0;

            MySqlParameter[] parameters = {
            new MySqlParameter("@Full_name", FullNameTextBox.Text),
            new MySqlParameter("@Login", LoginTextBox.Text),
            new MySqlParameter("@Phone", PhoneTextBox.Text),
            new MySqlParameter("@Email", EmailTextBox.Text),
            new MySqlParameter("@Password_hash", PasswordHasher.HashPassword(password)),
            new MySqlParameter("@Role" , RoleComboBox.Text),
            new MySqlParameter("@Photo",  Photo),
            new MySqlParameter("@Employment_contract_scan", Employment_contract_scan)
            };

            int insert = ctx.ExecuteNonQuery(query, parameters);

            if (RoleComboBox.Text == "Driver")
            {
                query = @"SELECT user_id 
                    FROM users 
                    WHERE 
                full_name = @Full_name 
                AND phone = @Phone";

                //MySqlParameter[] parameters2 = {
                //new MySqlParameter("@Full_name", FullNameTextBox.Text),
                //new MySqlParameter("@Login", LoginTextBox.Text),
                //new MySqlParameter("@Phone", PhoneTextBox.Text),
                //new MySqlParameter("@Email", EmailTextBox.Text),
                //new MySqlParameter("@Password_hash", PasswordHasher.HashPassword(password)),
                //new MySqlParameter("@Role" , RoleComboBox.Text),
                //new MySqlParameter("@Photo",  Photo),
                //new MySqlParameter("@Employment_contract_scan", Employment_contract_scan)
                //};

                using (MySqlDataReader reader = ctx.ExecuteReader(query, parameters))
                {
                    if (reader != null)
                    {
                        if (reader.Read())
                        {
                            Driver_id = reader.GetInt32(0);

                        }
                    }


                }

                query = "INSERT INTO drivers (`driver_id`, `rating` ) " +
                        "VALUES (@Driver_id, @Rating)";


                MySqlParameter[] parameters2 = {
                            new MySqlParameter("@Driver_id", Driver_id),
                            new MySqlParameter("@Rating", 4)
                            };

                insert = ctx.ExecuteNonQuery(query, parameters2);

            }

        }
        

        private void Photo_Drop(object sender, DragEventArgs e)
        {
            HandleFileDropPhoto(e, PhotoPreview);
        }

        private void Contract_Drop(object sender, DragEventArgs e)
        {
            HandleFileDropContract(e, ContractPreview);
        }

        // Обработка кнопки "Загрузить фото"
        private void UploadPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            if (openFileDialog.ShowDialog() == true)
            {
                BitmapImage bitmap = new BitmapImage(new Uri(openFileDialog.FileName));
                PhotoPreview.Source = bitmap;
                user_display_model_photo_path = openFileDialog.FileName;
            }
        }

        // Обработка кнопки "Загрузить скан"
        private void UploadContractButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            if (openFileDialog.ShowDialog() == true)
            {
                BitmapImage bitmap = new BitmapImage(new Uri(openFileDialog.FileName));
                ContractPreview.Source = bitmap;
                user_display_model_contract_path = openFileDialog.FileName;
            }
        }

        private void HandleFileDropPhoto(DragEventArgs e, Image imageControl)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Получаем массив перетаскиваемых файлов
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                // Берем первый файл (если их несколько)
                string filePath = files[0];

                user_display_model_photo_path = filePath;

                // Проверяем, что файл является изображением
                if (IsImageFile(filePath))
                {
                    // Загружаем изображение в Image
                    BitmapImage bitmap = new BitmapImage(new Uri(filePath));
                    imageControl.Source = bitmap;
                }
                else
                {
                    MessageBox.Show("Пожалуйста, перетащите файл изображения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void HandleFileDropContract(DragEventArgs e, Image imageControl)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Получаем массив перетаскиваемых файлов
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                // Берем первый файл (если их несколько)
                string filePath = files[0];

                user_display_model_contract_path = filePath;

                // Проверяем, что файл является изображением
                if (IsImageFile(filePath))
                {
                    // Загружаем изображение в Image
                    BitmapImage bitmap = new BitmapImage(new Uri(filePath));
                    imageControl.Source = bitmap;
                }
                else
                {
                    MessageBox.Show("Пожалуйста, перетащите файл изображения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private bool IsImageFile(string filePath)
        {
            string extension = System.IO.Path.GetExtension(filePath).ToLower();
            return imageExtensions.Contains(extension);
        }

        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////////
 
        private void Border_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effects = DragDropEffects.Copy;
            else
                e.Effects = DragDropEffects.None;
        }

        private void Window_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effects = DragDropEffects.Copy;
            else
                e.Effects = DragDropEffects.None;
        }

        private void Window_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effects = DragDropEffects.Copy;
            else
                e.Effects = DragDropEffects.None;
        }

        private void Button_Click_Creat(object sender, RoutedEventArgs e)
        {
            ShiftCreatWindow shiftCreatWindow = new ShiftCreatWindow();
            shiftCreatWindow.Owner = this;
            shiftCreatWindow.Show();

        }
    }
}