using ReminderApp.MVVM.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ReminderApp.MVVM.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase currentChildView;

        public ViewModelBase CurrentChildView
        {
            get { return currentChildView; }
            set { currentChildView = value; OnPropertyChanged(); }
        }



        public MainViewModel()
        {
            

            CurrentChildView = new HomeViewModel();
        }

        public void ExecuteRedirectToAddReminderCommand(object parameter)
        {
            CurrentChildView = new AddReminderViewModel();
        }

        public void ExecuteRedirectToHomeViewCommand(object parameter)
        {
            CurrentChildView = new HomeViewModel();
        }
    }
}
