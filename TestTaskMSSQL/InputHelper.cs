using System.Globalization;
using TestTaskMSSQL.Services;

namespace TestTaskMSSQL
{
    public class InputHelper
    {
        private readonly IService _Service;

        public InputHelper(IService Service)
        {
            _Service = Service;
        }


        public async Task ShowCreate()
        {
            Console.WriteLine("Новый работник:");

            try
            {
                var firstName = ReadString("Имя");
                var lastName = ReadString("Фамилия");
                var email = ReadString("Email");
                var dateOfBirth = ReadDate("Родился (dd.mm.yyyy)");
                var salary = ReadDecimal("Зарплата");

                Console.WriteLine("Сохраняем работника...");
                var employeeId = await _Service.Create(firstName, lastName, email, dateOfBirth, salary);

                Console.WriteLine($"Успешно создан новый работник с Id: {employeeId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка добавления работника: {ex.Message}");
            }
        }

        public async Task ShowAll()
        {
            Console.WriteLine("Все работники:");

            try
            {

                var employees = await _Service.GetAll();

                if (employees.Count == 0)
                {
                    Console.WriteLine("Нет работников");
                    return;
                }

                foreach (var employee in employees)
                {
                    Console.WriteLine(employee.print());
                }

                Console.WriteLine($"Всего: {employees.Count}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        public async Task ShowUpdate()
        {
            Console.WriteLine("Обновление:");

            try
            {
                var employeeId = ReadInt("Введите Id работника, которого хотите обновить:");


                var employee = await _Service.Get(employeeId);

                if (employee == null)
                {
                    Console.WriteLine("Нет такого");
                    return;
                }


                Console.WriteLine(employee);
                Console.WriteLine();

                var (fieldName, newValue) = ReadUpdateField();
                if (fieldName == null) return;


                var success = await _Service.Update(employeeId, fieldName, newValue);

                if (success)
                {
                    Console.WriteLine("Работик успешно обновлен");
                }
                else
                {
                    Console.WriteLine("Не удалось обновить работника");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        public async Task ShowDelete()
        {
            Console.WriteLine("Удаление работника");

            try
            {
                var employeeId = ReadInt("Введите Id работника для удаления:");

                var employee = await _Service.Get(employeeId);

                if (employee == null)
                {
                    Console.WriteLine("Нет такого");
                    return;
                }


                Console.WriteLine(employee.print() +"\n");
                

                if (ReadConfirmation("Вы точно желаете его удалить? (y/n)"))
                {

                    var success = await _Service.Delete(employeeId);

                    if (success)
                    {
                        Console.WriteLine("Успешно удален");
                    }
                    else
                    {
                        Console.WriteLine("Ошибка");
                    }
                }
                else
                {
                    Console.WriteLine("Удаление отменено");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        public async Task ShowHighSalary()
        {
            Console.WriteLine("Работники с зарплатой выше среднего:");

            try
            {
                var count = await _Service.HighSalary();
                Console.WriteLine($"Число ценных специалистов: {count}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        // Методы ввода данных
        private string ReadString(string title)
        {
            while (true)
            {
                Console.Write($"{title}: ");
                var input = Console.ReadLine()?.Trim();

                if (!string.IsNullOrEmpty(input))
                    return input;

                Console.WriteLine($"input error!");
            }
        }

        private DateTime ReadDate(string prompt)
        {
            while (true)
            {
                Console.Write($"{prompt}: ");
                var input = Console.ReadLine();

                if (DateTime.TryParse(input, out DateTime result))
                    return result;

                Console.WriteLine("Ошибка ввода даты");
            }
        }

        private decimal ReadDecimal(string prompt)
        {
            while (true)
            {
                Console.Write($"{prompt}: ");
                var input = Console.ReadLine();

                if (decimal.TryParse(input, NumberStyles.Currency, CultureInfo.InvariantCulture, out decimal result))
                    return result;

                Console.WriteLine("Неверный формат!");
            }
        }

        private int ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write($"{prompt}: ");
                var input = Console.ReadLine();

                if (int.TryParse(input, out int result))
                    return result;

                Console.WriteLine("Неверный формат!");
            }
        }

        private bool ReadConfirmation(string prompt)
        {
            Console.Write($"{prompt}: ");
            var input = Console.ReadLine()?.ToLower();
            return input == "y" || input == "yes";
        }

        private (string fieldName, object value) ReadUpdateField()
        {
            Console.WriteLine("Что обновляем?");
            Console.WriteLine("1. Имя");
            Console.WriteLine("2. Фамилию");
            Console.WriteLine("3. Email");
            Console.WriteLine("4. Дату рождения");
            Console.WriteLine("5. Зарплату");

            while (true)
            {
                Console.Write("Неверная цифра!");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        return ("FirstName", ReadString("Новое имя:"));
                    case "2":
                        return ("LastName", ReadString("Новая фамилия:"));
                    case "3":
                        return ("Email", ReadString("Новый Email"));
                    case "4":
                        return ("DateOfBirth", ReadDate("Новая дата рождения (dd.mm.yyyy)"));
                    case "5":
                        return ("Salary", ReadDecimal("Новая зарплата"));
                    default:
                        Console.WriteLine("Неверный номер!");
                        break;
                }
            }
        }



        public void ShowExit()
        {
            Console.Write("Нажмите клавишу для выхода ....");
            Console.ReadKey();
            Console.WriteLine("**********Пока!**********");
        }
    }
}