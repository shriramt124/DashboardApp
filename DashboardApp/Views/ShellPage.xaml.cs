// Views/ShellPage.xaml.cs (Simplified)
using DashboardApp.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace DashboardApp.Views
{
    public sealed partial class ShellPage : Page
    {
        public ShellViewModel ViewModel { get; }

        public ShellPage()
        {
            this.InitializeComponent();
            ViewModel = new ShellViewModel();
            this.DataContext = ViewModel;

            // Pass the Frame reference to the ViewModel so it can navigate
            ViewModel.ContentFrame = this.FindName("MainFrame") as Frame;

            // Navigate to the default page when the ShellPage loads
            ViewModel.Navigate("active_directory");
        }

        // The 'MainContentNavigationView_ItemInvoked' method has been completely removed.
    }
}