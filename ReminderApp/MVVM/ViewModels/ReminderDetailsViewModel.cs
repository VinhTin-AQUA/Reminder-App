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
    public class ReminderDetailsViewModel : ViewModelBase
    {
        private int id;
        private string hours;
        private string minutes;
        private string seconds;
        private string content;
        private List<DayRepeat> dayRepeats;
        private List<int> dayRepeated;

        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged(); }
        }

        public string Hours
        {
            get { return hours; }
            set { hours = value; OnPropertyChanged(); }
        }

        public string Minutes
        {
            get { return minutes; }
            set { minutes = value; OnPropertyChanged(); }
        }

        public string Seconds
        {
            get { return seconds; }
            set { seconds = value; OnPropertyChanged(); }
        }

        public string Content
        {
            get { return content; }
            set { content = value; OnPropertyChanged(); }
        }

        public List<DayRepeat> DayRepeats
        {
            get { return dayRepeats; }
            set { dayRepeats = value; OnPropertyChanged(); }
        }

        public List<int> DayRepeated
        {
            get { return dayRepeated; }
            set { dayRepeated = value; OnPropertyChanged(); }
        }

        public ICommand UpdateReminderCommand { get; set; }
        public ICommand UpdateDayOfWeekCommand { get; set; }
        public ICommand NavigateHomeViewCommand { get; set; }
        public ICommand DeleteReminderCommand { get; set; }


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ReminderDetailsViewModel()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }

        public ReminderDetailsViewModel(ReminderModel reminder)
        {
            hours = "";
            minutes = "";
            seconds = "";
            content = "";
            dayRepeats = null!;
            dayRepeated = null!;

            Id = reminder.Id;
            Hours = reminder.Hours.ToString();
            Minutes = reminder.Minutes.ToString();
            Seconds = reminder.Seconds.ToString();
            Content = reminder.Content.ToString();
            DayRepeated = reminder.DayRepeats;

            DayRepeats = DayRepeat.GetDayOfWeeks();
            foreach(int id in DayRepeated)
            {
                DayRepeats[id].IsChecked = true;
            }

            UpdateReminderCommand = new AsyncRelayCommand(ExecuteUpdateReminderCommand);
            UpdateDayOfWeekCommand = new RelayCommand(ExecuteUpdateDayOfWeekCommand);
            NavigateHomeViewCommand = new RelayCommand(ExecuteNavigateHomeViewCommand);
            DeleteReminderCommand = new AsyncRelayCommand(ExecuteDeleteReminderCommand);
        }

        private async Task ExecuteUpdateReminderCommand(object parameter)
        {
            int hours = 0;
            int minutes = 0;
            int seconds = 0;
            int id = (int)parameter;

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

            #region check time limit

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

            if (seconds < 5 && hours == 0 && minutes == 0)
            {
                MessageBox.Show("The time must be at least 5 seconds.",
                                   "Error: Seconds",
                                   MessageBoxButton.OK,
                                   MessageBoxImage.Error);
                return;
            }

            #endregion

            ReminderModel newReminderModel = new() {
                Id = id,
                DayRepeats = this.DayRepeated,
                Hours = hours,
                Minutes = minutes,
                Seconds = seconds,
                Content = content,
            };
            await ReminderDataContext.UpdateReminder(newReminderModel);
            WindowStore.NaivigateHomeChild();
        }

        private void ExecuteUpdateDayOfWeekCommand(object parameter)
        {
            DayRepeat dayRepeat = (DayRepeat)parameter;
            int index = DayRepeated.IndexOf(dayRepeat.Id);
            if (index != -1)
            {
                DayRepeated.RemoveAt(index);
            }
            else
            {
                DayRepeated.Add(dayRepeat.Id);
            }
        }

        private void ExecuteNavigateHomeViewCommand(object parameter)
        {
            var r = MessageBox.Show("Your work has not been saved. Do you want to exit?",
                "Back", MessageBoxButton.OKCancel, MessageBoxImage.Question);

            if (r == MessageBoxResult.Cancel)
            {
                return;
            }
            WindowStore.NaivigateHomeChild();
        }

        private async Task ExecuteDeleteReminderCommand(object parameter)
        {
            var r = MessageBox.Show("Do you want to remove this reminder?", "Remove reminder",
                MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if(r == MessageBoxResult.Cancel)
            {
                return;
            }
            int id = (int)parameter;
            await ReminderDataContext.DeleteReminder(id);
            WindowStore.NaivigateHomeChild();
        }

        private static bool IsNumeric(string text, ref int value)
        {
            // Kiểm tra xem văn bản có phải là số hay không
            return int.TryParse(text, out value);
        }
    }
}
