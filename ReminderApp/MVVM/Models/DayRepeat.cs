namespace ReminderApp.MVVM.Models
{
    public class DayRepeat
    {
        public int Id { get; private set; }
        public string? Day { get; private set; }
        public bool IsChecked { get; set; }

        public static List<DayRepeat> GetDayOfWeeks()
        {
            List<DayRepeat> dayOfWeeks =
            [
                 new() { Id = 0, Day = "Sunday", IsChecked = false },
                new() { Id = 1, Day = "Monday", IsChecked = false },
                new() { Id = 2, Day = "Tuesday", IsChecked = false },
                new() { Id = 3, Day = "Wednesday", IsChecked = false },
                new() { Id = 4, Day = "Thursday", IsChecked = false },
                new() { Id = 5, Day = "Friday", IsChecked = false },
                new() { Id = 6, Day = "Saturday", IsChecked = false },
            ];
            return dayOfWeeks;
        }
    }
}
