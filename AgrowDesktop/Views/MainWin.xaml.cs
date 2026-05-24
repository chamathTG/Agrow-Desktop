using AgrowDesktop.Models;
using AgrowDesktop.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace AgrowDesktop.Views
{
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
            MessageWin message = new MessageWin();


            if (string.IsNullOrWhiteSpace(UsernameBox.Text) || string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                message.ColTxtHandler("Please fill all the fields before login!", false);
                message.OpacityHandler(this);
                message.ShowDialog();

                return;
            }

            var vm = new LoginViewModel();

            AdminModel admin = vm.Login(UsernameBox.Text, PasswordBox.Password);

            if (admin != null)
            {
                message.ColTxtHandler("Login Successful!", true);
                message.OpacityHandler(this);
                message.ShowDialog();

                DashboardWin dash = new DashboardWin();
                dash.Show();
                this.Close();
            }
            else
            {
                message.ColTxtHandler("Invalid Username or Password!", false);
                message.OpacityHandler(this);
                message.ShowDialog();

                UsernameBox.Clear();
                PasswordBox.Clear();
            }
        }

        // ForogotPssClick
        private void Forogot_Pass(object sender, RoutedEventArgs e)
        {
            ForgetPassWin win = new ForgetPassWin();
            win.Owner = this;
            win.ShowDialog();
        }
    }
}
