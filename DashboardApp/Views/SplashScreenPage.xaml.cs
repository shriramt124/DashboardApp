
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DashboardApp.Views
{
    public sealed partial class SplashScreenPage : Page
    {
        private readonly List<DependencyItem> _dependencies;
        private int _currentProgress = 0;

        public SplashScreenPage()
        {
            this.InitializeComponent();
            _dependencies = CreateMockDependencies();
            this.Loaded += SplashScreenPage_Loaded;
        }

        private async void SplashScreenPage_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadDependenciesAsync();
        }

        private List<DependencyItem> CreateMockDependencies()
        {
            return new List<DependencyItem>
            {
                new DependencyItem { Name = "Active Directory Services", Status = "Pending", Icon = "&#xE716;" },
                new DependencyItem { Name = "AWS SDK", Status = "Pending", Icon = "&#xE753;" },
                new DependencyItem { Name = "Azure Services", Status = "Pending", Icon = "&#xE753;" },
                new DependencyItem { Name = "Database Connection", Status = "Pending", Icon = "&#xE7C3;" },
                new DependencyItem { Name = "Security Modules", Status = "Pending", Icon = "&#xE72E;" },
                new DependencyItem { Name = "Analytics Engine", Status = "Pending", Icon = "&#xE9D9;" },
                new DependencyItem { Name = "Forensics Tools", Status = "Pending", Icon = "&#xE8B7;" },
                new DependencyItem { Name = "User Interface", Status = "Pending", Icon = "&#xE8A7;" }
            };
        }

        private async Task LoadDependenciesAsync()
        {
            // Create UI elements for each dependency
            foreach (var dependency in _dependencies)
            {
                CreateDependencyUI(dependency);
            }

            // Simulate loading each dependency
            for (int i = 0; i < _dependencies.Count; i++)
            {
                var dependency = _dependencies[i];
                
                // Update current loading text
                CurrentLoadingText.Text = $"Loading {dependency.Name}...";
                
                // Update dependency status to loading
                dependency.Status = "Loading";
                UpdateDependencyUI(i, dependency);
                
                // Simulate loading time
                await Task.Delay(Random.Shared.Next(800, 2000));
                
                // Update dependency status to completed
                dependency.Status = "Completed";
                UpdateDependencyUI(i, dependency);
                
                // Update progress
                _currentProgress = (int)((i + 1) / (float)_dependencies.Count * 100);
                LoadingProgressBar.Value = _currentProgress;
                ProgressText.Text = $"{_currentProgress}%";
                
                await Task.Delay(300);
            }

            // Final completion
            CurrentLoadingText.Text = "Ready!";
            LoadingSpinner.IsActive = false;
            
            await Task.Delay(1000);
            
            // Navigate to main app (you'll need to implement this navigation)
            NavigateToMainApp();
        }

        private void CreateDependencyUI(DependencyItem dependency)
        {
            var grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });

            // Icon
            var icon = new FontIcon
            {
                Glyph = dependency.Icon,
                FontSize = 16,
                Foreground = new SolidColorBrush(Microsoft.UI.Colors.LightBlue),
                Margin = new Thickness(0, 0, 12, 0)
            };
            Grid.SetColumn(icon, 0);

            // Name
            var nameText = new TextBlock
            {
                Text = dependency.Name,
                Foreground = new SolidColorBrush(Microsoft.UI.Colors.White),
                FontSize = 13,
                VerticalAlignment = VerticalAlignment.Center
            };
            Grid.SetColumn(nameText, 1);

            // Status
            var statusText = new TextBlock
            {
                Text = dependency.Status,
                FontSize = 12,
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = GetStatusColor(dependency.Status)
            };
            Grid.SetColumn(statusText, 2);

            grid.Children.Add(icon);
            grid.Children.Add(nameText);
            grid.Children.Add(statusText);

            DependenciesStack.Children.Add(grid);
        }

        private void UpdateDependencyUI(int index, DependencyItem dependency)
        {
            if (index < DependenciesStack.Children.Count)
            {
                var grid = (Grid)DependenciesStack.Children[index];
                var statusText = (TextBlock)grid.Children[2];
                statusText.Text = dependency.Status;
                statusText.Foreground = GetStatusColor(dependency.Status);

                // Add loading spinner for loading status
                if (dependency.Status == "Loading" && grid.Children.Count == 3)
                {
                    var spinner = new ProgressRing
                    {
                        Width = 12,
                        Height = 12,
                        IsActive = true,
                        Margin = new Thickness(8, 0, 0, 0),
                        Foreground = new SolidColorBrush(Microsoft.UI.Colors.LightBlue)
                    };
                    Grid.SetColumn(spinner, 2);
                    grid.Children.Add(spinner);
                }
                else if (dependency.Status != "Loading" && grid.Children.Count > 3)
                {
                    // Remove spinner
                    grid.Children.RemoveAt(3);
                }
            }
        }

        private SolidColorBrush GetStatusColor(string status)
        {
            return status switch
            {
                "Pending" => new SolidColorBrush(Microsoft.UI.Colors.Gray),
                "Loading" => new SolidColorBrush(Microsoft.UI.Colors.Orange),
                "Completed" => new SolidColorBrush(Microsoft.UI.Colors.LightGreen),
                _ => new SolidColorBrush(Microsoft.UI.Colors.Gray)
            };
        }

        private void NavigateToMainApp()
        {
            // Navigate to the main application
            // This should be implemented based on your navigation pattern
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(ShellPage));
            }
        }

        private class DependencyItem
        {
            public string Name { get; set; } = "";
            public string Status { get; set; } = "";
            public string Icon { get; set; } = "";
        }
    }
}
