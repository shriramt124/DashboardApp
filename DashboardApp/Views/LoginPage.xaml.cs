using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;

namespace DashboardApp.Views
{
    public sealed partial class LoginPage : Page
    {
        private bool _isPasswordVisible = false;
        private Dictionary<string, string> _mockUsers;

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
                var textBox = new TextBox
                {
                    Background = PasswordTextBox.Background,
                    BorderThickness = PasswordTextBox.BorderThickness,
                    Foreground = PasswordTextBox.Foreground,
                    FontSize = PasswordTextBox.FontSize,
                    Text = password,
                    VerticalAlignment = PasswordTextBox.VerticalAlignment,
                    Name = "VisiblePasswordTextBox"
                };
                
                // Find the grid and replace password box with text box
                var grid = PasswordTextBox.Parent as Grid;
                if (grid != null)
                {
                    Grid.SetColumn(textBox, 1);
                    grid.Children.Add(textBox);
                }
                
                ShowPasswordIcon.Glyph = "\uE8F4"; // Hide icon
            }
            else
            {
                // Hide password
                var grid = PasswordTextBox.Parent as Grid;
                if (grid != null)
                {
                    // Find and remove the visible text box
                    for (int i = grid.Children.Count - 1; i >= 0; i--)
                    {
                        if (grid.Children[i] is TextBox textBox && textBox.Name == "VisiblePasswordTextBox")
                        {
                            PasswordTextBox.Password = textBox.Text;
                            grid.Children.RemoveAt(i);
                            break;
                        }
                    }
                }
                
                PasswordTextBox.Visibility = Visibility.Visible;
                ShowPasswordIcon.Glyph = "\uE7B3"; // Show icon
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameTextBox.Text.Trim();
            var password = PasswordTextBox.Password;

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

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to register page (to be created later)
            // For now, show a message
            ShowError("Registration feature coming soon!");
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
