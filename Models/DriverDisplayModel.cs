using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practis6_12.Models
{
    public class DriverDisplayModel
    {
        public int UserId { get; set; }

        public string FullName { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }

        public byte[] Photo { get; set; }
        public byte[] EmploymentContractScan { get; set; }


        public float Rating { get; set; }

        public string Driver_type { get; set; }

        public string CarModel { get; set; }

        public string CarNumber { get; set; }


    }
}
