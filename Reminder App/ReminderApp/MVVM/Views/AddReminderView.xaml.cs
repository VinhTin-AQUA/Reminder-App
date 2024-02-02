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
        public AddReminderView()
        {
            InitializeComponent();
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
    }
}
