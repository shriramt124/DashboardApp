
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DashboardApp.Views;
using System;
using System.Collections.Generic;

namespace DashboardApp.ViewModels;

public abstract class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

public class NavigationItem
{
    public string Name { get; set; } = string.Empty;
    public string PageTag { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
}

public class ShellViewModel : BaseViewModel
{
    // Navigation collections for dropdowns
    public ObservableCollection<NavigationItem> DashboardItems { get; } = new()
    {
        new NavigationItem { Name = "Dashboards", PageTag = "dashboard_home", Icon = "&#xE74C;" },
        new NavigationItem { Name = "Overview", PageTag = "dashboard_overview", Icon = "&#xE7C3;" },
        new NavigationItem { Name = "Analytics", PageTag = "dashboard_analytics", Icon = "&#xE9D9;" }
    };

    public ObservableCollection<NavigationItem> ActiveDirectoryItems { get; } = new()
    {
        new NavigationItem { Name = "Active Directory", PageTag = "active_directory", Icon = "&#xE716;" },
        new NavigationItem { Name = "Users", PageTag = "ad_users", Icon = "&#xE77B;" },
        new NavigationItem { Name = "Groups", PageTag = "ad_groups", Icon = "&#xE902;" },
        new NavigationItem { Name = "Computers", PageTag = "ad_computers", Icon = "&#xE7F8;" },
        new NavigationItem { Name = "Domains", PageTag = "ad_domains", Icon = "&#xE968;" },
        new NavigationItem { Name = "Forest", PageTag = "ad_forest", Icon = "&#xE8A5;" }
    };

    public ObservableCollection<NavigationItem> AwsItems { get; } = new()
    {
        new NavigationItem { Name = "AWS", PageTag = "aws_home", Icon = "&#xE753;" },
        new NavigationItem { Name = "EC2", PageTag = "aws_ec2", Icon = "&#xE7F8;" },
        new NavigationItem { Name = "S3", PageTag = "aws_s3", Icon = "&#xE8B7;" },
        new NavigationItem { Name = "RDS", PageTag = "aws_rds", Icon = "&#xE74E;" },
        new NavigationItem { Name = "Lambda", PageTag = "aws_lambda", Icon = "&#xE945;" }
    };

    public ObservableCollection<NavigationItem> AzureItems { get; } = new()
    {
        new NavigationItem { Name = "Azure", PageTag = "azure_home", Icon = "&#xE753;" },
        new NavigationItem { Name = "Virtual Machines", PageTag = "azure_vms", Icon = "&#xE7F8;" },
        new NavigationItem { Name = "Storage", PageTag = "azure_storage", Icon = "&#xE8B7;" },
        new NavigationItem { Name = "SQL Database", PageTag = "azure_sql", Icon = "&#xE74E;" },
        new NavigationItem { Name = "Functions", PageTag = "azure_functions", Icon = "&#xE945;" }
    };

    public ObservableCollection<NavigationItem> DigitalForensicsItems { get; } = new()
    {
        new NavigationItem { Name = "Digital Forensics", PageTag = "forensics_home", Icon = "&#xE720;" },
        new NavigationItem { Name = "Cases", PageTag = "forensics_cases", Icon = "&#xE7C5;" },
        new NavigationItem { Name = "Evidence", PageTag = "forensics_evidence", Icon = "&#xE8B9;" },
        new NavigationItem { Name = "Analysis", PageTag = "forensics_analysis", Icon = "&#xE9D9;" },
        new NavigationItem { Name = "Reports", PageTag = "forensics_reports", Icon = "&#xE74C;" }
    };

    // Selected items for tracking current selection
    private NavigationItem? _selectedDashboardItem;
    public NavigationItem? SelectedDashboardItem
    {
        get => _selectedDashboardItem;
        set { _selectedDashboardItem = value; OnPropertyChanged(); }
    }

