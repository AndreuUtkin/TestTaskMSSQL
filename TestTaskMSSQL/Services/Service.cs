using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTaskMSSQL.DAO;
using TestTaskMSSQL.Models;
namespace TestTaskMSSQL.Services
{
    public class Service:IService
    {
        private readonly IDao _Dao;
        public Service(IDao employeeDao)
        {
            _Dao = employeeDao;
        }
        public async Task<bool> TestConnection()
        {
            try
            {
                return await _Dao.TestConnection();
            }
            catch
            {
                return false;
            }
        }
        //проверка корректности работника
        bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; 
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
        //проверка корректности работника
        private void Validate(string firstName, string lastName, string email, DateTime dateOfBirth, decimal salary)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("Нет имени");

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Нет фамилии");

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Нет email");

            if (!IsValidEmail(email))
                throw new ArgumentException("Неправильный email");

            if (dateOfBirth > DateTime.Now.AddYears(-18))
                throw new ArgumentException("Работнику должно быть более 18 лет");

            if (dateOfBirth < DateTime.Now.AddYears(-100))
                throw new ArgumentException("Возраст нереалистичный");

            if (salary < 0)
                throw new ArgumentException("Зарплата не может быть отрицательной");

            if (salary > 1000000)
                throw new ArgumentException("Слишком большая зарплата");
        }
        //создание работника
        public async Task<int> Create(string firstName, string lastName, string email, DateTime dateOfBirth, decimal salary)
        {
            Validate(firstName, lastName, email, dateOfBirth, salary);

            var employee = new Employee
            {
                FirstName = firstName.Trim(),
                LastName = lastName.Trim(),
                Email = email.Trim(),
                DateOfBirth = dateOfBirth,
                Salary = salary
            };

            return await _Dao.Create(employee);
        }
        //получение всех работников
        public async Task<List<Employee>> GetAll()
        {
            return await _Dao.GetAll();
        }
        //обновление работника
        public async Task<bool> Update(int employeeId, string field, object value)
        {
            var existingEmployee = await _Dao.Get(employeeId);
            if (existingEmployee == null)
                throw new ArgumentException("Работник не найден");

            if (field == "Email" && value is string email)
            {
                if (string.IsNullOrWhiteSpace(email))
                    throw new ArgumentException("Нет email");

                if (!IsValidEmail(email))
                    throw new ArgumentException("Неправильный email");
            }

            if (field == "Salary" && value is decimal salary)
            {
                if (salary < 0)
                    throw new ArgumentException("Зарплата не может быть отрицательной");
            }

            return await _Dao.Update(employeeId, field, value);
        }
        //удаление работника
        public async Task<bool> Delete(int employeeId)
        {
            var existingEmployee = await _Dao.Get(employeeId);
            if (existingEmployee == null)
                throw new ArgumentException("Работник не найден");

            return await _Dao.Delete(employeeId);
        }
        //работники с зарплатой выше среднего
        public async Task<int> HighSalary()
        {
            return await _Dao.HighSalary();
        }
        //поиск работника по id
        public async Task<Employee> Get(int employeeId)
        {
            return await _Dao.Get(employeeId);
        }
    }
}
