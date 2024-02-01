using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReminderApp.MVVM.Stores
{
    public class WindowStore
    {
        private static MainWindow? mainWindow = null;

        public static MainWindow? MainWindow
        {
            get 
            { 
                if (mainWindow == null)
                {
                    mainWindow = new MainWindow();
                }
                return mainWindow; 
            }
            set { mainWindow = value; }
        }

    }
}