    private NavigationItem? _selectedActiveDirectoryItem;
    public NavigationItem? SelectedActiveDirectoryItem
    {
        get => _selectedActiveDirectoryItem;
        set { _selectedActiveDirectoryItem = value; OnPropertyChanged(); }
    }

    private NavigationItem? _selectedAwsItem;
    public NavigationItem? SelectedAwsItem
    {
        get => _selectedAwsItem;
        set { _selectedAwsItem = value; OnPropertyChanged(); }
    }

    private NavigationItem? _selectedAzureItem;
    public NavigationItem? SelectedAzureItem
    {
        get => _selectedAzureItem;
        set { _selectedAzureItem = value; OnPropertyChanged(); }
    }

    private NavigationItem? _selectedForensicsItem;
    public NavigationItem? SelectedForensicsItem
    {
        get => _selectedForensicsItem;
        set { _selectedForensicsItem = value; OnPropertyChanged(); }
    }

    // Search functionality
    private string _topSearchQuery = string.Empty;
    public string TopSearchQuery
    {
        get => _topSearchQuery;
        set { _topSearchQuery = value; OnPropertyChanged(); }
    }

    // Current page tracking
    private string _currentPageTag = "active_directory";
    public string CurrentPageTag
    {
        get => _currentPageTag;
        set { _currentPageTag = value; OnPropertyChanged(); }
    }

    public Frame? ContentFrame { get; set; }

    public ShellViewModel()
    {
        // Set initial selected items
        SelectedDashboardItem = DashboardItems[0];
        SelectedActiveDirectoryItem = ActiveDirectoryItems[0];
        SelectedAwsItem = AwsItems[0];
        SelectedAzureItem = AzureItems[0];
        SelectedForensicsItem = DigitalForensicsItems[0];
    }

    public void Navigate(string pageTag)
    {
        if (ContentFrame == null) return;

        Type? pageType = GetPageType(pageTag);
        //if the source page type is not the same as the page type, navigate to the new page
        if (pageType != null && ContentFrame.SourcePageType != pageType)
        {
            ContentFrame.Navigate(pageType);
            CurrentPageTag = pageTag;
        }
    }

    private Type? GetPageType(string pageTag)
    {
        //this function just return the page tag that we are using 
        return pageTag switch
        {
            // Dashboard pages
            "dashboard_home" => typeof(HomePage),
            "dashboard_overview" => typeof(DashboardOverviewPage),
            "dashboard_analytics" => typeof(DashboardAnalyticsPage),
            
            // Active Directory pages
            "active_directory" => typeof(ActiveDirectoryPage),
            "ad_users" => typeof(AdUsersPage),
            "ad_groups" => typeof(AdGroupsPage),
            "ad_computers" => typeof(AdComputersPage),
            "ad_domains" => typeof(AdDomainsPage),
            "ad_forest" => typeof(AdForestPage),
            
            // AWS pages
            "aws_home" => typeof(AwsHomePage),
            "aws_ec2" => typeof(AwsEc2Page),
            "aws_s3" => typeof(AwsS3Page),
            "aws_rds" => typeof(AwsRdsPage),
            "aws_lambda" => typeof(AwsLambdaPage),
            
            // Azure pages
            "azure_home" => typeof(AzureHomePage),
            "azure_vms" => typeof(AzureVmsPage),
            "azure_storage" => typeof(AzureStoragePage),
            "azure_sql" => typeof(AzureSqlPage),
            "azure_functions" => typeof(AzureFunctionsPage),
            
            // Digital Forensics pages
            "forensics_home" => typeof(ForensicsHomePage),
            "forensics_cases" => typeof(ForensicsCasesPage),
            "forensics_evidence" => typeof(ForensicsEvidencePage),
            "forensics_analysis" => typeof(ForensicsAnalysisPage),
            "forensics_reports" => typeof(ForensicsReportsPage),
            //default me home page return kar do 
            _ => typeof(HomePage)
        };
    }
}
