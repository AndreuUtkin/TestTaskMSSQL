using TestTaskMSSQL.DAO;
using TestTaskMSSQL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.Tests.Mocks
{
    public class MockDao : IDao
    {
        private readonly List<Employee> _employees = new();
        private int _nextId = 1;

       
        public Task<bool> TestConnection() => Task.FromResult(true);

        public Task<int> Create(Employee employee)
        {
            employee.EmployeeID = _nextId++;
            _employees.Add(employee);
            return Task.FromResult(employee.EmployeeID);
        }

        public Task<List<Employee>> GetAll()
        {
            return Task.FromResult(new List<Employee>(_employees));
        }

        public Task<bool> Update(int employeeId, string field, object value)
        {
            var employee = _employees.Find(e => e.EmployeeID == employeeId);
            if (employee == null) return Task.FromResult(false);

            switch (field)
            {
                case "FirstName": employee.FirstName = (string)value; break;
                case "LastName": employee.LastName = (string)value; break;
                case "Email": employee.Email = (string)value; break;
                case "DateOfBirth": employee.DateOfBirth = (DateTime)value; break;
                case "Salary": employee.Salary = (decimal)value; break;
                default: return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        public Task<bool> Delete(int employeeId)
        {
            var employee = _employees.Find(e => e.EmployeeID == employeeId);
            if (employee == null) return Task.FromResult(false);

            return Task.FromResult(_employees.Remove(employee));
        }

        public Task<int> HighSalary()
        {
            if (_employees.Count == 0) return Task.FromResult(0);

            var averageSalary = _employees.Average(e => e.Salary);
            var count = _employees.Count(e => e.Salary > averageSalary);
            return Task.FromResult(count);
        }

        public Task<Employee> Get(int employeeId)
        {
            var employee = _employees.Find(e => e.EmployeeID == employeeId);
            return Task.FromResult(employee);
        }
    }
}