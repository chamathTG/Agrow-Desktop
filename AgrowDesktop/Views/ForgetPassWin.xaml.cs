using AgrowDesktop.ViewModels;
using System.Windows;

namespace AgrowDesktop.Views
{
    /// <summary>
    /// Interaction logic for ForgetPassWin.xaml
    /// </summary>
    public partial class ForgetPassWin : Window
    {
        public ForgetPassWin()
        {
            InitializeComponent();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ResetPassword_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NicBox.Text) ||
                string.IsNullOrWhiteSpace(NewPasswordBox.Password))
            {
                new MessageWin("Please fill all fields!", false).ShowDialog();
                return;
            }

            var vm = new ForgotPassViewModel();

            string result = vm.ResetPassword(
                NicBox.Text,
                NewPasswordBox.Password
            );

            bool success = result.ToLower().Contains("success");

            new MessageWin(result, success).ShowDialog();

            NicBox.Clear();
            NewPasswordBox.Clear();

            if (success)
            {
                this.Close();
            }
        }
    }
}
