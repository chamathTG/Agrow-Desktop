using AgrowDesktop.Models;
using AgrowDesktop.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace AgrowDesktop.Views
{
    /// <summary>
    /// Interaction logic for MainWin.xaml
    /// </summary>
    public partial class MainWin : Window
    {
        public MainWin()
        {
            InitializeComponent();
        }

        // TopbarOP
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
            Application.Current.Shutdown();
        }

        // LoadCrateNewPage
        private void Create_New(object sender, MouseButtonEventArgs e)
        {
            CreateAccWin win = new CreateAccWin();
            win.Show();
            this.Close();
        }

        // LoadLoginBtn
        private void Login_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(UsernameBox.Text) || string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                new MessageWin("Please fill all fields before login!", false).ShowDialog();
                return;
            }

            var vm = new LoginViewModel();

            AdminModel admin = vm.Login(UsernameBox.Text, PasswordBox.Password);

            if (admin != null)
            {
                new MessageWin("Login successful!", true).ShowDialog();

                DashboardWin dash = new DashboardWin();

                // WeCan Pass Username To DashWin
                // dash.UserText.Text = admin.Username;

                dash.Show();

                this.Close();
            }
            else
            {
                new MessageWin("Invalid username or password!", false).ShowDialog();

                UsernameBox.Clear();
                PasswordBox.Clear();
            }
        }

        // ForogotPssClick
        private void Forogot_Pass(object sender, RoutedEventArgs e)
        {
            ForgetPassWin win = new ForgetPassWin();
            win.ShowDialog();
        }
    }
}
