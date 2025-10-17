using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTaskMSSQL.Models;

namespace TestTaskMSSQL.Services
{
    public interface IService
    {
        Task<bool> TestConnection();
        Task<int> Create(string firstName, string lastName, string email, DateTime dateOfBirth, decimal salary);
        Task<List<Employee>> GetAll();
        Task<Employee> Get(int employeeId);
        Task<bool> Update(int employeeId, string field, object value);
        Task<bool> Delete(int employeeId);
        Task<int> HighSalary();
        
    }
}
