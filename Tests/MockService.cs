using TestTaskMSSQL.Models;
using TestTaskMSSQL.Services;
using EmployeeManagement.Tests.Mocks;
using System;
using Xunit;

namespace TestTaskMSSQL.Tests
{
    public class MockService
    {
        private readonly IService _Service;
        private readonly MockDao _mockDao;

        public MockService()
        {
            _mockDao = new MockDao();
            _Service = new Service(_mockDao);
        }

        [Fact]
        public async Task Create_ValidData_ReturnsEmployeeId()
        {
            // Arrange
            var firstName = "John";
            var lastName = "Doe";
            var email = "john.doe@email.com";
            var dateOfBirth = new DateTime(1990, 1, 1);
            var salary = 50000m;

            // Act
            var result = await _Service.Create(firstName, lastName, email, dateOfBirth, salary);

            // Assert
            Xunit.Assert.True(result > 0);
        }

        [Fact]
        public async Task Create_EmptyFirstName()
        {
            // Arrange
            var firstName = "";
            var lastName = "Doe";
            var email = "john.doe@email.com";
            var dateOfBirth = new DateTime(1990, 1, 1);
            var salary = 50000m;

            // Act & Assert
            await Xunit.Assert.ThrowsAsync<ArgumentException>(() =>
                _Service.Create(firstName, lastName, email, dateOfBirth, salary));
        }

        [Fact]
        public async Task Create_EmployeeTooYoung()
        {
            // Arrange
            var firstName = "John";
            var lastName = "Doe";
            var email = "john.doe@email.com";
            var dateOfBirth = DateTime.Now.AddYears(-17); // 17 лет
            var salary = 50000m;

            // Act & Assert
            await Xunit.Assert.ThrowsAsync<ArgumentException>(() =>
                _Service.Create(firstName, lastName, email, dateOfBirth, salary));
        }

        [Fact]
        public async Task Create_NegativeSalary()
        {
            // Arrange
            var firstName = "John";
            var lastName = "Doe";
            var email = "john.doe@email.com";
            var dateOfBirth = new DateTime(1990, 1, 1);
            var salary = -1000m;

            // Act & Assert
            await Xunit.Assert.ThrowsAsync<ArgumentException>(() =>
                _Service.Create(firstName, lastName, email, dateOfBirth, salary));
        }

        [Fact]
        public async Task Create_InvalidEmail()
        {
            // Arrange
            var firstName = "John";
            var lastName = "Doe";
            var email = "invalid-email";
            var dateOfBirth = new DateTime(1990, 1, 1);
            var salary = 50000m;

            // Act & Assert
            await Xunit.Assert.ThrowsAsync<ArgumentException>(() =>
                _Service.Create(firstName, lastName, email, dateOfBirth, salary));
        }

        [Fact]
        public async Task GetAll_NoEmployees()
        {
            // Act
            var result = await _Service.GetAll();

            // Assert
            Xunit.Assert.Empty(result);
        }

        [Fact]
        public async Task GetAll_Normal()
        {
            // Arrange
            await _Service.Create("John", "Doe", "john@email.com", new DateTime(1990, 1, 1), 50000m);
            await _Service.Create("Jane", "Smith", "jane@email.com", new DateTime(1985, 5, 15), 60000m);

            // Act
            var result = await _Service.GetAll();

            // Assert
            Xunit.Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task Update_Valid()
        {
            // Arrange
            var employeeId = await _Service.Create("John", "Doe", "john@email.com", new DateTime(1990, 1, 1), 50000m);

            // Act
            var result = await _Service.Update(employeeId, "FirstName", "Jonathan");

            // Assert
            Xunit.Assert.True(result);
        }

      

        [Fact]
        public async Task Delete_Normal()
        {
            // Arrange
            var employeeId = await _Service.Create("John", "Doe", "john@email.com", new DateTime(1990, 1, 1), 50000m);

            // Act
            var result = await _Service.Delete(employeeId);

            // Assert
            Xunit.Assert.True(result);
        }


        [Fact]
        public async Task No_HighSalary()
        {
            // Act
            var result = await _Service.HighSalary();

            // Assert
            Xunit.Assert.Equal(0, result);
        }

        [Fact]
        public async Task HighSalary_Normal()
        {
            // Arrange
            await _Service.Create("John", "Doe", "john@email.com", new DateTime(1990, 1, 1), 40000m);
            await _Service.Create("Jane", "Smith", "jane@email.com", new DateTime(1985, 5, 15), 60000m); // Выше среднего

            // Act
            var result = await _Service.HighSalary();

            // Assert
            Xunit.Assert.Equal(1, result);
        }

        [Fact]
        public async Task GetEmployeeByIdAsync_ExistingEmployee_ReturnsEmployee()
        {
            // Arrange
            var expectedId = await _Service.Create("John", "Doe", "john@email.com", new DateTime(1990, 1, 1), 50000m);

            // Act
            var result = await _Service.Get(expectedId);

            // Assert
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(expectedId, result.EmployeeID);
            Xunit.Assert.Equal("John", result.FirstName);
        }

        [Fact]
        public async Task Get_WrongId()
        {
            // Act
            var result = await _Service.Get(999);

            // Assert
            Xunit.Assert.Null(result);
        }
    }
}