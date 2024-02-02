using ReminderApp.MVVM.Models;
using ReminderApp.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ReminderApp.MVVM.Views
{
    /// <summary>
    /// Interaction logic for AddReminderView.xaml
    /// </summary>
    public partial class AddReminderView : UserControl
    {
        private readonly AddReminderViewModel dataContext = null!;
        public AddReminderView()
        {
            InitializeComponent();
            dataContext = (AddReminderViewModel)this.DataContext;
        }

        private void hourInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text == "")
            {
                return;
            }

            // Chỉ cho phép nhập các ký tự số
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            if(checkBox != null)
            {
                var dayOfWeek = (DaysOfWeek)checkBox.Tag;
                dataContext.AddWeekDay(dayOfWeek);
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox != null)
            {
                var dayOfWeek = (DaysOfWeek)checkBox.Tag;
                dataContext.RemoveWeekDay(dayOfWeek);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(dataContext.ReminderModel.DaysOfWeek[0].ToString());
        }

        //public bool IsToday(DaysOfWeek day)
        //{
        //    DateTime today = DateTime.Today;
        //    DaysOfWeek todayEnum = (DaysOfWeek)today.DayOfWeek;

        //    return todayEnum == day;
        //}
    }
}
