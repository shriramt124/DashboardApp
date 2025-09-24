
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DashboardApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;

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

public class SearchResult
{
    public string DisplayName { get; set; } = string.Empty;
    public string PageTag { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
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

    private ObservableCollection<SearchResult> _searchSuggestions = new();
    public ObservableCollection<SearchResult> SearchSuggestions
    {
        get => _searchSuggestions;
        set { _searchSuggestions = value; OnPropertyChanged(); }
    }

    // Master list of all searchable items
    private readonly List<SearchResult> _allSearchableItems = new();

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

        // Initialize search items
        InitializeSearchableItems();
    }

    private void InitializeSearchableItems()
    {
        _allSearchableItems.Clear();

        // Add dashboard items
        _allSearchableItems.Add(new SearchResult { DisplayName = "Dashboard Overview", PageTag = "dashboard_overview", Category = "Dashboard", Icon = "&#xE7C3;", Description = "View system overview and key metrics" });
        _allSearchableItems.Add(new SearchResult { DisplayName = "Analytics", PageTag = "dashboard_analytics", Category = "Dashboard", Icon = "&#xE9D9;", Description = "Analyze system performance and trends" });

        // Add Active Directory items
        _allSearchableItems.Add(new SearchResult { DisplayName = "Active Directory", PageTag = "active_directory", Category = "Active Directory", Icon = "&#xE716;", Description = "Manage Active Directory services" });
        _allSearchableItems.Add(new SearchResult { DisplayName = "AD Users", PageTag = "ad_users", Category = "Active Directory", Icon = "&#xE77B;", Description = "Manage user accounts and permissions" });
        _allSearchableItems.Add(new SearchResult { DisplayName = "AD Groups", PageTag = "ad_groups", Category = "Active Directory", Icon = "&#xE902;", Description = "Manage security and distribution groups" });
        _allSearchableItems.Add(new SearchResult { DisplayName = "AD Computers", PageTag = "ad_computers", Category = "Active Directory", Icon = "&#xE7F8;", Description = "Manage computer accounts and policies" });
        _allSearchableItems.Add(new SearchResult { DisplayName = "AD Domains", PageTag = "ad_domains", Category = "Active Directory", Icon = "&#xE968;", Description = "Manage domain configuration and trust relationships" });
        _allSearchableItems.Add(new SearchResult { DisplayName = "AD Forest", PageTag = "ad_forest", Category = "Active Directory", Icon = "&#xE8A5;", Description = "Manage forest-wide settings and schemas" });

        // Add AWS items
        _allSearchableItems.Add(new SearchResult { DisplayName = "AWS Console", PageTag = "aws_home", Category = "AWS", Icon = "&#xE753;", Description = "Access AWS management console" });
        _allSearchableItems.Add(new SearchResult { DisplayName = "EC2 Instances", PageTag = "aws_ec2", Category = "AWS", Icon = "&#xE7F8;", Description = "Manage EC2 virtual machines and compute resources" });
        _allSearchableItems.Add(new SearchResult { DisplayName = "S3 Storage", PageTag = "aws_s3", Category = "AWS", Icon = "&#xE8B7;", Description = "Manage S3 buckets and object storage" });
        _allSearchableItems.Add(new SearchResult { DisplayName = "RDS Databases", PageTag = "aws_rds", Category = "AWS", Icon = "&#xE74E;", Description = "Manage relational database instances" });
        _allSearchableItems.Add(new SearchResult { DisplayName = "Lambda Functions", PageTag = "aws_lambda", Category = "AWS", Icon = "&#xE945;", Description = "Manage serverless Lambda functions" });

        // Add Azure items
        _allSearchableItems.Add(new SearchResult { DisplayName = "Azure Portal", PageTag = "azure_home", Category = "Azure", Icon = "&#xE753;", Description = "Access Azure management portal" });
        _allSearchableItems.Add(new SearchResult { DisplayName = "Virtual Machines", PageTag = "azure_vms", Category = "Azure", Icon = "&#xE7F8;", Description = "Manage Azure virtual machines" });
        _allSearchableItems.Add(new SearchResult { DisplayName = "Storage Accounts", PageTag = "azure_storage", Category = "Azure", Icon = "&#xE8B7;", Description = "Manage Azure storage accounts and containers" });
        _allSearchableItems.Add(new SearchResult { DisplayName = "SQL Databases", PageTag = "azure_sql", Category = "Azure", Icon = "&#xE74E;", Description = "Manage Azure SQL databases and servers" });
        _allSearchableItems.Add(new SearchResult { DisplayName = "Azure Functions", PageTag = "azure_functions", Category = "Azure", Icon = "&#xE945;", Description = "Manage Azure serverless functions" });

        // Add Digital Forensics items
        _allSearchableItems.Add(new SearchResult { DisplayName = "Forensics Home", PageTag = "forensics_home", Category = "Forensics", Icon = "&#xE720;", Description = "Digital forensics overview and tools" });
        _allSearchableItems.Add(new SearchResult { DisplayName = "Forensics Cases", PageTag = "forensics_cases", Category = "Forensics", Icon = "&#xE7C5;", Description = "Manage investigation cases and workflows" });
        _allSearchableItems.Add(new SearchResult { DisplayName = "Evidence Management", PageTag = "forensics_evidence", Category = "Forensics", Icon = "&#xE8B9;", Description = "Track and analyze digital evidence" });
        _allSearchableItems.Add(new SearchResult { DisplayName = "Forensics Analysis", PageTag = "forensics_analysis", Category = "Forensics", Icon = "&#xE9D9;", Description = "Perform detailed forensic analysis" });
        _allSearchableItems.Add(new SearchResult { DisplayName = "Forensics Reports", PageTag = "forensics_reports", Category = "Forensics", Icon = "&#xE74C;", Description = "Generate forensic investigation reports" });
    }

    public void UpdateSearchSuggestions(string query)
    {
        SearchSuggestions.Clear();

        if (string.IsNullOrWhiteSpace(query) || query.Length < 2)
            return;

        var filteredItems = _allSearchableItems
            .Where(item => item.DisplayName.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                          item.Category.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                          item.Description.Contains(query, StringComparison.OrdinalIgnoreCase))
            .OrderBy(item => item.Category)
            .ThenBy(item => item.DisplayName)
            .Take(8);

        foreach (var item in filteredItems)
        {
            SearchSuggestions.Add(item);
        }
    }

    public void PerformGlobalSearch(string query)
    {
        // Find the best match and navigate to it
        var bestMatch = _allSearchableItems
            .FirstOrDefault(item => item.DisplayName.Contains(query, StringComparison.OrdinalIgnoreCase));

        if (bestMatch != null)
        {
            Navigate(bestMatch.PageTag);
        }
    }

    public void ClearSearch()
    {
        TopSearchQuery = string.Empty;
        SearchSuggestions.Clear();
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
