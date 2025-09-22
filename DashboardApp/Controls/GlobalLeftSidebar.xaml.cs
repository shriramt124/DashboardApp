// Controls/GlobalLeftSidebar.xaml.cs
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using DashboardApp.ViewModels;

namespace DashboardApp.Controls;

public sealed partial class GlobalLeftSidebar : UserControl
{
    public ShellViewModel ViewModel
    {
        get { return (ShellViewModel)GetValue(ViewModelProperty); }
        set { SetValue(ViewModelProperty, value); }
    }

    public static readonly DependencyProperty ViewModelProperty =
        DependencyProperty.Register("ViewModel", typeof(ShellViewModel), typeof(GlobalLeftSidebar), new PropertyMetadata(null));

    public GlobalLeftSidebar()
    {
        this.InitializeComponent();
    }

    private void OnDashboardsClick(object sender, RoutedEventArgs e)
    {
        ViewModel?.Navigate("dashboard_overview");
        UpdateToggleStates("dashboards");
    }

    private void OnDataManagementClick(object sender, RoutedEventArgs e)
    {
        ViewModel?.Navigate("active_directory");
        UpdateToggleStates("data_management");
    }

    private void OnAnalyticsClick(object sender, RoutedEventArgs e)
    {
        ViewModel?.Navigate("dashboard_analytics");
        UpdateToggleStates("analytics");
    }

    private void OnSecurityClick(object sender, RoutedEventArgs e)
    {
        ViewModel?.Navigate("forensics_home");
        UpdateToggleStates("security");
    }

    private void OnReportsClick(object sender, RoutedEventArgs e)
    {
        ViewModel?.Navigate("forensics_reports");
        UpdateToggleStates("reports");
    }

    private void OnToolsClick(object sender, RoutedEventArgs e)
    {
        ViewModel?.Navigate("aws_home");
        UpdateToggleStates("tools");
    }

    private void OnSettingsClick(object sender, RoutedEventArgs e)
    {
        // Settings functionality can be added later
        UpdateToggleStates("settings");
    }

    private void OnHelpClick(object sender, RoutedEventArgs e)
    {
        // Help functionality can be added later
        UpdateToggleStates("help");
    }

    private void UpdateToggleStates(string activeButton)
    {
        // Reset all buttons
        DashboardsButton.IsChecked = false;
        DataManagementButton.IsChecked = false;
        AnalyticsButton.IsChecked = false;
        SecurityButton.IsChecked = false;
        ReportsButton.IsChecked = false;
        ToolsButton.IsChecked = false;
        SettingsButton.IsChecked = false;
        HelpButton.IsChecked = false;

        // Set the active button
        switch (activeButton)
        {
            case "dashboards":
                DashboardsButton.IsChecked = true;
                break;
            case "data_management":
                DataManagementButton.IsChecked = true;
                break;
            case "analytics":
                AnalyticsButton.IsChecked = true;
                break;
            case "security":
                SecurityButton.IsChecked = true;
                break;
            case "reports":
                ReportsButton.IsChecked = true;
                break;
            case "tools":
                ToolsButton.IsChecked = true;
                break;
            case "settings":
                SettingsButton.IsChecked = true;
                break;
            case "help":
                HelpButton.IsChecked = true;
                break;
        }
    }
}