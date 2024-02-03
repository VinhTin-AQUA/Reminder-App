using Notification.Wpf;
using ReminderApp.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReminderApp.Stores
{
    public static class ReminderTask
    {
        private static bool isRunning = false;

        public static async Task Start(List<ReminderModel> reminders)
        {
            isRunning = true;
            List<Task> tasks = new List<Task>();

            foreach(var reminder in reminders)
            {
                tasks.Add(Task.Run(() => ShowNotification(reminder)));
            }

            await Task.WhenAll(tasks);
        }

        public static void Stop()
        {
            isRunning = false;
        }

     
        public static async Task ShowNotification(ReminderModel reminder)
        {
            while(true)
            {
                TimeSpan timeSpan = new TimeSpan(reminder.Hours, reminder.Minutes, reminder.Seconds);
                await Task.Delay(timeSpan);

                if (isRunning == false)
                {
                    break;
                }

                var notificationManager = new NotificationManager();
                notificationManager.Show("Title", reminder.Content);
            }
        }
    }
}
