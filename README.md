# C# консольное приложение с с подключением к MS SQL Server
# Обновление Update
![Uploading image.png…]()
![Uploading image.png…]()

# Немного о проекте:
Данное приложение создано в соотвестствии с ТЗ и включает в себя следующие классы(не считая интерфейсов):

1.Employee - модель данных
2. Dao - реализует логику связанную с взаимодействием с бд (CRUD операции)
3. Service - бизнес логика и валидация данных
4. InputHelper - класс обработки ввода и обработки исключений связанных с ним

# Пройдемся по сделанному:
 # 1.Настройте бд
 <img width="925" height="345" alt="image" src="https://github.com/user-attachments/assets/cdd92ded-8c59-4382-93cc-1a65559dfccc" />
 
 # 2.Создать консольное приложение:
 
 запуск приложения
 <img width="565" height="291" alt="image" src="https://github.com/user-attachments/assets/97fb92e4-43b7-4dc2-a2d3-f79357ffade4" />
 
 Создание работника 
 
 <img width="438" height="231" alt="image" src="https://github.com/user-attachments/assets/81761034-25d2-49ed-ab45-c6c86df5a42a" />
 
Вывод всех работников 

<img width="1038" height="177" alt="image" src="https://github.com/user-attachments/assets/50e71671-3095-49c3-b469-a21f168ffcee" />

Обновление работника

<img width="997" height="289" alt="image" src="https://github.com/user-attachments/assets/bf7d1656-b6f0-451b-84d9-249fed96fc6f" />

<img width="1001" height="79" alt="image" src="https://github.com/user-attachments/assets/fccd52f1-0b85-4706-92ed-bceb6a5b2089" />

Удаление

<img width="1047" height="171" alt="image" src="https://github.com/user-attachments/assets/a8f66d27-972c-4687-8281-baf355bea6f9" />

<img width="919" height="103" alt="image" src="https://github.com/user-attachments/assets/582d1ac7-02db-4bff-9be0-a8d2561c5190" />

Кол-во сотрудников с зарплатой выше среднего

<img width="413" height="92" alt="image" src="https://github.com/user-attachments/assets/4a08bab1-b657-4b79-8ac0-3eae2ca8dbfc" />

Выход:

<img width="275" height="79" alt="image" src="https://github.com/user-attachments/assets/8272cdc3-d3e8-45ee-bcdf-92fdaeada36b" />

# 3. Обработка исключений:

Ошибка email

<img width="537" height="224" alt="image" src="https://github.com/user-attachments/assets/27923446-9d95-4d3e-ad13-b88e06bc2dfb" />

Ввод пустого слова 

<img width="179" height="94" alt="image" src="https://github.com/user-attachments/assets/41501134-ad95-4f3f-a5a9-8dc234182b1b" />

Ошибка ввода даты

<img width="360" height="76" alt="image" src="https://github.com/user-attachments/assets/00ea3d09-71af-4e72-828b-5864bcb0e941" />

<img width="585" height="110" alt="image" src="https://github.com/user-attachments/assets/bf8d63ff-65af-4caa-9e01-1361fdaf93d5" />

<img width="716" height="105" alt="image" src="https://github.com/user-attachments/assets/7f8862fd-427d-4be0-8159-0bbfcbd8ac29" />

Ошибка id

<img width="587" height="106" alt="image" src="https://github.com/user-attachments/assets/5b3e195b-2feb-4cca-8877-34fb4c48787f" />

Отрицательная зарплата

<img width="747" height="85" alt="image" src="https://github.com/user-attachments/assets/23abd751-d653-49f8-8452-1ea6f2577400" />

Обработка ввода чисел

<img width="583" height="57" alt="image" src="https://github.com/user-attachments/assets/dbef6d5c-5d61-4da5-8517-82922699d5ff" />

<img width="1041" height="82" alt="image" src="https://github.com/user-attachments/assets/02327610-cb59-4489-886f-f16648b67daa" />

# 4. Реализованы Unit тесты

<img width="548" height="531" alt="image" src="https://github.com/user-attachments/assets/927ffff5-efe1-4651-892c-00045187e5e1" />

# Почему при выходе явно не указывается Dispose
Я использую using для всех disposable объектов и соединение закрывается автоматически даже при ошибка при выходе из методов Dao
# Как предотвращаются SQL иньекции
Во всех методах Dao использованы параметризированные запросы






