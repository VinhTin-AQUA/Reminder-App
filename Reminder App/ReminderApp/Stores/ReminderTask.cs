using Notification.Wpf;
using Notification.Wpf.Classes;
using ReminderApp.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using ReminderApp.Utils;

namespace ReminderApp.Stores
{
    public static class ReminderTask
    {
        private static bool isRunning = false;

        public static async Task Start(List<ReminderModel> reminders)
        {
            isRunning = true;
            List<Task> tasks = new List<Task>();

            foreach (var reminder in reminders)
            {
                tasks.Add(Task.Run(() => ShowNotification(reminder)));
            }

            await Task.WhenAll(tasks);
        }

        public static void Stop()
        {
            isRunning = false;
        }

        private static async Task ShowNotification(ReminderModel reminder)
        {
            // System.AppDomain.CurrentDomain.BaseDirectory
            await Application.Current.Dispatcher.Invoke(async () =>
            {
                int checkDayOfWeek = CheckDayOfWeek(reminder);
                if(checkDayOfWeek == 0)
                {
                    return;
                }

                while (true)
                {
                    TimeSpan timeSpan = new TimeSpan(reminder.Hours, reminder.Minutes, reminder.Seconds);
                    await Task.Delay(timeSpan);

                    if (isRunning == false)
                    {
                        break;
                    }

                    var content = new NotificationContent
                    {
                        Title = "Reminder Notification",
                        Message = reminder.Content,
                        Type = NotificationType.Success,
                        RowsCount = 3,
                        CloseOnClick = true,
                        Background = new SolidColorBrush(Colors.White),
                        Foreground = new SolidColorBrush(Colors.Black),

                        Icon = new BitmapImage(new Uri(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, Urls.ReminderIcon))),

                        Image = new NotificationImage()
                        {
                            Source = new BitmapImage(new Uri(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, Urls.ReminderBackground))),
                            Position = ImagePosition.Top
                        }
                    };

                    var notificationManager = new NotificationManager();
                    notificationManager.Show(content, expirationTime: TimeSpan.FromSeconds(15));

                    if(checkDayOfWeek == -1)
                    {
                        break;
                    }
                }
            });
        }

        private static int CheckDayOfWeek(ReminderModel reminder)
        {
            var r = reminder.DayRepeats;
            DateTime today = DateTime.Now;
            var dayOfWeek = (int)today.DayOfWeek;

            /*
             * -1: không có ngày lặp lại
             * 0: có ngày lặp lại nhưng không phải hôm nay
             * 1: có ngày lặp lại và có hôm nay
             */

            if (r.Count() == 0)
            {
                return -1;
            }

            if (r.Any(d => d == dayOfWeek) == true)
            {
                return 1;
            }
            return 0;
        }
    }
}
