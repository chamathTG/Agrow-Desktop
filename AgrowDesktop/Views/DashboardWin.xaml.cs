using AgrowDesktop.Models;
using AgrowDesktop.Services;
using Google.Protobuf;
using Microsoft.Win32;
using System.IO;
using System.Linq;
using System.Windows;

namespace AgrowDesktop.Views
{
    public partial class DashboardWin : Window
    {
        private DbService db = new DbService();

        // Local cache
        private List<UserModel> _users = new();
        private List<ProductModel> _products = new();
        private List<OrderModel> _orders = new();

        public DashboardWin()
        {
            InitializeComponent();
        }

        // CUSTOM MESSAGE
        private void ShowMsg(string text, bool success = true)
        {
            MessageWin message = new MessageWin();

            message.ColTxtHandler(text, success);
            message.OpacityHandler(this);
            message.ShowDialog();
        }

        // ─────────────────────────────────────────────
        // WINDOW LOADED
        // ─────────────────────────────────────────────
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadAllData();
        }

        // ─────────────────────────────────────────────
        // LOAD ALL DATA
        // ─────────────────────────────────────────────
        private void LoadAllData()
        {
            _users = db.GetAllUsers();
            _products = db.GetAllProducts();
            _orders = db.GetAllOrders();

            UsersGrid.ItemsSource = _users;
            ProductsGrid.ItemsSource = _products;
            OrdersGrid.ItemsSource = _orders;

            UpdateStatCards();
            UpdatePendingBadge();
        }

        // ─────────────────────────────────────────────
        // STAT CARDS
        // ─────────────────────────────────────────────
        private void UpdateStatCards()
        {
            if (StatTotalUsers != null)
                StatTotalUsers.Text = _users.Count.ToString();

            if (StatTotalProducts != null)
                StatTotalProducts.Text = _products.Count.ToString();

            if (StatTotalOrders != null)
                StatTotalOrders.Text = _orders.Count.ToString();

            int pending = _orders.Count(o => o.Status == "Pending");

            if (StatPendingOrders != null)
                StatPendingOrders.Text = pending.ToString();
        }

        // ─────────────────────────────────────────────
        // PENDING BADGE
        // ─────────────────────────────────────────────
        private void UpdatePendingBadge()
        {
            if (PendingBadge == null || PendingBadgeText == null)
                return;

            int pending = _orders.Count(o => o.Status == "Pending");

            if (pending > 0)
            {
                PendingBadgeText.Text = pending.ToString();
                PendingBadge.Visibility = Visibility.Visible;
            }
            else
            {
                PendingBadge.Visibility = Visibility.Collapsed;
            }
        }

        // ─────────────────────────────────────────────
        // USERS
        // ─────────────────────────────────────────────
        private void LoadUsers_Click(object sender, RoutedEventArgs e)
        {
            _users = db.GetAllUsers();

            UsersGrid.ItemsSource = _users;

            UpdateStatCards();
        }

        private void ToggleBlock_Click(object sender, RoutedEventArgs e)
        {
            if (UsersGrid.SelectedItem is UserModel user)
            {
                string action = user.IsBlocked ? "Unblock" : "Block";

                var result = MessageBox.Show(
                    $"Are you sure you want to {action} user '{user.Username}'?",
                    "Confirm Action",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    db.ToggleBlockUser(user.Id, !user.IsBlocked);

                    LoadAllData();

                    ShowMsg($"User {action.ToLower()}ed successfully.");
                }
            }
            else
            {
                ShowMsg("Please select a user first.", false);
            }
        }

        // ─────────────────────────────────────────────
        // PRODUCTS
        // ─────────────────────────────────────────────
        private void LoadProducts_Click(object sender, RoutedEventArgs e)
        {
            _products = db.GetAllProducts();

            ProductsGrid.ItemsSource = _products;

            UpdateStatCards();
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsGrid.SelectedItem is ProductModel product)
            {
                var result = MessageBox.Show(
                    $"Delete product '{product.Title}'?\n\nThis action cannot be undone.",
                    "Confirm Delete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    db.DeleteProduct(product.Id);

                    LoadAllData();

                    ShowMsg("Product deleted successfully.");
                }
            }
            else
            {
                ShowMsg("Please select a product first.", false);
            }
        }

        // ─────────────────────────────────────────────
        // ORDERS
        // ─────────────────────────────────────────────
        private void LoadOrders_Click(object sender, RoutedEventArgs e)
        {
            _orders = db.GetAllOrders();

            OrdersGrid.ItemsSource = _orders;

            UpdateStatCards();
            UpdatePendingBadge();
        }

        private void UpdateOrderStatus_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersGrid.SelectedItem is OrderModel order &&
                StatusBox.SelectedItem is System.Windows.Controls.ComboBoxItem item)
            {
                string status = item.Content?.ToString() ?? "";

                if (string.IsNullOrWhiteSpace(status))
                {
                    ShowMsg("Invalid status selected.", false);
                    return;
                }

                db.UpdateOrderStatus(order.Id, status);

                LoadAllData();

                ShowMsg("Order status updated successfully.");
            }
            else
            {
                ShowMsg("Please select an order and status.", false);
            }
        }

        // ─────────────────────────────────────────────
        // EXPORT CSV
        // ─────────────────────────────────────────────
        private void SaveCsv(string defaultFileName, string csvContent)
        {
            var dialog = new SaveFileDialog
            {
                FileName = defaultFileName,
                DefaultExt = ".csv",
                Filter = "CSV files (*.csv)|*.csv"
            };

            if (dialog.ShowDialog() == true)
            {
                File.WriteAllText(dialog.FileName, csvContent);

                ShowMsg("Exported successfully!");
            }
        }

        private void ExportUsers_Click(object sender, RoutedEventArgs e)
        {
            var lines = new List<string>
            {
                "ID,Role,Username,Mobile,Blocked"
            };

            foreach (var u in _users)
            {
                lines.Add($"{u.Id},{u.Role},{u.Username},{u.Mobile},{u.IsBlocked}");
            }

            SaveCsv("Users_Export.csv", string.Join("\n", lines));
        }

        private void ExportProducts_Click(object sender, RoutedEventArgs e)
        {
            var lines = new List<string>
            {
                "ID,Farmer,Title,Price"
            };

            foreach (var p in _products)
            {
                lines.Add($"{p.Id},{p.FarmerName},\"{p.Title}\",{p.Price}");
            }

            SaveCsv("Products_Export.csv", string.Join("\n", lines));
        }

        private void ExportOrders_Click(object sender, RoutedEventArgs e)
        {
            var lines = new List<string>
            {
                "ID,Customer,Farmer,Product,Qty,Total,Status"
            };

            foreach (var o in _orders)
            {
                lines.Add($"{o.Id},{o.CustomerName},{o.FarmerName},\"{o.ProductTitle}\",{o.Qty},{o.Total},{o.Status}");
            }

            SaveCsv("Orders_Export.csv", string.Join("\n", lines));
        }

        // ─────────────────────────────────────────────
        // WINDOW BUTTONS
        // ─────────────────────────────────────────────
        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TopBar_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}