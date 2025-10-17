using TestTaskMSSQL.DAO;
using TestTaskMSSQL.Services;

namespace TestTaskMSSQL
{
    class Program
    {
        private static InputHelper _IH;
        private static bool _isRunning = true;

        static async Task Main(string[] args)
        {
            try
            {
                // Инициализация зависимостей
                IDao employeeDao = new Dao();
                IService employeeService = new Service(employeeDao);
                _IH = new InputHelper(employeeService);

                Console.WriteLine("Пробуем подключиться к бд....");
                try
                {
                    var initialized = await employeeService.TestConnection();
                    if (!initialized)
                    {
                        Console.WriteLine("Нет подключения к бд");
                        _IH.ShowExit();
                    }

                    Console.WriteLine("Успешное подключение к бд!");

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка подключения: {ex.Message}");
                    _IH.ShowExit();
                }


                while (_isRunning)
                {
                    await ProcessMenu();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                _IH.ShowExit();
            }
        }

        static async Task ProcessMenu()
        {
            Console.WriteLine("\n*******Меню*********");
            Console.WriteLine("1. Новый работник");
            Console.WriteLine("2. Все работники");
            Console.WriteLine("3. Обновить работника");
            Console.WriteLine("4. Удалить работника");
            Console.WriteLine("5. Показать работников с зарплатой выше среднего");
            Console.WriteLine("6. Выход");
            Console.Write("Введите цифру: ");


            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await _IH.ShowCreate();
                    break;
                case "2":
                    await _IH.ShowAll();
                    break;
                case "3":
                    await _IH.ShowUpdate();
                    break;
                case "4":
                    await _IH.ShowDelete();
                    break;
                case "5":
                    await _IH.ShowHighSalary();
                    break;
                case "6":
                    _IH.ShowExit();
                    _isRunning = false;
                    break;
                default:
                    Console.WriteLine("Неверно");
                    break;
            }
        }


    }
}