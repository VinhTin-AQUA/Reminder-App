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
    public static class DataContext
    {
        public static int Id { get; set; } = 0;

        public static async Task InitData()
        {
            if (Directory.Exists(Urls.DataUrl) == false)
            {
                Directory.CreateDirectory(Urls.DataUrl);
            }
            using (var fileStream = new FileStream(Urls.RemiderUrl, FileMode.Create, FileAccess.ReadWrite)) { }
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
        }

        public static async Task SaveReminder(ReminderModel model)
        {
            List<ReminderModel>? list = null!;
            using (var sr = new StreamReader(Urls.RemiderUrl))
            {
                var content = await sr.ReadToEndAsync();
                list = JsonConvert.DeserializeObject<List<ReminderModel>>(content);

            }
            if(list == null)
            {
                list = new List<ReminderModel>();
            }
            list!.Add(model);
            var jsonContent = JsonConvert.SerializeObject(list, Formatting.Indented);

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
