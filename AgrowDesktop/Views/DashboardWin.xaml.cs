using System.Windows;
using System.Windows.Input;

namespace AgrowDesktop.Views
{
    /// <summary>
    /// Interaction logic for DashboardWin.xaml
    /// </summary>
    public partial class DashboardWin : Window
    {
        public DashboardWin()
        {
            InitializeComponent();
        }

        //TopbarOP
        private void TopBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
