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
    public class HomeViewModel : ViewModelBase
    {
        private List<ReminderModel> reminders;

        public List<ReminderModel> Reminders
        {
            get { return reminders; }
            set { reminders = value; OnPropertyChanged(); }
        }

        public ICommand NavigateToAddReminderCommand { get; set; }
        public ICommand NavigateToStopRemindingViewCommand { get; set; }


        public HomeViewModel()
        {
            NavigateToAddReminderCommand = new RelayCommand(ExecuteNavigateToAddReminderCommand);
            NavigateToStopRemindingViewCommand = new RelayCommand(ExecuteNavigateToNavigateToStopRemindingViewCommand);
            Init();
        }

        private async void Init()
        {
            await ReminderDataContext.ReadRemindes();
            Reminders = ReminderDataContext.Reminders!;
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
