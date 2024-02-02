using Newtonsoft.Json.Linq;
using ReminderApp.MVVM.Core;
using ReminderApp.MVVM.Models;
using ReminderApp.Stores;
using ReminderApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ReminderApp.MVVM.ViewModels
{
    public class AddReminderViewModel : ViewModelBase
    {
        private Array weekDays;
        private ReminderModel reminderModel;
        private string hours;
        private string minutes;
        private string seconds;

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

        public ICommand AddDayOfWeekCommand { get; set; }

        public ICommand SaveReminderCommand { get; set; }
        public string Hours { get => hours; set { hours = value; OnPropertyChanged(); } }
        public string Minutes { get => minutes; set { minutes = value; OnPropertyChanged(); } }
        public string Seconds { get => seconds; set { seconds = value; OnPropertyChanged(); } }

        public AddReminderViewModel()
        {
            reminderModel = null!;
            weekDays = null!;
            hours = "0";
            minutes = "0";
            seconds = "0";

            WeekDays = Enum.GetValues(typeof(DaysOfWeek)); ;
            ReminderModel = new ReminderModel();

            NavigateHomeViewCommand = new RelayCommand(ExecuteNavigateHomeViewCommand);
            AddDayOfWeekCommand = new RelayCommand(ExecuteAddDayOfWeekCommand);
            SaveReminderCommand = new AsyncRelayCommand(ExecuteSaveReminderCommand);
        }
        private void ExecuteNavigateHomeViewCommand(object parameter)
        {
            WindowStore.NaivigateHomeChild();
        }

        private void ExecuteAddDayOfWeekCommand(object obj)
        {
            DaysOfWeek dayOfWeek = (DaysOfWeek)obj;
            int index = ReminderModel.DaysOfWeek.IndexOf(dayOfWeek);
            if (index != -1)
            {
                ReminderModel.DaysOfWeek.RemoveAt(index);
            }
            else
            {
                ReminderModel.DaysOfWeek.Add(dayOfWeek);
            }
        }

        private async Task ExecuteSaveReminderCommand(object obj)
        {
            int hours = 0;
            int minutes = 0;
            int seconds = 0;

            #region check characters

            if (IsNumeric(Hours, ref hours) == false)
            {
                MessageBox.Show("The hours must be a integer number.",
                                   "Error: Hours",
                                   MessageBoxButton.OK,
                                   MessageBoxImage.Error);
                return;
            }

            if (IsNumeric(Minutes, ref minutes) == false)
            {
                MessageBox.Show("The minutes must be a integer number.",
                                   "Error: Minutes",
                                   MessageBoxButton.OK,
                                   MessageBoxImage.Error);
                return;
            }

            if (IsNumeric(Seconds, ref seconds) == false)
            {
                MessageBox.Show("The seconds must be a integer number.",
                                   "Error: Seconds",
                                   MessageBoxButton.OK,
                                   MessageBoxImage.Error);
                return;
            }

            #endregion

            #region check negative number

            if (hours < 0)
            {
                MessageBox.Show("The hours must be positive number.",
                                   "Error: Hours",
                                   MessageBoxButton.OK,
                                   MessageBoxImage.Error);
                return;
            }

            if (minutes < 0)
            {
                MessageBox.Show("The minutes must be positive number.",
                                   "Error: Minutes",
                                   MessageBoxButton.OK,
                                   MessageBoxImage.Error);
                return;
            }

            if (seconds < 0 && hours == 0 && minutes == 0)
            {
                MessageBox.Show("The seconds must be at least 10.",
                                   "Error: Seconds",
                                   MessageBoxButton.OK,
                                   MessageBoxImage.Error);
                return;
            }

            #endregion

            ReminderModel.Hours = hours;
            ReminderModel.Minutes = minutes;
            ReminderModel.Seconds = seconds;
            ReminderModel.Id = ReminderDataContext.Id;

            await ReminderDataContext.SaveReminder(ReminderModel);
            WindowStore.NaivigateHomeChild();
        }

        private static bool IsNumeric(string text, ref int value)
        {
            // Kiểm tra xem văn bản có phải là số hay không
            return int.TryParse(text, out value);
        }
    }
}
