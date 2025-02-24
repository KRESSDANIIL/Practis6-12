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
    public class DispatcherDisplayModel
    {

        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Login { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public byte[] Photo { get; set; }
        public byte[] EmploymentContractScan { get; set; }

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

        public ImageSource ContractScanSource
        {
            get
            {
                if (EmploymentContractScan == null) return null;
                BitmapImage image = new BitmapImage();
                using (var ms = new MemoryStream(EmploymentContractScan))
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

