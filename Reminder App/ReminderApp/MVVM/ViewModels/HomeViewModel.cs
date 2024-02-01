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
    public class HomeViewModel : ViewModelBase
    {
        public ICommand NavigateToAddReminderCommand { get; set; }
        public ICommand NavigateToStopRemindingViewCommand { get; set; }


        public HomeViewModel()
        {
            NavigateToAddReminderCommand = new RelayCommand(ExecuteNavigateToAddReminderCommand);
            NavigateToStopRemindingViewCommand = new RelayCommand(ExecuteNavigateToNavigateToStopRemindingViewCommand);
        }

        private void ExecuteNavigateToAddReminderCommand(object parameter)
        {
            var context = WindowStore.MainWindow!.DataContext as MainViewModel;
            context!.ExecuteRedirectToAddReminderCommand(null!);
        }

        private void ExecuteNavigateToNavigateToStopRemindingViewCommand(object parameter)
        {
            var context = WindowStore.MainWindow!.DataContext as MainViewModel;
            context!.ExecuteRedirectToStopRemindingViewCommand(null!);
        }
    }
}
