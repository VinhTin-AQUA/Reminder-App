using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReminderApp.MVVM.Models
{
    public class ReminderModel
    {
        public int Id { get; set; }
        public List<int> DayRepeats { get; set; } = new List<int>();
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }
        public string Content { get; set; } = "";
    }
}
