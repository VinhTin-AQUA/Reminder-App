using ReminderApp.MVVM.Models;
using ReminderApp.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReminderApp.Stores
{
    public class WindowStore
    {
        private static MainWindow? mainWindow = null;

        #region main window

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


        public static void NaivigateHomeChild()
        {
            var context = MainWindow!.DataContext as MainViewModel;
            context!.ExecuteRedirectToHomeViewCommand(null!);
        }

        public static void NaivigateReminderDetailsChild(ReminderModel reminderModel)
        {
            var context = MainWindow!.DataContext as MainViewModel;
            context!.NavigateToReminderDetails(reminderModel);
        }

        #endregion
    }
}
