# Dashboard App

## Overview
A comprehensive WinUI 3 dashboard application built with .NET 8.0 for Windows platforms. This application provides management interfaces for various enterprise technologies including Active Directory, AWS services, Azure services, and Digital Forensics tools.

## Project Architecture
- **Framework**: WinUI 3 with .NET 8.0
- **Target Platform**: Windows 10.0.19041.0 (minimum 10.0.17763.0)
- **Project Type**: MSIX packaged Windows application
- **Structure**: MVVM pattern with Views, ViewModels, and Controls

## Key Features
### Active Directory Management
- User management (AdUsersPage)
- Group management (AdGroupsPage) 
- Computer management (AdComputersPage)
- Domain management (AdDomainsPage)
- Forest management (AdForestPage)

### AWS Services Integration
- EC2 instance management (AwsEc2Page)
- S3 storage management (AwsS3Page)
- RDS database management (AwsRdsPage)
- Lambda functions management (AwsLambdaPage)

### Azure Services Integration
- Virtual machines management (AzureVmsPage)
- SQL database management (AzureSqlPage)
- Storage account management (AzureStoragePage)
- Azure Functions management (AzureFunctionsPage)

### Digital Forensics Tools
- Case management (ForensicsCasesPage)
- Evidence tracking (ForensicsEvidencePage)
- Analysis tools (ForensicsAnalysisPage)
- Report generation (ForensicsReportsPage)

### Dashboard & Analytics
- Overview dashboard (DashboardOverviewPage)
- Analytics and reporting (DashboardAnalyticsPage)

## Project Structure
```
DashboardApp/
├── Views/           # UI pages for different modules
├── ViewModels/      # Data binding and business logic
├── Controls/        # Reusable UI controls
│   ├── GlobalLeftSidebar.xaml
│   └── TopNavbar.xaml
├── Models/          # Data models (folder exists for future use)
├── Assets/          # Application icons and images
├── Properties/      # Application properties
├── App.xaml         # Application definition
├── MainWindow.xaml  # Main application window
└── Package.appxmanifest # App package configuration
```

## Recent Changes
- Project created with comprehensive page structure
- All major service categories implemented as separate pages
- Navigation structure established with sidebar and top navbar
- MSIX packaging configured for Windows deployment

## User Preferences
- None documented yet

## Development Notes
- Built with Windows App SDK 1.8.250907003
- Uses Microsoft.Windows.SDK.BuildTools 10.0.26100.4948
- Supports x86, x64, and ARM64 architectures
- Nullable reference types enabled for better code safety
- Ready for deployment as MSIX package

## Current State
- All page structures are in place
- Application compiles successfully
- Ready for implementation of business logic and API integrations