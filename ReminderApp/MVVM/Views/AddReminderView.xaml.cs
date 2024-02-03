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


        

        
    }
}
