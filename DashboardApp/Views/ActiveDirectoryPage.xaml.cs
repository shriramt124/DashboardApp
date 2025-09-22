// Views/ActiveDirectoryPage.xaml.cs
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using System;

namespace DashboardApp.Views;

public sealed partial class ActiveDirectoryPage : Page
{
    public ActiveDirectoryPage()
    {
        this.InitializeComponent();
        
        // Wire up event handlers
        SearchTextBox.TextChanged += SearchTextBox_TextChanged;
        DomainFilterComboBox.SelectionChanged += DomainFilterComboBox_SelectionChanged;
        StatusFilterComboBox.SelectionChanged += StatusFilterComboBox_SelectionChanged;
        RefreshButton.Click += RefreshButton_Click;
        
        // Set default selections
        DomainFilterComboBox.SelectedIndex = 0; // All Domains
        StatusFilterComboBox.SelectedIndex = 0; // All Status
    }
    
    private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        // Handle search functionality
        var searchText = SearchTextBox.Text?.ToLower() ?? "";
        // TODO: Implement actual filtering logic when data binding is implemented
        System.Diagnostics.Debug.WriteLine($"Search text changed: {searchText}");
    }
    
    private void DomainFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Handle domain filter
        if (DomainFilterComboBox.SelectedItem is ComboBoxItem selectedItem)
        {
            var selectedDomain = selectedItem.Content?.ToString() ?? "";
            System.Diagnostics.Debug.WriteLine($"Domain filter changed: {selectedDomain}");
            // TODO: Implement actual filtering logic when data binding is implemented
        }
    }
    
    private void StatusFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Handle status filter
        if (StatusFilterComboBox.SelectedItem is ComboBoxItem selectedItem)
        {
            var selectedStatus = selectedItem.Content?.ToString() ?? "";
            System.Diagnostics.Debug.WriteLine($"Status filter changed: {selectedStatus}");
            // TODO: Implement actual filtering logic when data binding is implemented
        }
    }
    
    private void RefreshButton_Click(object sender, RoutedEventArgs e)
    {
        // Handle refresh functionality
        System.Diagnostics.Debug.WriteLine("Refresh button clicked");
        // TODO: Implement actual data refresh logic
        
        // Visual feedback for refresh
        RefreshButton.IsEnabled = false;
        var timer = new DispatcherTimer();
        timer.Interval = TimeSpan.FromSeconds(1);
        timer.Tick += (s, args) => {
            RefreshButton.IsEnabled = true;
            timer.Stop();
        };
        timer.Start();
    }
}