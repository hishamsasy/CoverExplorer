CoverExplorer

CoverExplorer is a modern .NET desktop application that helps developers visualize and analyze test coverage results in a user-friendly way. It allows users to select specific classes or namespaces from their projects and view detailed coverage statistics with charts and filtering options.

🚀 Features

Run .NET Tests – Execute unit tests and generate code coverage reports.

Visual Coverage Analysis – View test coverage data in a structured and graphical format.

Namespace & Class Filtering – Select specific parts of your codebase for detailed analysis.

Coverage Report Generation – Uses dotnet test and ReportGenerator to process results.

🛠️ Installation

Prerequisites

.NET 8 SDK (Download)

Windows OS (for WPF support)

Clone the Repository

git clone https://github.com/hishamsasy/CoverExplorer.git
cd CoverExplorer

Run the Application

Open the solution in Visual Studio 2022+.

Set CoverExplorer as the startup project.

Press F5 to build and run the application.

📊 How It Works

Select your .csproj files to analyze.

Run Tests – The app executes dotnet test and collects coverage data.

View Results – Explore coverage statistics in an intuitive dashboard.

Filter Data – Select specific namespaces and classes for deeper insights.

🔧 Tech Stack

Frontend: WPF (C#), Material Design for WPF

Backend: .NET 8, MVVM Architecture

Code Coverage: Coverlet, ReportGenerator

Charts & Visuals: LiveCharts2


📄 License

This project is licensed under the MIT License - see the LICENSE file for details.
