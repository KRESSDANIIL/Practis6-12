using Microsoft.Win32;
using MySql.Data.MySqlClient;
using Practis6_12.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Threading;

namespace Practis6_12.Windows
{
    /// <summary>
    /// Логика взаимодействия для EmployeeDriverPersonalCardWindow.xaml
    /// </summary>
    public partial class EmployeeDriverPersonalCardWindow : Window
    {
        DB ctx = new DB();

        string display_model_photo_path;

        string display_model_contract_path;

        string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".bmp", ".gif" };

        int EmployeeId;
        BitmapImage PhotoSouce;
        BitmapImage ContractScanSource;

        public DriverDisplayModel Driver { get; set; }
        public EmployeeDriverPersonalCardWindow(DriverDisplayModel driver)
        {
            InitializeComponent();


            Driver = driver;

            if (driver.Photo != null && driver.Photo.Length > 0)
            {
                BitmapImage bitmap = new BitmapImage();
                using (MemoryStream ms = new MemoryStream(driver.Photo))
                {
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = ms;
                    bitmap.EndInit();
                }
                PhotoPreview.Source = bitmap;
                PhotoSouce = bitmap;
            }
            else Debug.WriteLine("No Photo");

            if (driver.EmploymentContractScan != null && driver.EmploymentContractScan.Length > 0)
            {
                BitmapImage bitmap = new BitmapImage();
                using (MemoryStream ms = new MemoryStream(driver.EmploymentContractScan))
                {
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = ms;
                    bitmap.EndInit();
                }
                ContractPreview.Source = bitmap;
                ContractScanSource = bitmap;
            }
            else Debug.WriteLine("No Scan");

            EmployeeId = driver.UserId;

            FullNameTextBox.Text = driver.FullName;
            LoginTextBox.Text = driver.Login;
            PhoneTextBox.Text = driver.Phone;
            EmailTextBox.Text = driver.Email;
            PasswordBox.Password = "Скрыт";

        }


        private void UploadPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            if (openFileDialog.ShowDialog() == true)
            {
                BitmapImage bitmap = new BitmapImage(new Uri(openFileDialog.FileName));
                PhotoPreview.Source = bitmap;
                display_model_photo_path = openFileDialog.FileName;
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
                display_model_contract_path = openFileDialog.FileName;
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            FullNameTextBox.IsReadOnly = false;
            LoginTextBox.IsReadOnly = false;
            PhoneTextBox.IsReadOnly = false;
            EmailTextBox.IsReadOnly = false;
            PasswordBox.IsEnabled = true;
            UploadPhotoButton.IsEnabled = true;
            UploadContractButton.IsEnabled = true;

            PhotoBorder.AllowDrop = true;
            ContractBorder.AllowDrop = true;

            SaveButton.IsEnabled = true;
            EditButton.IsEnabled = false;


        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            FullNameTextBox.IsReadOnly = true;
            LoginTextBox.IsReadOnly = true;
            PhoneTextBox.IsReadOnly = true;
            EmailTextBox.IsReadOnly = true;
            PasswordBox.IsEnabled = false;
            UploadPhotoButton.IsEnabled = false;
            UploadContractButton.IsEnabled = false;

            SaveButton.IsEnabled = false;
            EditButton.IsEnabled = true;


            if (FullNameTextBox.Text != Driver.FullName ||
            LoginTextBox.Text != Driver.Login ||
            PhoneTextBox.Text != Driver.Phone ||
            EmailTextBox.Text != Driver.Email ||
            PasswordBox.Password != "Скрыт" ||
            PhotoPreview.Source != PhotoSouce ||
            ContractPreview.Source != ContractScanSource
            )
            {
                byte[] Photo;
                byte[] Employment_contract_scan;


                string query = @"
        UPDATE users
        SET 
            full_name = @Full_name,
            login = @Login,
            phone = @Phone,
            email = @Email,
            password_hash = @Password_hash,
            photo = @Photo,
            employment_contract_scan = @Employment_contract_scan
        WHERE 
            user_id = @User_id";

                if (PhotoPreview.Source != PhotoSouce || PhotoPreview.Source == null) { Photo = ImageConverter.ConvertImageToByteArray(display_model_photo_path); }
                else Photo = ImageConverter.ConvertImageSourceToByteArray(PhotoPreview.Source);
                if (ContractPreview.Source != ContractScanSource || ContractPreview.Source == null) { Employment_contract_scan = ImageConverter.ConvertImageToByteArray(display_model_contract_path); }
                else Employment_contract_scan = ImageConverter.ConvertImageSourceToByteArray(ContractPreview.Source);

                MySqlParameter[] parameters = {
            new MySqlParameter("@Full_name", FullNameTextBox.Text),
            new MySqlParameter("@Login", LoginTextBox.Text),
            new MySqlParameter("@Phone",  PhoneTextBox.Text),
            new MySqlParameter("@Email", EmailTextBox.Text),
            new MySqlParameter("@Password_hash",  PasswordHasher.HashPassword(PasswordBox.Password)),
            new MySqlParameter("@Photo",  Photo),
            new MySqlParameter("@Employment_contract_scan", Employment_contract_scan),
            new MySqlParameter("@User_id", EmployeeId),
            };
                Debug.WriteLine("IwannaKillMyFuvkinCode");
                Debug.WriteLine(EmployeeId);
                int insert = ctx.ExecuteNonQuery(query, parameters);
                if (insert > 0)
                    MessageBox.Show("Данные успешно занесены!", "Действие", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("Ошибка", "Действие", MessageBoxButton.OK, MessageBoxImage.Error);


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

        private void HandleFileDropPhoto(DragEventArgs e, Image imageControl)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Получаем массив перетаскиваемых файлов
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                // Берем первый файл (если их несколько)
                string filePath = files[0];

                display_model_photo_path = filePath;

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

                display_model_contract_path = filePath;

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


    }


}


    

