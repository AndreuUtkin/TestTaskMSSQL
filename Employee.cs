using System;
namespace TestTaskMSSQL.Models;
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public double Salary { get; set; }

        public string print()
        {
            return $"Id: {EmployeeID} имя: {FirstName} {LastName} email: {Email} родился: {DateOfBirth:dd.MM.yyyy} зарплата: {Salary:C}";
        }
    }
}
