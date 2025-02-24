using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace Practis6_12
{
    class ImageConverter
    {
        public static byte[] ConvertImageToByteArray(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath)) return null;

            try
            {
                // Чтение изображения в массив байтов
                byte[] imageBytes = File.ReadAllBytes(imagePath);
                return imageBytes;
            }
            catch (Exception ex)
            {
                // Обработка ошибок (например, если файл не найден)
                Console.WriteLine($"Ошибка при чтении изображения: {ex.Message}");
                return null;
            }
        }
        public static byte[] ConvertImageSourceToByteArray(ImageSource imageSource)
        {
            if (imageSource == null) return null;

            BitmapSource bitmapSource = imageSource as BitmapSource;
            if (bitmapSource == null) return null;

            using (MemoryStream stream = new MemoryStream())
            {
                BitmapEncoder encoder = new PngBitmapEncoder(); // Используем PNG, можно менять на JpegBitmapEncoder
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                encoder.Save(stream);
                return stream.ToArray();
            }
        }
    }
}
