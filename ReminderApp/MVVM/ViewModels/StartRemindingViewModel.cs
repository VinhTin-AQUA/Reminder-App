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
    public class StartRemindingViewModel : ViewModelBase
    {
        private List<ReminderModel> reminders;
        private List<ReminderModel> remindersToStart;
        private bool hasStarted;
        private bool hasStopped;

        public List<ReminderModel> Reminders
        {
            get { return reminders; }
            set { reminders = value; OnPropertyChanged(); }
        }

        public List<ReminderModel> RemindersToStart
        {
            get { return remindersToStart; }
            set { remindersToStart = value; OnPropertyChanged(); }
        }

        public bool HasStarted
        {
            get { return hasStarted; }
            set { hasStarted = value; OnPropertyChanged(); }
        }

        public bool HasStopped
        {
            get { return hasStopped; }
            set { hasStopped = value; OnPropertyChanged(); }
        }

        public ICommand StopAndBackCommand { get; set; }
        public ICommand StartCommand { get; set; }
        public ICommand StopCommand { get; set; }
        public ICommand ChooseReminderCommand { get; set; }

        public StartRemindingViewModel()
        {
            reminders = null!;
            remindersToStart = null!;
            HasStarted = false;
            HasStopped = true;
            StopAndBackCommand = new RelayCommand(ExecuteStopAndBackCommand);
            StartCommand = new AsyncRelayCommand(ExecuteStartCommand);
            StopCommand = new RelayCommand(ExecuteStopCommand);
            ChooseReminderCommand = new RelayCommand(ExecuteChooseReminderCommand);
            Reminders = ReminderDataContext.Reminders!;
            RemindersToStart = new List<ReminderModel>();
        }

        private void ExecuteStopAndBackCommand(object parameter)
        {
            ReminderTask.Stop();
            WindowStore.NaivigateHomeChild();
        }

        private async Task ExecuteStartCommand(object parameter)
        {
            HasStarted = false;
            HasStopped = true;
            if (RemindersToStart.Count == 0)
            {
                MessageBox.Show("Please select any reminder to start.", "No reminders to start.",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            HasStarted = true;
            HasStopped = false;
            await ReminderTask.Start(RemindersToStart);
        }

        private void ExecuteStopCommand(object parameter)
        {
            HasStarted = false;
            HasStopped = true;
            ReminderTask.Stop();
        }

        private void ExecuteChooseReminderCommand(object parameter)
        {
            ReminderModel reminder = (ReminderModel)parameter;
            if (reminder == null)
            {
                return;
            }

            if (RemindersToStart.Any(r => r.Id == reminder.Id))
            {
                RemindersToStart.Remove(reminder);
            }
            else
            {
                RemindersToStart.Add(reminder);
            }
        }
    }
}
