using Newtonsoft.Json;
using ReminderApp.MVVM.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReminderApp.Utils
{
    public static class DataContext
    {
        public static async Task SaveReminder(ReminderModel model)
        {
            var jsonContent = JsonConvert.SerializeObject(model,Formatting.Indented);


            using (var fileStream = new FileStream(Urls.RemiderUrl,FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (var sw = new StreamWriter(fileStream))
                {
                    await sw.WriteAsync(jsonContent);
                }
            }
        }
    }
}
