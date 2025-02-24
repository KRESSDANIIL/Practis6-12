using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace Practis6_12.Models
{
    public class ShiftDisplayModel
    {
        public int Shift_id { get; set; }

        public string FullName { get; set; }

        public int User_id { get; set; }
        public DateTime start_time { get; set; }
        public DateTime End_time { get; set; }
        public byte[] Photo { get; set; }

        public ImageSource PhotoSource
        {
            get
            {
                if (Photo == null) return null;
                BitmapImage image = new BitmapImage();
                using (var ms = new MemoryStream(Photo))
                {
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = ms;
                    image.EndInit();
                }
                return image;
            }

        }

    }
}
