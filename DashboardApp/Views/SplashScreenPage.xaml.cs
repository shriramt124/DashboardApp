using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DashboardApp.Views
{
    public sealed partial class SplashScreenPage : Page
    {
        private readonly List<string> _loadingSteps;
        private int _currentProgress = 0;

        public SplashScreenPage()
        {
            this.InitializeComponent();
            _loadingSteps = new List<string>
            {
                "Initializing core services",
                "Loading security modules",
                "Connecting to databases",
                "Preparing user interface",
                "Finalizing setup"
            };
            this.Loaded += SplashScreenPage_Loaded;
        }

        private async void SplashScreenPage_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadApplicationAsync();
        }

        private async Task LoadApplicationAsync()
        {
            for (int i = 0; i < _loadingSteps.Count; i++)
            {
                var step = _loadingSteps[i];

                // Update current loading text
                CurrentLoadingText.Text = step;
                CurrentSpinner.IsActive = true;

                // Simulate loading time
                await Task.Delay(Random.Shared.Next(800, 1500));

                // Update progress
                _currentProgress = (int)((i + 1) / (float)_loadingSteps.Count * 100);
                LoadingProgressBar.Value = _currentProgress;
                ProgressText.Text = $"{_currentProgress}%";

                await Task.Delay(200);
            }

            // Final completion
            CurrentLoadingText.Text = "Ready!";
            CurrentSpinner.IsActive = false;

            await Task.Delay(1000);

            // Navigate to main app
            NavigateToMainApp();
        }

        private void NavigateToMainApp()
        {
            // Navigate to the login page
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(LoginPage));
            }
        }
    }
}