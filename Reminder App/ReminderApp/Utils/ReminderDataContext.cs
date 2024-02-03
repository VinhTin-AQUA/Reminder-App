using Newtonsoft.Json;
using ReminderApp.MVVM.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ReminderApp.Utils
{
    public static class ReminderDataContext
    {
        public static int Id { get; set; } = 0;

        public static List<ReminderModel>? Reminders { get; set; }


        public static async Task InitData()
        {
            if (Directory.Exists(Urls.DataUrl) == false)
            {
                Directory.CreateDirectory(Urls.DataUrl);
            }
            using (var fileStream = new FileStream(Urls.RemiderUrl, FileMode.OpenOrCreate, FileAccess.ReadWrite)) { }
            using (var fileStream = new FileStream(Urls.IdUrl, FileMode.OpenOrCreate, FileAccess.ReadWrite)) 
            {
                using (var sr = new StreamReader(fileStream) )
                {
                    var content = await sr.ReadToEndAsync();
                    if ( content != "")
                    {
                        Id = int.Parse(content);
                    } else
                    {
                        Id = 0;
                    }
                }
            }

            await ReadRemindes();
        }

        public static async Task SaveReminder(ReminderModel model)
        {
            await ReadRemindes();
            Reminders!.Add(model);
            var jsonContent = JsonConvert.SerializeObject(Reminders, Formatting.Indented);

            using (var sw = new StreamWriter(Urls.RemiderUrl, false))
            {
                await sw.WriteAsync(jsonContent);
                await WriteLastedId(model.Id);
            }
        }

        public static async Task ReadRemindes()
        {
            using (var sr = new StreamReader(Urls.RemiderUrl))
            {
                var content = await sr.ReadToEndAsync();
                Reminders = JsonConvert.DeserializeObject<List<ReminderModel>>(content);
                if(Reminders == null)
                {
                    Reminders = new List<ReminderModel>();
                }
            }
        }

        public static async Task UpdateReminder(ReminderModel model)
        {
            //await ReadRemindes();
            var reminder = Reminders!
                .Where(r => r.Id == model.Id)
                .FirstOrDefault();

            if(reminder == null)
            {
                return;
            }
            reminder.DayRepeats = model.DayRepeats;
            reminder.Hours = model.Hours;
            reminder.Minutes = model.Minutes;
            reminder.Seconds = model.Seconds;
            reminder.Content = model.Content;
            var jsonContent = JsonConvert.SerializeObject(Reminders, Formatting.Indented);

            using (var sw = new StreamWriter(Urls.RemiderUrl, false))
            {
                await sw.WriteAsync(jsonContent);
                await WriteLastedId(model.Id);
            }
        }



        private static async Task WriteLastedId(int id)
        {
            using (var sw = new StreamWriter(Urls.IdUrl, false))
            {
                await sw.WriteAsync(id.ToString());
                Id++;
            }
        }
    }
}
