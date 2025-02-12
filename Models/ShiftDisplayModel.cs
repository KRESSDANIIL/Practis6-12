using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practis6_12.Models
{
    public class ShiftDisplayModel
    {
        public int Shift_id { get; set; }
        public int User_id { get; set; }
        public DateTime start_time { get; set; }
        public DateTime End_time { get; set; }
        public byte[] Photo { get; set; }

    }
}
