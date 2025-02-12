using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practis6_12.Models
{
    public class OrderDisplayModel
    {
        public int Order_id { get; set; }

        public int Dispatcher_id { get; set; }

        public int Driver_id { get; set; }
        public string Client_phone { get; set; }

        public string Pickup_location { get; set; }
        public string Dropoff_location { get; set; }

        public string Status { get; set; }
        public decimal Fare { get; set; }

        public bool Discount_applied { get; set; }

        public DateTime Created_at { get; set; }

        public DateTime Completed_at { get; set; }
    }
}
