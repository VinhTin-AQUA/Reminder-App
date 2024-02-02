using ReminderApp.MVVM.Core;
using ReminderApp.MVVM.Models;
using ReminderApp.Stores;
using ReminderApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ReminderApp.MVVM.ViewModels
{
    public class StopRemindingViewModel : ViewModelBase
    {
        private List<ReminderModel> reminders;

        public List<ReminderModel> Reminders 
        { 
            get { return reminders; }
            set { reminders = value; OnPropertyChanged(); }
        }

        public ICommand StopCommand { get; set; }

        public StopRemindingViewModel()
        {
            reminders = null!;
            StopCommand = new RelayCommand(ExecuteStopCommand);
            Init();
        }

        private async void Init()
        {
            await ReminderDataContext.ReadRemindes();
            Reminders = ReminderDataContext.Reminders!;
        }

        private void ExecuteStopCommand(object parameter)
        {
            var context = WindowStore.MainWindow!.DataContext as MainViewModel;
            context!.ExecuteRedirectToHomeViewCommand(null!);
        }
    }
}
