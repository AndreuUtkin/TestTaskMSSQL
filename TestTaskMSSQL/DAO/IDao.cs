using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTaskMSSQL.Models;

namespace TestTaskMSSQL.DAO
{
    public interface IDao
    {
        Task<bool> TestConnection();
        Task<int> Create(Employee employee);
        Task<List<Employee>> GetAll();
        Task<Employee> Get(int employeeId);
        Task<bool> Update(int employeeId, Dictionary<string, object> updates);
        Task<bool> Delete(int employeeId);
        Task<int> HighSalary();
        
    }
}
