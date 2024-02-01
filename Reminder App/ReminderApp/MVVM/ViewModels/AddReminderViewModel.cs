using ReminderApp.MVVM.Core;
using ReminderApp.MVVM.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ReminderApp.MVVM.ViewModels
{
    public class AddReminderViewModel : ViewModelBase
    {
        private List<string> reminders;

        public List<string> Reminders
        {
            get { return reminders; }
            set { reminders = value; OnPropertyChanged(); }
        }

        public ICommand NavigateHomeViewCommand { get; set; }

        public AddReminderViewModel()
        {
            reminders = null!;
            Reminders = [
                "Don't repeat",
                "Monday",
                "Tuesday",
                "Wednesday",
                "Thursday",
                "Friday",
                "Saturday",
                "Sunday"
            ];
            NavigateHomeViewCommand = new RelayCommand(ExecuteNavigateHomeViewCommand);
        }
        private void ExecuteNavigateHomeViewCommand(object parameter)
        {
            var context = WindowStore.MainWindow!.DataContext as MainViewModel;
            context!.ExecuteRedirectToHomeViewCommand(null!);
        }

    }
}
