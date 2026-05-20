using AgrowDesktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

        private void PasswordHidden_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PasswordVisible.Visibility == Visibility.Visible)
            {
                PasswordVisible.Text = PasswordHidden.Password;
            }
        }

        private void ShowPassword_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordHidden.Visibility == Visibility.Visible)
            {
                //ShowPss
                PasswordVisible.Text = PasswordHidden.Password;

                PasswordHidden.Visibility = Visibility.Collapsed;
                PasswordVisible.Visibility = Visibility.Visible;
            }
            else
            {
                //HidePass
                PasswordHidden.Password = PasswordVisible.Text;

                PasswordVisible.Visibility = Visibility.Collapsed;
                PasswordHidden.Visibility = Visibility.Visible;
            }
        }

        //LoadLoginWin
        private void Login_Win(object sender, RoutedEventArgs e)
        {
            MainWin win = new MainWin();
            win.Show();
            this.Close();
        }

        //SignUpBtn
        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            string password = PasswordHidden.Visibility == Visibility.Visible
            ? PasswordHidden.Password
            : PasswordVisible.Text;

            // CheckEmptyFields
            if (string.IsNullOrWhiteSpace(NicBox.Text) ||
                string.IsNullOrWhiteSpace(UserNBox.Text) ||
                string.IsNullOrWhiteSpace(EmailBox.Text) ||
                string.IsNullOrWhiteSpace(password))
            {
                new MessageWin("Please fill all fields to complete sign up!", false).ShowDialog();
                return;
            }

            var vm = new CreateAccViewModel();

            string result = vm.SignUp(
                NicBox.Text,
                UserNBox.Text,
                password,
                EmailBox.Text
            );

            bool success = result.ToLower().Contains("success");

            //ShowMsg
            new MessageWin(result, success).ShowDialog();

            //ClearAll
            NicBox.Clear();
            UserNBox.Clear();
            EmailBox.Clear();
            PasswordHidden.Clear();
            PasswordVisible.Clear();

            //LoadLogin
            if (success)
            {
                MainWin loginWin = new MainWin();
                loginWin.Show();

                this.Close();
            }

        }
    }
}
