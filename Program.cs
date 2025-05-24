// Program.cs
// Main entry point for the enhanced Employee Comparison Application

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;

namespace EmployeeComparisonApp
{
    class Program
    {
        // File paths for data persistence
        private static readonly string DataFile = "employees.json";
        private static readonly string CsvFile = "employees.csv";
        // List to store employees
        private static List<Employee> employees = new List<Employee>();
        // Input history for departments
        private static List<string> departmentHistory = new List<string>();
        // Current console color theme
        private static ConsoleColor themeColor = ConsoleColor.Cyan;
        // Random messages for progress bars
        private static readonly string[] ProgressMessages = { "Processing...", "Crunching data...", "Almost there...", "Loading awesomeness..." };

        static void Main(string[] args)
        {
            // Load employees from JSON
            LoadEmployees();

            // Display welcome screen
            DisplayWelcomeScreen();

            // Main menu loop
            while (true)
            {
                DisplayMainMenu();
                string choice = Console.ReadLine()?.Trim();
                Console.Clear();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            ManageEmployees();
                            break;
                        case "2":
                            GenerateReports();
                            break;
                        case "3":
                            ChangeTheme();
                            break;
                        case "4":
                            if (ConfirmExit())
                            {
                                DisplayProgressBar("Shutting down", 10, ConsoleColor.Magenta);
                                return;
                            }
                            break;
                        default:
                            DisplayError("Invalid choice. Please select 1-4.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    DisplayError($"Error: {ex.Message}");
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        // Displays the welcome screen with ASCII art and animation
        static void DisplayWelcomeScreen()
        {
            Console.ForegroundColor = themeColor;
            Console.WriteLine(@"  _____ _          _ _       
 |  ___| |__   ___| | | ___  
 | |_  | '_ \ / __| | |/ _ \ 
 |  _| | | | | (__| | |  __/ 
 |_|   |_| |_| \___|_|_|\___|");
            Console.WriteLine(" Employee Comparison App v2.0 ");
            Console.WriteLine("=============================");
            Console.ResetColor();
            DisplayProgressBar(ProgressMessages[new Random().Next(ProgressMessages.Length)], 20, themeColor);
            Console.WriteLine($"\nReady to use! {employees.Count} employee(s) loaded.");
        }

        // Displays the main menu
        static void DisplayMainMenu()
        {
            Console.ForegroundColor = themeColor;
            Console.WriteLine("=== Main Menu ===");
            Console.ResetColor();
            Console.WriteLine("1. Manage Employees");
            Console.WriteLine("2. Generate Reports");
            Console.WriteLine("3. Change Theme");
            Console.WriteLine("4. Exit");
            Console.Write("\nEnter your choice (1-4): ");
        }

        // Displays the employee management sub-menu
        static void ManageEmployees()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = themeColor;
                Console.WriteLine("=== Employee Management ===");
                Console.ResetColor();
                Console.WriteLine("1. Add Employee");
                Console.WriteLine("2. Edit Employee");
                Console.WriteLine("3. Delete Employee");
                Console.WriteLine("4. Compare Employees");
                Console.WriteLine("5. List All Employees");
                Console.WriteLine("6. Save Employees");
                Console.WriteLine("7. Back to Main Menu");
                Console.Write("\nEnter your choice (1-7): ");
                string choice = Console.ReadLine()?.Trim();
                Console.Clear();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            AddEmployee();
                            break;
                        case "2":
                            EditEmployee();
                            break;
                        case "3":
                            DeleteEmployee();
                            break;
                        case "4":
                            CompareEmployees();
                            break;
                        case "5":
                            ListEmployees();
                            break;
                        case "6":
                            SaveEmployees();
                            break;
                        case "7":
                            return;
                        default:
                            DisplayError("Invalid choice. Please select 1-7.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    DisplayError($"Error: {ex.Message}");
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        // Displays the reports sub-menu
        static void GenerateReports()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = themeColor;
                Console.WriteLine("=== Reports ===");
                Console.ResetColor();
                Console.WriteLine("1. Employee Statistics");
                Console.WriteLine("2. Search Employees");
                Console.WriteLine("3. Export to CSV");
                Console.WriteLine("4. Back to Main Menu");
                Console.Write("\nEnter your choice (1-4): ");
                string choice = Console.ReadLine()?.Trim();
                Console.Clear();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            DisplayStatistics();
                            break;
                        case "2":
                            SearchEmployees();
                            break;
                        case "3":
                            ExportToCsv();
                            break;
                        case "4":
                            return;
                        default:
                            DisplayError("Invalid choice. Please select 1-4.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    DisplayError($"Error: {ex.Message}");
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        // Adds a new employee
        static void AddEmployee()
        {
            Console.WriteLine("=== Add New Employee ===");
            Employee employee = CreateEmployeeFromInput();
            employees.Add(employee);
            if (!departmentHistory.Contains(employee.Department))
                departmentHistory.Add(employee.Department);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nEmployee added: {employee}");
            Console.ResetColor();
        }

        // Creates an Employee from user input
        static Employee CreateEmployeeFromInput(int existingId = -1)
        {
            // Prompt and validate ID
            Console.Write("Enter Employee ID (positive integer): ");
            if (!int.TryParse(Console.ReadLine()?.Trim(), out int id) || id <= 0)
                throw new ArgumentException("Invalid ID. Must be a positive integer.");

            // Check for duplicate ID unless editing
            if (existingId != id && employees.Any(e => e.Id == id))
                throw new ArgumentException($"ID {id} already exists.");

            // Prompt and validate first name
            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name cannot be empty.");

            // Prompt and validate last name
            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name cannot be empty.");

            // Prompt and validate department with history suggestion
            Console.Write("Enter Department (suggestions: ");
            Console.Write(string.Join(", ", departmentHistory));
            Console.Write("): ");
            string department = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(department))
                throw new ArgumentException("Department cannot be empty.");

            // Prompt and validate hire date
            Console.Write("Enter Hire Date (yyyy-MM-dd): ");
            if (!DateTime.TryParse(Console.ReadLine()?.Trim(), out DateTime hireDate))
                throw new ArgumentException("Invalid date format. Use yyyy-MM-dd.");

            return new Employee(id, firstName, lastName, department, hireDate);
        }

        // Edits an existing employee
        static void EditEmployee()
        {
            if (employees.Count == 0)
            {
                DisplayError("No employees to edit.");
                return;
            }

            ListEmployees();
            Console.Write("Enter ID of employee to edit: ");
            if (!int.TryParse(Console.ReadLine()?.Trim(), out int id))
                throw new ArgumentException("Invalid ID.");

            Employee employee = employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
                throw new ArgumentException("Employee not found.");

            Console.WriteLine($"\nEditing: {employee}");
            Employee newEmployee = CreateEmployeeFromInput(id);
            employees.Remove(employee);
            employees.Add(newEmployee);
            if (!departmentHistory.Contains(newEmployee.Department))
                departmentHistory.Add(newEmployee.Department);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nEmployee updated successfully!");
            Console.ResetColor();
        }

        // Deletes an employee
        static void DeleteEmployee()
        {
            if (employees.Count == 0)
            {
                DisplayError("No employees to delete.");
                return;
            }

            ListEmployees();
            Console.Write("Enter ID of employee to delete: ");
            if (!int.TryParse(Console.ReadLine()?.Trim(), out int id))
                throw new ArgumentException("Invalid ID.");

            Employee employee = employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
                throw new ArgumentException("Employee not found.");

            employees.Remove(employee);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nEmployee deleted successfully!");
            Console.ResetColor();
        }

        // Compares two employees
        static void CompareEmployees()
        {
            if (employees.Count < 2)
            {
                DisplayError("At least two employees are required for comparison.");
                return;
            }

            ListEmployees();
            Console.Write("Enter ID of first employee: ");
            if (!int.TryParse(Console.ReadLine()?.Trim(), out int id1))
                throw new ArgumentException("Invalid ID for first employee.");

            Console.Write("Enter ID of second employee: ");
            if (!int.TryParse(Console.ReadLine()?.Trim(), out int id2))
                throw new ArgumentException("Invalid ID for second employee.");

            Employee emp1 = employees.FirstOrDefault(e => e.Id == id1);
            Employee emp2 = employees.FirstOrDefault(e => e.Id == id2);

            if (emp1 == null || emp2 == null)
                throw new ArgumentException("One or both employee IDs not found.");

            Console.WriteLine("\nComparison Results:");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Employee 1: {emp1}");
            Console.WriteLine($"Employee 2: {emp2}");
            Console.WriteLine($"Are employees equal? {emp1 == emp2}");
            Console.WriteLine($"Are employees not equal? {emp1 != emp2}");
            Console.ResetColor();
        }

        // Lists all employees with sorting option
        static void ListEmployees()
        {
            if (employees.Count == 0)
            {
                DisplayError("No employees to display.");
                return;
            }

            Console.WriteLine("Sort by (1: ID, 2: Name, 3: Hire Date): ");
            string sortChoice = Console.ReadLine()?.Trim();
            List<Employee> sortedEmployees = employees;
            switch (sortChoice)
            {
                case "2":
                    sortedEmployees = employees.OrderBy(e => e.FirstName).ThenBy(e => e.LastName).ToList();
                    break;
                case "3":
                    sortedEmployees = employees.OrderBy(e => e.HireDate).ToList();
                    break;
                default:
                    sortedEmployees = employees.OrderBy(e => e.Id).ToList();
                    break;
            }

            Console.WriteLine("\n=== Employee List ===");
            Console.WriteLine($"{"ID",-5} {"First Name",-15} {"Last Name",-15} {"Department",-15} {"Hire Date",-12}");
            Console.WriteLine(new string('=', 62));
            foreach (var emp in sortedEmployees)
            {
                Console.WriteLine($"{emp.Id,-5} {emp.FirstName,-15} {emp.LastName,-15} {emp.Department,-15} {emp.HireDate:yyyy-MM-dd}");
            }
        }

        // Displays employee statistics
        static void DisplayStatistics()
        {
            if (employees.Count == 0)
            {
                DisplayError("No employees to analyze.");
                return;
            }

            Console.WriteLine("=== Employee Statistics ===");
            Console.WriteLine($"Total Employees: {employees.Count}");
            var departments = employees.GroupBy(e => e.Department).Select(g => new { Department = g.Key, Count = g.Count() });
            Console.WriteLine("\nEmployees by Department:");
            foreach (var dept in departments)
            {
                Console.WriteLine($"{dept.Department}: {dept.Count}");
            }

            var averageTenure = employees.Average(e => (DateTime.Now - e.HireDate).TotalDays / 365);
            Console.WriteLine($"\nAverage Tenure: {averageTenure:F2} years");
        }

        // Searches employees by name or department
        static void SearchEmployees()
        {
            if (employees.Count == 0)
            {
                DisplayError("No employees to search.");
                return;
            }

            Console.Write("Enter search term (name or department): ");
            string searchTerm = Console.ReadLine()?.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(searchTerm))
                throw new ArgumentException("Search term cannot be empty.");

            var results = employees.Where(e =>
                e.FirstName.ToLower().Contains(searchTerm) ||
                e.LastName.ToLower().Contains(searchTerm) ||
                e.Department.ToLower().Contains(searchTerm)).ToList();

            if (results.Count == 0)
            {
                DisplayError("No employees found matching the search term.");
                return;
            }

            Console.WriteLine($"\nFound {results.Count} employee(s):");
            Console.WriteLine($"{"ID",-5} {"First Name",-15} {"Last Name",-15} {"Department",-15} {"Hire Date",-12}");
            Console.WriteLine(new string('=', 62));
            foreach (var emp in results)
            {
                Console.WriteLine($"{emp.Id,-5} {emp.FirstName,-15} {emp.LastName,-15} {emp.Department,-15} {emp.HireDate:yyyy-MM-dd}");
            }
        }

        // Exports employees to a CSV file
        static void ExportToCsv()
        {
            if (employees.Count == 0)
            {
                DisplayError("No employees to export.");
                return;
            }

            try
            {
                var csvLines = new List<string> { "ID,FirstName,LastName,Department,HireDate" };
                csvLines.AddRange(employees.Select(e => $"{e.Id},\"{e.FirstName}\",\"{e.LastName}\",\"{e.Department}\",\"{e.HireDate:yyyy-MM-dd}\""));
                File.WriteAllLines(CsvFile, csvLines);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nEmployees exported to {CsvFile}!");
                Console.ResetColor();
                DisplayProgressBar("Exporting data", 10, themeColor);
            }
            catch (IOException ex)
            {
                DisplayError($"Failed to export to CSV: {ex.Message}");
            }
        }

        // Saves employees to JSON
        static void SaveEmployees()
        {
            try
            {
                string json = JsonConvert.SerializeObject(employees, Formatting.Indented);
                File.WriteAllText(DataFile, json);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nEmployees saved successfully!");
                Console.ResetColor();
                DisplayProgressBar("Saving data", 10, themeColor);
            }
            catch (IOException ex)
            {
                DisplayError($"Failed to save employees: {ex.Message}");
            }
        }

        // Loads employees from JSON
        static void LoadEmployees()
        {
            try
            {
                if (File.Exists(DataFile))
                {
                    string json = File.ReadAllText(DataFile);
                    employees = JsonConvert.DeserializeObject<List<Employee>>(json) ?? new List<Employee>();
                    departmentHistory.AddRange(employees.Select(e => e.Department).Distinct().Except(departmentHistory));
                }
            }
            catch (Exception ex)
            {
                DisplayError($"Failed to load employees: {ex.Message}");
                employees = new List<Employee>();
            }
        }

        // Changes the console theme
        static void ChangeTheme()
        {
            Console.WriteLine("Select Theme:");
            Console.WriteLine("1. Cyan (Default)");
            Console.WriteLine("2. Blue");
            Console.WriteLine("3. Green");
            Console.WriteLine("4. Magenta");
            Console.Write("Enter choice (1-4): ");
            string choice = Console.ReadLine()?.Trim();
            switch (choice)
            {
                case "2":
                    themeColor = ConsoleColor.Blue;
                    break;
                case "3":
                    themeColor = ConsoleColor.Green;
                    break;
                case "4":
                    themeColor = ConsoleColor.Magenta;
                    break;
                default:
                    themeColor = ConsoleColor.Cyan;
                    break;
            }
            Console.ForegroundColor = themeColor;
            Console.WriteLine("\nTheme updated!");
            Console.ResetColor();
        }

        // Confirms exit
        static bool ConfirmExit()
        {
            Console.Write("Are you sure you want to exit? (y/n): ");
            string response = Console.ReadLine()?.Trim().ToLower();
            return response == "y" || response == "yes";
        }

        // Displays an error message
        static void DisplayError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nError: {message}");
            Console.ResetColor();
        }

        // Displays a progress bar with random messages
        static void DisplayProgressBar(string message, int steps, ConsoleColor color)
        {
            Console.Write($"\n{message}: [");
            Console.ForegroundColor = color;
            for (int i = 0; i < steps; i++)
            {
                Console.Write("█");
                Thread.Sleep(100);
            }
            Console.Write("]");
            Console.ResetColor();
        }
    }
}