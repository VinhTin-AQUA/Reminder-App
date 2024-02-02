using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReminderApp.Utils
{
    public static class Urls
    {
        private static string dataUrl = @"C:\Users\tinho\Desktop\Projects\Reminder-App\Reminder App\ReminderApp\Data\";

        private static string remiderUrl = "reminderInfo";

        public static string RemiderUrl
        {
            get 
            { 
                return dataUrl + remiderUrl; 
            }
        }
    }
}
