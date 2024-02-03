using Newtonsoft.Json;
using ReminderApp.MVVM.Models;
using System;
using System.Collections;
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
                using (var sr = new StreamReader(fileStream))
                {
                    var content = await sr.ReadToEndAsync();
                    if (content != "")
                    {
                        Id = int.Parse(content);
                    }
                    else
                    {
                        Id = 0;
                    }
                }
            }

            await ReadRemindes();


            InitImages();
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
                if (Reminders == null)
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

            if (reminder == null)
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
            }
        }

        public static async Task DeleteReminder(int id)
        {
            var reminder = Reminders!
                .Where(r => r.Id == id)
                .FirstOrDefault();
            if (reminder == null)
            {
                return;
            }
            Reminders!.Remove(reminder);
            var jsonContent = JsonConvert.SerializeObject(Reminders, Formatting.Indented);
            using (var sw = new StreamWriter(Urls.RemiderUrl, false))
            {
                await sw.WriteAsync(jsonContent);
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

        #region private methods

        private static void InitImages()
        {
            if (Directory.Exists(Urls.ResourceUrl) == false)
            {
                Directory.CreateDirectory(Urls.ResourceUrl);
            }

            if (Directory.Exists(Urls.ImageUrl) == false)
            {
                Directory.CreateDirectory(Urls.ImageUrl);
            }

            if (File.Exists(Urls.ReminderBackground) == false ||
                File.Exists(Urls.ReminderIcon) == false)
            {
                // Đọc mảng byte từ file
                byte[] bytes;
                var baseDirectory = System.AppDomain.CurrentDomain.BaseDirectory.Split("\\bin")[0];
                var backgroundPath = baseDirectory + "/Resources/ImageData/reminder_background.bin";
                var iconPath = baseDirectory + "/Resources/ImageData/reminder_icon.bin";

                //C:\Users\tinho\Desktop\Projects\Reminder-App\Reminder App\ReminderApp\bin\Debug\net8.0-windows\
                using (FileStream fs = new FileStream(backgroundPath, FileMode.Open, FileAccess.Read))
                {
                    BinaryReader br = new BinaryReader(fs);
                    bytes = br.ReadBytes((int)fs.Length);

                }

                using (var ms = new MemoryStream(bytes))
                {
                    using (var fs = new FileStream(Urls.ReminderBackground, FileMode.Create))
                    {
                        ms.WriteTo(fs);
                    }
                }


                using (FileStream fs = new FileStream(iconPath, FileMode.Open, FileAccess.Read))
                {
                    BinaryReader br = new BinaryReader(fs);
                    bytes = br.ReadBytes((int)fs.Length);
                }

                using (var ms = new MemoryStream(bytes))
                {
                    using (var fs = new FileStream(Urls.ReminderIcon, FileMode.Create))
                    {
                        ms.WriteTo(fs);
                    }
                }
            }
        }

        #endregion
    }
}
