using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ReminderApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /* chức năng: khi full màng hình, có thể dùng chuột kéo header để kích thước ứng dụng trở về ban đầu */
        [DllImport("user32.dll")]
        //private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        public static extern int SendMessage(IntPtr hWnd, int wWnd, int wParam, int lParam);


        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper windowInteropHelper = new WindowInteropHelper(this);
            SendMessage(windowInteropHelper.Handle, 161, 2, 0);
        }

        private void panelControl_MouseEnter(object sender, MouseEventArgs e)
        {
            /* khi full mành hình thì chừa lại thanh task bar */
            /* được trong sự kiện mouse enter vì người dùng có thể sử dụng nhiều màng hình với độ phân giải khác nhau */
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        private void minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void maximize_Click(object sender, RoutedEventArgs e)
        {
            if(WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            else
            {
                WindowState = WindowState.Maximized;
            }
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            var r = MessageBox.Show("Do you want to exist", "Exist", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if(r == MessageBoxResult.OK)
            {
                Application.Current.Shutdown();
            }
        }
    }
}