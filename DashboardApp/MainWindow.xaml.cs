// MainWindow.xaml.cs
using Microsoft.UI.Xaml;

namespace DashboardApp;

public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        this.InitializeComponent();
        this.Title = "Professional Dashboard - System Management";
        
        // Set initial window size and constraints for better UX
        SetupWindow();
    }
    
    private void SetupWindow()
    {
        var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
        var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hwnd);
        var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
        
        if (appWindow != null)
        {
            // Set initial size
            appWindow.Resize(new Windows.Graphics.SizeInt32(1400, 900));
            
            // Set minimum size using OverlappedPresenter
            if (appWindow.Presenter is Microsoft.UI.Windowing.OverlappedPresenter overlappedPresenter)
            {
                overlappedPresenter.SetBorderAndTitleBar(true, true);
                overlappedPresenter.IsResizable = true;
                overlappedPresenter.IsMaximizable = true;
                overlappedPresenter.IsMinimizable = true;
            }
        }
    }
}