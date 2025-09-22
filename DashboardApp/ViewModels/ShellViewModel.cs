// ViewModels/ShellViewModel.cs
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DashboardApp.Views;
using System; // Needed for page types
using System.Collections.Generic;

namespace DashboardApp.ViewModels;

// Base class for INotifyPropertyChanged
public abstract class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

public class ShellViewModel : BaseViewModel
{
    // For the Top Navbar dropdowns
    public ObservableCollection<string> Dashboards { get; } = new() { "Dashboards", "Overview", "Analytics" };
    public ObservableCollection<string> ActiveDirectoryItems { get; } = new() { "Active Directory", "Users", "Groups" };
    public ObservableCollection<string> AwsItems { get; } = new() { "AWS", "EC2", "S3" };
    public ObservableCollection<string> AzureItems { get; } = new() { "Azure", "VMs", "Storage" };

    private string? _selectedDashboard;
    public string? SelectedDashboard
    {
        get => _selectedDashboard;
        set { _selectedDashboard = value; OnPropertyChanged(); }
    }

    private string? _selectedActiveDirectoryItem;
    public string? SelectedActiveDirectoryItem
    {
        get => _selectedActiveDirectoryItem;
        set { _selectedActiveDirectoryItem = value; OnPropertyChanged(); }
    }

    private string? _selectedAwsItem;
    public string? SelectedAwsItem
    {
        get => _selectedAwsItem;
        set { _selectedAwsItem = value; OnPropertyChanged(); }
    }

    private string? _selectedAzureItem;
    public string? SelectedAzureItem
    {
        get => _selectedAzureItem;
        set { _selectedAzureItem = value; OnPropertyChanged(); }
    }

    // For the Top NavigationView's search box.
    private string _topSearchQuery = string.Empty;
    public string TopSearchQuery
    {
        get => _topSearchQuery;
        set { _topSearchQuery = value; OnPropertyChanged(); }
    }

    // This will hold the reference to the Frame in ShellPage for navigation.
    // In a more complex app, you might use a dedicated NavigationService.
    public Frame? ContentFrame { get; set; }

    public ShellViewModel()
    {
        // Set initial selected items
        SelectedDashboard = Dashboards[0];
        SelectedActiveDirectoryItem = ActiveDirectoryItems[0];
        SelectedAwsItem = AwsItems[0];
        SelectedAzureItem = AzureItems[0];
    }

    // This method will be called from ShellPage's code-behind to handle navigation
    public void Navigate(string pageTag)
    {
        if (ContentFrame == null) return;

        Type? pageType = null;
        switch (pageTag)
        {
            case "dashboard_home":
                pageType = typeof(HomePage);
                break;
            case "active_directory":
                pageType = typeof(ActiveDirectoryPage);
                break;
            // Add other cases for your main content pages
            default:
                pageType = typeof(HomePage); // Default to home
                break;
        }

        // Fix: Ensure pageType is not null before calling Navigate, and cast to non-nullable Type
        // Safe comparison: cast pageType to non-nullable Type
        if ((Type)pageType !=null && ContentFrame.SourcePageType != (Type)pageType)
        {
            ContentFrame.Navigate((Type)pageType);
        }
    }
}