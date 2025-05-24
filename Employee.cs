// Employee.cs
// Defines the Employee class with properties, validation, and comparison operators

using System;
using Newtonsoft.Json;

namespace EmployeeComparisonApp
{
    // Represents an employee with extended attributes
    public class Employee
    {
        // Private backing fields for encapsulation
        private int _id;
        private string _firstName;
        private string _lastName;
        private string _department;
        private DateTime _hireDate;

        // ID property with validation
        [JsonProperty("id")]
        public int Id
        {
            get => _id;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("ID must be a positive integer.");
                _id = value;
            }
        }

        // FirstName property with validation
        [JsonProperty("firstName")]
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("First name cannot be empty or whitespace.");
                _firstName = value.Trim();
            }
        }

        // LastName property with validation
        [JsonProperty("lastName")]
        public string LastName
        {
            get => _lastName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Last name cannot be empty or whitespace.");
                _lastName = value.Trim();
            }
        }

        // Department property with validation
        [JsonProperty("department")]
        public string Department
        {
            get => _department;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Department cannot be empty or whitespace.");
                _department = value.Trim();
            }
        }

        // HireDate property with validation
        [JsonProperty("hireDate")]
        public DateTime HireDate
        {
            get => _hireDate;
            set
            {
                if (value > DateTime.Now)
                    throw new ArgumentException("Hire date cannot be in the future.");
                _hireDate = value;
            }
        }

        // Constructor to initialize an Employee
        public Employee(int id, string firstName, string lastName, string department, DateTime hireDate)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Department = department;
            HireDate = hireDate;
        }

        // Overload == operator to compare employees by ID
        public static bool operator ==(Employee emp1, Employee emp2)
        {
            if (ReferenceEquals(emp1, null) && ReferenceEquals(emp2, null))
                return true;
            if (ReferenceEquals(emp1, null) || ReferenceEquals(emp2, null))
                return false;
            return emp1.Id == emp2.Id;
        }

        // Overload != operator to complement == operator
        public static bool operator !=(Employee emp1, Employee emp2)
        {
            return !(emp1 == emp2);
        }

        // Override Equals for consistency with == operator
        public override bool Equals(object obj)
        {
            if (obj is Employee other)
            {
                return this == other;
            }
            return false;
        }

        // Override GetHashCode for consistent hashing
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        // Override ToString for formatted display
        public override string ToString()
        {
            return $"ID: {Id}, Name: {FirstName} {LastName}, Dept: {Department}, Hired: {HireDate:yyyy-MM-dd}";
        }
    }
}