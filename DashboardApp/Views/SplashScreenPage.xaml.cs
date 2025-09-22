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
                new DependencyItem { Name = "Active Directory Services", Icon = "&#xE716;" },
                new DependencyItem { Name = "AWS SDK", Icon = "&#xE753;" },
                new DependencyItem { Name = "Azure Services", Icon = "&#xE753;" },
                new DependencyItem { Name = "Database Connection", Icon = "&#xE7C3;" },
                new DependencyItem { Name = "Security Modules", Icon = "&#xE72E;" },
                new DependencyItem { Name = "Analytics Engine", Icon = "&#xE9D9;" },
                new DependencyItem { Name = "Forensics Tools", Icon = "&#xE8B7;" },
                new DependencyItem { Name = "User Interface", Icon = "&#xE8A7;" }
            };
        }

        private async Task LoadDependenciesAsync()
        {
            for (int i = 0; i < _dependencies.Count; i++)
            {
                var dependency = _dependencies[i];

                // Update current loading text
                CurrentLoadingText.Text = $"Loading {dependency.Name}...";
                CurrentSpinner.IsActive = true;

                // Simulate loading time
                await Task.Delay(Random.Shared.Next(1000, 2500));

                // Add completed dependency to the list
                AddCompletedDependency(dependency);

                // Update progress
                _currentProgress = (int)((i + 1) / (float)_dependencies.Count * 100);
                LoadingProgressBar.Value = _currentProgress;
                ProgressText.Text = $"{_currentProgress}%";

                await Task.Delay(300);
            }

            // Final completion
            CurrentLoadingText.Text = "Ready!";
            CurrentSpinner.IsActive = false;

            await Task.Delay(1500);

            // Navigate to main app
            NavigateToMainApp();
        }

        private void AddCompletedDependency(DependencyItem dependency)
        {
            var grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });

            // Icon
            var icon = new FontIcon
            {
                Glyph = dependency.Icon,
                FontSize = 12,
                Foreground = new SolidColorBrush(Microsoft.UI.Colors.LightGreen),
                Margin = new Thickness(0, 0, 8, 0),
                VerticalAlignment = VerticalAlignment.Center
            };
            Grid.SetColumn(icon, 0);

            // Name
            var nameText = new TextBlock
            {
                Text = dependency.Name,
                Foreground = new SolidColorBrush(Microsoft.UI.Colors.LightGray),
                FontSize = 11,
                VerticalAlignment = VerticalAlignment.Center
            };
            Grid.SetColumn(nameText, 1);

            // Checkmark
            var checkmark = new FontIcon
            {
                Glyph = "&#xE73E;", // Checkmark icon
                FontSize = 12,
                Foreground = new SolidColorBrush(Microsoft.UI.Colors.LightGreen),
                VerticalAlignment = VerticalAlignment.Center
            };
            Grid.SetColumn(checkmark, 2);

            grid.Children.Add(icon);
            grid.Children.Add(nameText);
            grid.Children.Add(checkmark);

            CompletedStack.Children.Add(grid);

            // Keep only last 4 completed items visible
            if (CompletedStack.Children.Count > 4)
            {
                CompletedStack.Children.RemoveAt(0);
            }
        }

        private void NavigateToMainApp()
        {
            // Navigate to the main application
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(ShellPage));
            }
        }

        private class DependencyItem
        {
            public string Name { get; set; } = "";
            public string Icon { get; set; } = "";
        }
    }
}