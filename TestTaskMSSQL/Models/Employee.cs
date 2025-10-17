using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskMSSQL.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal Salary { get; set; }

        public string print()
        {
            return $"Id: {EmployeeID} имя: {FirstName} {LastName} email: {Email} родился: {DateOfBirth:dd.MM.yyyy} зарплата: {Salary:C}";
        }
    }
}


