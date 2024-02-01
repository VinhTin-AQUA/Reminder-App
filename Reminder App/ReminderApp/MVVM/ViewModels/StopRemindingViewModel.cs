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
    public class StopRemindingViewModel : ViewModelBase
    {
        public ICommand StopCommand { get; set; }

        public StopRemindingViewModel()
        {
            StopCommand = new RelayCommand(ExecuteStopCommand);
        }

        private void ExecuteStopCommand(object parameter)
        {
            var context = WindowStore.MainWindow!.DataContext as MainViewModel;
            context!.ExecuteRedirectToHomeViewCommand(null!);
        }
    }
}
