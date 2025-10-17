using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTaskMSSQL.Models;

namespace TestTaskMSSQL.DAO
{
    public class Dao : IDao
    {
        private readonly string _connectionString;

        public Dao()
        {
            _connectionString = @"Server=localhost;Database=EmployeeDB;Trusted_Connection=true;";
        }
        public async Task<bool> TestConnection()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<int> Create(Employee employee)
        {
            const string query = @"
                INSERT INTO Employees (FirstName, LastName, Email, DateOfBirth, Salary)
                VALUES (@FirstName, @LastName, @Email, @DateOfBirth, @Salary);
                SELECT SCOPE_IDENTITY();";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@FirstName", employee.FirstName);
            command.Parameters.AddWithValue("@LastName", employee.LastName);
            command.Parameters.AddWithValue("@Email", employee.Email);
            command.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@Salary", employee.Salary);

            await connection.OpenAsync();
            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        public async Task<List<Employee>> GetAll()
        {
            var employees = new List<Employee>();
            const string query = "SELECT * FROM Employees ORDER BY EmployeeID";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                employees.Add(new Employee
                {
                    EmployeeID = reader.GetInt32("EmployeeID"),
                    FirstName = reader.GetString("FirstName"),
                    LastName = reader.GetString("LastName"),
                    Email = reader.GetString("Email"),
                    DateOfBirth = reader.GetDateTime("DateOfBirth"),
                    Salary = reader.GetDecimal("Salary")
                });
            }

            return employees;
        }

        public async Task<bool> Update(int employeeId, string field, object value)
        {
            var allowedFields = new[] { "FirstName", "LastName", "Email", "DateOfBirth", "Salary" };
            if (!allowedFields.Contains(field))
                throw new ArgumentException("Неверное имя");

            var query = $"UPDATE Employees SET {field} = @Value WHERE EmployeeID = @EmployeeID";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);

            if (value is DateTime dateValue)
                value = dateValue.ToString("yyyy-MM-dd");

            command.Parameters.AddWithValue("@Value", value ?? DBNull.Value);
            command.Parameters.AddWithValue("@EmployeeID", employeeId);

            await connection.OpenAsync();
            var rowsAffected = await command.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }

        public async Task<bool> Delete(int employeeId)
        {
            const string query = "DELETE FROM Employees WHERE EmployeeID = @EmployeeID";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@EmployeeID", employeeId);

            await connection.OpenAsync();
            var rowsAffected = await command.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }

        public async Task<int> HighSalary()
        {
            const string query = @"
                SELECT COUNT(*) 
                FROM Employees 
                WHERE Salary > (SELECT AVG(Salary) FROM Employees)";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);

            await connection.OpenAsync();
            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        public async Task<Employee> Get(int employeeId)
        {
            const string query = "SELECT * FROM Employees WHERE EmployeeID = @EmployeeID";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@EmployeeID", employeeId);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new Employee
                {
                    EmployeeID = reader.GetInt32("EmployeeID"),
                    FirstName = reader.GetString("FirstName"),
                    LastName = reader.GetString("LastName"),
                    Email = reader.GetString("Email"),
                    DateOfBirth = reader.GetDateTime("DateOfBirth"),
                    Salary = reader.GetDecimal("Salary")
                };
            }

            return null;
        }
        
    }
}
