using AgrowDesktop.Models;
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
    /// Interaction logic for MainWin.xaml
    /// </summary>
    public partial class MainWin : Window
    {
        public MainWin()
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

        //LoadCrateNewPage
        private void Create_New(object sender, MouseButtonEventArgs e)
        {
            CreateAccWin win = new CreateAccWin();
            win.Show();

            this.Close();
        }

        //PasswordShowHide
        private bool isPasswordVisible = false;

        private void ShowPassword_Click(object sender, RoutedEventArgs e)
        {
            if (isPasswordVisible)
            {
                PasswordHidden.Password = PasswordVisible.Text;

                PasswordHidden.Visibility = Visibility.Visible;
                PasswordVisible.Visibility = Visibility.Collapsed;

                isPasswordVisible = false;
            }
            else
            {
                PasswordVisible.Text = PasswordHidden.Password;

                PasswordVisible.Visibility = Visibility.Visible;
                PasswordHidden.Visibility = Visibility.Collapsed;

                isPasswordVisible = true;
            }
        }

        //LoadLoginBtn
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string password = PasswordHidden.Visibility == Visibility.Visible
        ? PasswordHidden.Password
        : PasswordVisible.Text;

            if (string.IsNullOrWhiteSpace(UsernameBox.Text) ||
                string.IsNullOrWhiteSpace(password))
            {
                new MessageWin("Please fill all fields before login!", false).ShowDialog();
                return;
            }

            var vm = new LoginViewModel();

            AdminModel admin = vm.Login(UsernameBox.Text, password);

            if (admin != null)
            {
                new MessageWin("Login successful!", true).ShowDialog();

                DashboardWin dash = new DashboardWin();

                //WeCan Pass UsernameToDashWin
                // dash.UserText.Text = admin.Username;

                dash.Show();

                this.Close();
            }
            else
            {
                new MessageWin("Invalid username or password!", false).ShowDialog();

                UsernameBox.Clear();
                PasswordHidden.Clear();
                PasswordVisible.Clear();
            }
        }

        //ForogotPssClick
        private void Forogot_Pass(object sender, RoutedEventArgs e)
        {
            ForgetPassWin win = new ForgetPassWin();
            win.Show();
        }
    }
}
