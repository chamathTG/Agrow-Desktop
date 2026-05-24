using AgrowDesktop.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace AgrowDesktop.Views
{
    /// <summary>
    /// Interaction logic for CreateAccWin.xaml
    /// </summary>
    public partial class CreateAccWin : Window
    {
        public CreateAccWin()
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
            this.Close();
        }

        // LoadLoginWin
        private void Login_Win(object sender, RoutedEventArgs e)
        {
            MainWin win = new MainWin();
            win.Show();
            this.Close();
        }

        // SignUpBtn
        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            // CheckEmptyFields
            if (string.IsNullOrWhiteSpace(NicBox.Text) ||
                string.IsNullOrWhiteSpace(UserNBox.Text) ||
                string.IsNullOrWhiteSpace(EmailBox.Text) ||
                string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                new MessageWin("Please fill all fields to complete sign up!", false).ShowDialog();
                return;
            }

            var vm = new CreateAccViewModel();

            string result = vm.SignUp(
                NicBox.Text,
                UserNBox.Text,
                PasswordBox.Password,
                EmailBox.Text
            );

            bool success = result.ToLower().Contains("success");

            // ShowMsg
            new MessageWin(result, success).ShowDialog();

            //ClearAll
            NicBox.Clear();
            UserNBox.Clear();
            EmailBox.Clear();
            PasswordBox.Clear();

            // LoadLogin
            if (success)
            {
                MainWin loginWin = new MainWin();
                loginWin.Show();

                this.Close();
            }

        }
    }
}
