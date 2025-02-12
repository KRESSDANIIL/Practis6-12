using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}

