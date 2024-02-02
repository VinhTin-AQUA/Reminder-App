using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReminderApp.Utils
{
    public static class Urls
    {
        private static string dataUrl = @"./Data";

        private static string remiderUrl = "/reminderInfo.json";
        private static string idUrl = "/identity.txt";


        public static string DataUrl
        {
            get { return dataUrl; }
        }

        public static string RemiderUrl
        {
            get 
            { 
                return dataUrl + remiderUrl; 
            }
        }

        public static string IdUrl
        {
            get
            {
                return dataUrl + idUrl;
            }
        }
    }
}
