using ReminderApp.Stores;
using ReminderApp.Utils;
using System.Configuration;
using System.Data;
using System.Windows;

namespace ReminderApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private async void Application_Startup(object sender, StartupEventArgs e)
        {
            await ReminderDataContext.InitData();
            WindowStore.MainWindow!.Show();
        }
    }

}
