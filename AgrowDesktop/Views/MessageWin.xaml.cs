using System.Windows;
using System.Windows.Media;

namespace AgrowDesktop.Views
{
    public partial class MessageWin : Window
    {
        public MessageWin()
        {
            InitializeComponent();
        }

        public void ColTxtHandler(string message, bool isSuccess = true)
        {
            InitializeComponent();

            MessageText.Text = message;

            if(isSuccess)
            {
                MessageText.Foreground = Brushes.ForestGreen;
            }
            else
            {
                MessageText.Foreground = Brushes.Red;
            }
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // This method handle the opacitiy change with loading this window
        public void OpacityHandler(Window msgOwner)
        {
            this.Owner = msgOwner;

            this.Loaded += (s, e) =>
            {
                msgOwner.Opacity = 0.5;
            };

            this.Closed += (s, e) =>
            {
                msgOwner.Opacity = 1;
            };
        }
    }
}
