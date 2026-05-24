using AgrowDesktop.ViewModels;
using Google.Protobuf;
using System.Windows;

namespace AgrowDesktop.Views
{
    public partial class ForgetPassWin : Window
    {
        public ForgetPassWin()
        {
            InitializeComponent();

            this.Loaded += (s, e) =>
            {
                if(this.Owner != null)
                {
                    this.Owner.Opacity = 0.5;
                }
            };

            this.Closed += (s, e) =>
            {
                if (this.Owner != null)
                {
                    this.Owner.Opacity = 1;
                }
            };
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ResetPassword_Click(object sender, RoutedEventArgs e)
        {
            MessageWin message = new MessageWin();

            if (string.IsNullOrWhiteSpace(NicBox.Text) || string.IsNullOrWhiteSpace(NewPasswordBox.Password))
            {
                message.ColTxtHandler("Please fill all the fields!", false);
                message.OpacityHandler(this);
                message.ShowDialog();

                return;
            }

            var vm = new ForgotPassViewModel();

            string result = vm.ResetPassword(NicBox.Text, NewPasswordBox.Password);

            bool success = result.ToLower().Contains("success");

            message.ColTxtHandler(result, success);
            message.OpacityHandler(this);
            message.ShowDialog();

            NicBox.Clear();
            NewPasswordBox.Clear();

            if(success)
            {
                this.Close();
            }
        }
    }
}
