Employee Comparison Application
Overview
This is a feature-rich .NET console application for managing and comparing employee records. It features a dynamic menu system, JSON and CSV persistence, sorting, searching, statistics, and customizable themes, making it highly interactive and engaging.
Features

Add, edit, and delete employees with ID, First Name, Last Name, Department, and Hire Date.
Compare employees by ID using overloaded == and != operators.
List employees with sorting by ID, name, or hire date.
Search employees by name or department.
Generate statistics (e.g., total employees, department breakdown, average tenure).
Export employee data to CSV.
Save and load data to/from JSON.
Customizable console color themes (Cyan, Blue, Green, Magenta).
Interactive sub-menus with ASCII art, progress bars, and input history for departments.
Robust input validation and error handling.

Prerequisites

.NET SDK (version 6.0 or later)
Git
A code editor like Visual Studio or Visual Studio Code

Installation

Clone the repository:git clone https://github.com/yourusername/EmployeeComparisonApp.git


Navigate to the project directory:cd EmployeeComparisonApp


Install the required NuGet package:dotnet add package Newtonsoft.Json


Build and run the application:dotnet run



Usage

Run the application using dotnet run.
Navigate the main menu:
Manage Employees: Add, edit, delete, compare, list, or save employees.
Generate Reports: View statistics, search employees, or export to CSV.
Change Theme: Switch console color themes.
Exit: Confirm and close the application.


Follow on-screen prompts for input.

Example Output
  _____ _          _ _       
 |  ___| |__   ___| | | ___  
 | |_  | '_ \ / __| | |/ _ \ 
 |  _| | | | | (__| | |  __/ 
 |_|   |_| |_| \___|_|_|\___|
 Employee Comparison App v2.0 
=============================
Crunching data...: [████████████████████]
Ready to use! 0 employee(s) loaded.

=== Main Menu ===
1. Manage Employees
2. Generate Reports
3. Change Theme
4. Exit

Enter your choice (1-4):


