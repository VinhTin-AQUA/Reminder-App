using Newtonsoft.Json.Linq;
using ReminderApp.MVVM.Core;
using ReminderApp.MVVM.Models;
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
        private Array weekDays;
        private ReminderModel reminderModel;


        public Array WeekDays
        {
            get { return weekDays; }
            set { weekDays = value; OnPropertyChanged(); }
        }

        public ReminderModel ReminderModel
        {
            get { return reminderModel; }
            set { reminderModel = value; OnPropertyChanged(); }
        }

        
        public ICommand NavigateHomeViewCommand { get; set; }

        public AddReminderViewModel()
        {
            reminderModel = null!;
            weekDays = null!;

            WeekDays = Enum.GetValues(typeof(DaysOfWeek)); ;
            ReminderModel = new ReminderModel();
            NavigateHomeViewCommand = new RelayCommand(ExecuteNavigateHomeViewCommand);
        }
        private void ExecuteNavigateHomeViewCommand(object parameter)
        {
            var context = WindowStore.MainWindow!.DataContext as MainViewModel;
            context!.ExecuteRedirectToHomeViewCommand(null!);
        }

        public void AddWeekDay(DaysOfWeek weekDay)
        {
            ReminderModel.DaysOfWeek.Add(weekDay);
        }

        public void RemoveWeekDay(DaysOfWeek weekDay)
        {
            ReminderModel.DaysOfWeek.Remove(weekDay);
        }
    }
}
