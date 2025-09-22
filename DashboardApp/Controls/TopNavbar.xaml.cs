
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

        private void UpdateActiveDropdown(string pageTag)
        {
            // Reset all dropdown styles to normal
            DashboardDropdown.Style = (Style)Resources["SubtleDropDownButtonStyle"];
            ActiveDirectoryDropdown.Style = (Style)Resources["SubtleDropDownButtonStyle"];
            AwsDropdown.Style = (Style)Resources["SubtleDropDownButtonStyle"];
            AzureDropdown.Style = (Style)Resources["SubtleDropDownButtonStyle"];
            ForensicsDropdown.Style = (Style)Resources["SubtleDropDownButtonStyle"];

            // Update icon colors to secondary
            UpdateDropdownIconColor(DashboardDropdown, false);
            UpdateDropdownIconColor(ActiveDirectoryDropdown, false);
            UpdateDropdownIconColor(AwsDropdown, false);
            UpdateDropdownIconColor(AzureDropdown, false);
            UpdateDropdownIconColor(ForensicsDropdown, false);

            // Set active dropdown based on page tag
            if (pageTag.StartsWith("dashboard"))
            {
                DashboardDropdown.Style = (Style)Resources["ActiveDropDownButtonStyle"];
                UpdateDropdownIconColor(DashboardDropdown, true);
            }
            else if (pageTag.StartsWith("ad") || pageTag == "active_directory")
            {
                ActiveDirectoryDropdown.Style = (Style)Resources["ActiveDropDownButtonStyle"];
                UpdateDropdownIconColor(ActiveDirectoryDropdown, true);
            }
            else if (pageTag.StartsWith("aws"))
            {
                AwsDropdown.Style = (Style)Resources["ActiveDropDownButtonStyle"];
                UpdateDropdownIconColor(AwsDropdown, true);
            }
            else if (pageTag.StartsWith("azure"))
            {
                AzureDropdown.Style = (Style)Resources["ActiveDropDownButtonStyle"];
                UpdateDropdownIconColor(AzureDropdown, true);
            }
            else if (pageTag.StartsWith("forensics"))
            {
                ForensicsDropdown.Style = (Style)Resources["ActiveDropDownButtonStyle"];
                UpdateDropdownIconColor(ForensicsDropdown, true);
            }
        }

        private void UpdateDropdownIconColor(DropDownButton dropdown, bool isActive)
        {
            if (dropdown.Content is StackPanel stackPanel)
            {
                foreach (var child in stackPanel.Children)
                {
                    if (child is FontIcon icon)
                    {
                        icon.Foreground = isActive ? 
                            (SolidColorBrush)Application.Current.Resources["BrandPrimaryBrush"] :
                            (SolidColorBrush)Application.Current.Resources["TextColorSecondaryBrush"];
                    }
                    else if (child is TextBlock textBlock)
                    {
                        textBlock.Foreground = isActive ?
                            (SolidColorBrush)Application.Current.Resources["BrandPrimaryBrush"] :
                            (SolidColorBrush)Application.Current.Resources["TextColorPrimaryBrush"];
                        textBlock.FontWeight = isActive ? FontWeights.SemiBold : FontWeights.Normal;
                    }
                }
            }
        }
    }
}
