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

        public ICommand StopAndBackCommand { get; set; }
        public ICommand StartCommand { get; set; }
        public ICommand StopCommand { get; set; }
        public ICommand ChooseReminderCommand { get; set; }


        public StartRemindingViewModel()
        {
            reminders = null!;
            remindersToStart = null!;
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
            if (RemindersToStart.Count == 0)
            {
                MessageBox.Show("Please select any reminder to start.", "No reminders to start.",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            await ReminderTask.Start(RemindersToStart);
        }

        private void ExecuteStopCommand(object parameter)
        {
            ReminderTask.Stop();
        }

        private void ExecuteChooseReminderCommand(object parameter)
        {
            ReminderModel reminder = (ReminderModel)parameter;
            if( reminder == null )
            {
                return;
            }

            if(RemindersToStart.Any(r => r.Id == reminder.Id ) )
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
