
using DashboardApp.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace DashboardApp.Controls
{
    public sealed partial class TopNavbar : UserControl
    {
        public ShellViewModel? ViewModel { get; set; }

        public TopNavbar()
        {
            this.InitializeComponent();
        }

        private void OnMenuItemClick(object sender, RoutedEventArgs e)
        {
            if (sender is MenuFlyoutItem menuItem && menuItem.Tag is string pageTag)
            {
                ViewModel?.Navigate(pageTag);
                UpdateActiveDropdown(pageTag);
            }
        }

        private void OnSearchQuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion is SearchResult searchResult)
            {
                ViewModel?.Navigate(searchResult.PageTag);
                ViewModel?.ClearSearch();
            }
            else if (!string.IsNullOrEmpty(args.QueryText))
            {
                // Perform global search with the query text
                ViewModel?.PerformGlobalSearch(args.QueryText);
            }
        }

        private void OnSearchTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                ViewModel?.UpdateSearchSuggestions(sender.Text);
            }
        }

        private void OnSearchSuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            if (args.SelectedItem is SearchResult searchResult)
            {
                sender.Text = searchResult.DisplayName;
            }
        }

        private void OnUserMenuClick(object sender, RoutedEventArgs e)
        {
            if (sender is MenuFlyoutItem menuItem && menuItem.Tag is string action)
            {
                switch (action)
                {
                    case "profile":
                        // Navigate to profile settings
                        break;
                    case "account":
                        // Navigate to account settings
                        break;
                    case "notifications":
                        // Navigate to notifications
                        break;
                    case "signout":
                        // Handle sign out
                        break;
                }
            }
        }

        private void UpdateActiveDropdown(string pageTag)
        {
            // Reset all dropdown styles to normal
            DashboardDropdown.Style = (Style)Resources["ModernDropDownButtonStyle"];
            ActiveDirectoryDropdown.Style = (Style)Resources["ModernDropDownButtonStyle"];
            AwsDropdown.Style = (Style)Resources["ModernDropDownButtonStyle"];
            AzureDropdown.Style = (Style)Resources["ModernDropDownButtonStyle"];
            ForensicsDropdown.Style = (Style)Resources["ModernDropDownButtonStyle"];

            // Update colors to default
            UpdateDropdownColors(DashboardDropdown, false);
            UpdateDropdownColors(ActiveDirectoryDropdown, false);
            UpdateDropdownColors(AwsDropdown, false);
            UpdateDropdownColors(AzureDropdown, false);
            UpdateDropdownColors(ForensicsDropdown, false);

            // Set active dropdown based on page tag
            if (pageTag.StartsWith("dashboard"))
            {
                DashboardDropdown.Style = (Style)Resources["ActiveDropDownButtonStyle"];
                UpdateDropdownColors(DashboardDropdown, true);
            }
            else if (pageTag.StartsWith("ad") || pageTag == "active_directory")
            {
                ActiveDirectoryDropdown.Style = (Style)Resources["ActiveDropDownButtonStyle"];
                UpdateDropdownColors(ActiveDirectoryDropdown, true);
            }
            else if (pageTag.StartsWith("aws"))
            {
                AwsDropdown.Style = (Style)Resources["ActiveDropDownButtonStyle"];
                UpdateDropdownColors(AwsDropdown, true);
            }
            else if (pageTag.StartsWith("azure"))
            {
                AzureDropdown.Style = (Style)Resources["ActiveDropDownButtonStyle"];
                UpdateDropdownColors(AzureDropdown, true);
            }
            else if (pageTag.StartsWith("forensics"))
            {
                ForensicsDropdown.Style = (Style)Resources["ActiveDropDownButtonStyle"];
                UpdateDropdownColors(ForensicsDropdown, true);
            }
        }

        private void UpdateDropdownColors(DropDownButton dropdown, bool isActive)
        {
            if (dropdown.Content is StackPanel stackPanel)
            {
                foreach (var child in stackPanel.Children)
                {
                    if (child is FontIcon icon)
                    {
                        icon.Foreground = new SolidColorBrush(isActive ? 
                            Windows.UI.Color.FromArgb(255, 88, 166, 255) : // #58A6FF
                            Windows.UI.Color.FromArgb(255, 240, 246, 252)); // #F0F6FC
                    }
                    else if (child is TextBlock textBlock)
                    {
                        textBlock.Foreground = new SolidColorBrush(isActive ?
                            Windows.UI.Color.FromArgb(255, 88, 166, 255) : // #58A6FF
                            Windows.UI.Color.FromArgb(255, 240, 246, 252)); // #F0F6FC
                        textBlock.FontWeight = isActive ? FontWeight : FontWeight;
                    }
                }
            }
        }
    }
}
