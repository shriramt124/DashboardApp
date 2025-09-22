
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;

namespace DashboardApp.Views
{
    public sealed partial class LoginPage : Page
    {
        private bool _isPasswordVisible = false;
        private Dictionary<string, string> _mockUsers;
        private TextBox _visiblePasswordTextBox;

        public LoginPage()
        {
            this.InitializeComponent();
            InitializeMockUsers();
        }

        private void InitializeMockUsers()
        {
            _mockUsers = new Dictionary<string, string>
            {
                { "admin", "admin123" },
                { "user", "user123" },
                { "manager", "manager123" },
                { "guest", "guest123" }
            };
        }

        private void ShowPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            _isPasswordVisible = !_isPasswordVisible;
            
            if (_isPasswordVisible)
            {
                // Show password as text
                var password = PasswordTextBox.Password;
                PasswordTextBox.Visibility = Visibility.Collapsed;
                
                // Create a TextBox to show the password
                _visiblePasswordTextBox = new TextBox
                {
                    Background = PasswordTextBox.Background,
                    BorderThickness = PasswordTextBox.BorderThickness,
                    Foreground = PasswordTextBox.Foreground,
                    FontSize = PasswordTextBox.FontSize,
                    Text = password,
                    VerticalAlignment = PasswordTextBox.VerticalAlignment
                };
                
                // Find the grid and replace password box with text box
                var grid = PasswordTextBox.Parent as Grid;
                if (grid != null)
                {
                    Grid.SetColumn(_visiblePasswordTextBox, 1);
                    grid.Children.Add(_visiblePasswordTextBox);
                }
                
                ShowPasswordIcon.Glyph = "\uE8F4"; // Hide icon
            }
            else
            {
                // Hide password
                if (_visiblePasswordTextBox != null)
                {
                    var grid = PasswordTextBox.Parent as Grid;
                    if (grid != null)
                    {
                        PasswordTextBox.Password = _visiblePasswordTextBox.Text;
                        grid.Children.Remove(_visiblePasswordTextBox);
                    }
                }
                
                PasswordTextBox.Visibility = Visibility.Visible;
                ShowPasswordIcon.Glyph = "\uE7B3"; // Show icon
                _visiblePasswordTextBox = null;
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameTextBox.Text.Trim();
            var password = _isPasswordVisible && _visiblePasswordTextBox != null 
                ? _visiblePasswordTextBox.Text 
                : PasswordTextBox.Password;

            // Clear previous error
            ErrorMessageTextBlock.Visibility = Visibility.Collapsed;

            // Validate input
            if (string.IsNullOrEmpty(username))
            {
                ShowError("Please enter a username.");
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                ShowError("Please enter a password.");
                return;
            }

            // Check credentials against mock data
            if (_mockUsers.ContainsKey(username.ToLower()) && _mockUsers[username.ToLower()] == password)
            {
                // Successful login
                ShowSuccess();
                NavigateToMainApp();
            }
            else
            {
                ShowError("Invalid username or password.");
            }
        }

        private void ShowError(string message)
        {
            ErrorMessageTextBlock.Text = message;
            ErrorMessageTextBlock.Foreground = new Microsoft.UI.Xaml.Media.SolidColorBrush(
                Microsoft.UI.Colors.Red);
            ErrorMessageTextBlock.Visibility = Visibility.Visible;
        }

        private void ShowSuccess()
        {
            ErrorMessageTextBlock.Text = "Login successful! Redirecting...";
            ErrorMessageTextBlock.Foreground = new Microsoft.UI.Xaml.Media.SolidColorBrush(
                Microsoft.UI.Colors.Green);
            ErrorMessageTextBlock.Visibility = Visibility.Visible;
        }

        private async void NavigateToMainApp()
        {
            // Wait a moment to show success message
            await System.Threading.Tasks.Task.Delay(1500);

            // Navigate to the main shell
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(ShellPage));
            }
        }
    }
}
