// Controls/TopNavbar.xaml.cs
using DashboardApp.ViewModels; // <-- IMPORTANT: Add this using statement!
using Microsoft.UI.Xaml.Controls;

namespace DashboardApp.Controls // <-- Must match the path in x:Class
{
    public sealed partial class TopNavbar : UserControl // <-- Must be 'partial'
    {
        // Inside the 'TopNavbar' class
        public ShellViewModel? ViewModel { get; set; }

        public TopNavbar()
        {
            this.InitializeComponent();
        }
    }
}