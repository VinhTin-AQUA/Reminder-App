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

        private static string resourceUrl = @"./Resources";
        private static string imageUrl = @"/images";
        private static string reminderBackground = @"/reminder_background.png";
        private static string reminderIcon = @"/reminder_icon.ico";

        // data
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

        // resource
        public static string ResourceUrl
        {
            get { return resourceUrl; }
        }

        public static string ImageUrl
        {
            get { return resourceUrl + imageUrl; }
        }

        public static string ReminderBackground
        {
            get { return resourceUrl + imageUrl + reminderBackground; }
        }

        public static string ReminderIcon
        {
            get { return resourceUrl + imageUrl + reminderIcon; }
        }
    }
}
