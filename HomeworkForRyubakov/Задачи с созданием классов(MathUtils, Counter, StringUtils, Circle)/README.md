# Практическая работа:
## Задание: Использование модификаторов доступа в C#
Цель: Научиться применять различные модификаторы доступа для управления видимостью классов и их членов, а также понять, как это влияет на инкапсуляцию и безопасность кода.

## Выполнила: 
Рамазанова Диляра ИПО-31.22
## Инструменты:
Visual Studio 2022

## Вопрос 1
Почему следующая программа не компилируется:

Ссылка на программу:

[![ex1.cs](https://img.shields.io/badge/🔢_MathUtils_класс-4285F4?style=for-the-badge&logo=csharp&logoColor=white)](https://github.com/wienwe/DyadyaRyuba/blob/main/HomeworkForRyubakov/Задачи%20с%20созданием%20классов(MathUtils%2C%20Counter%2C%20StringUtils%2C%20Circle)/ex1.cs)  

## Ответ:

Программа не компилируется из-за несоответствия модификаторов доступа классов. Класс Employee имеет модификатор public, а его базовый класс Person - internal. Это нарушает правило, что производный класс не может быть более доступным, чем его базовый класс.
Исправление: нужно сделать оба класса либо public, либо internal (обычно internal по умолчанию).

# Ссылка на исправление:

[![ans1.cs](https://img.shields.io/badge/📌_Решение_MathUtils-4CAF50?style=for-the-badge&logo=checkcircle&logoColor=white)](https://github.com/wienwe/DyadyaRyuba/blob/main/HomeworkForRyubakov/Задачи%20с%20созданием%20классов(MathUtils%2C%20Counter%2C%20StringUtils%2C%20Circle)/ans1.cs)  

## Вопрос 2
Даны следующие классы:


Ссылка на программу:

[![🧮 Задача: Counter класс](https://img.shields.io/badge/🧮_Задача:_Классы-4285F4?style=for-the-badge&logo=csharp&logoColor=white)](https://github.com/wienwe/DyadyaRyuba/blob/main/HomeworkForRyubakov/Задачи%20с%20созданием%20классов(MathUtils%2C%20Counter%2C%20StringUtils%2C%20Circle)/ex2.cs)


Какие конструкторы и в каком порядке в данном случае будет выполняться?


## Ответ:

При создании объекта Employee tom = new Employee("Tom", "Microsoft"); конструкторы выполняются в следующем порядке:

*Employee(string name, string company) - вызываемый конструктор*

*Person(string name) - через base(name), который вызывает:*

*Person(string name, int age) через this(name, 18)*

Затем выполняется тело конструктора *Employee(string name, string company)*
## Вопрос 3
Как запретить наследование от класса?


Чтобы запретить наследование от класса, нужно пометить его ключевым словом sealed
## Ссылка на ответ:
[![ans3.cs](https://img.shields.io/badge/📌_Решение_StringUtils-4CAF50?style=for-the-badge&logo=checkcircle&logoColor=white)](https://github.com/wienwe/DyadyaRyuba/blob/main/HomeworkForRyubakov/Задачи%20с%20созданием%20классов(MathUtils%2C%20Counter%2C%20StringUtils%2C%20Circle)/ans3.cs)
## Вопрос 4
Что выведет на консоль следующая программа и почему?


Ссылка на программу:


[![ex4.cs](https://img.shields.io/badge/📝_StringUtils_класс-4285F4?style=for-the-badge&logo=csharp&logoColor=white)](https://github.com/wienwe/DyadyaRyuba/blob/main/HomeworkForRyubakov/Задачи%20с%20созданием%20классов(MathUtils%2C%20Counter%2C%20StringUtils%2C%20Circle)/ex4.cs)  


## Ответ:
Программа выведет:


*Грузовик с грузоподъемностью 1.1 тонн*
Почему: создается объект Truck с параметрами 2 сиденья и грузоподъемность 1.1 тонн, затем выводится только информация о грузоподъемности.
## Вопрос 5
Что выведет на консоль следующая программа и почему?


Ссылка на программу:


[![ex5.cs](https://img.shields.io/badge/⭕_Circle_класс-4285F4?style=for-the-badge&logo=csharp&logoColor=white)](https://github.com/wienwe/DyadyaRyuba/blob/main/HomeworkForRyubakov/Задачи%20с%20созданием%20классов(MathUtils%2C%20Counter%2C%20StringUtils%2C%20Circle)/ex5.cs)  

## Ответ:
Программа выведет:

*Auto has been created
Truck has been created
Truck with capacity 1.1*


Почему:

При создании Truck сначала вызывается конструктор по умолчанию Auto()

Затем выполняется конструктор Truck(decimal capacity)

Выводится информация о создании объектов

В конце выводится значение грузоподъемности
## Вопрос 6
Что выведет на консоль следующая программа и почему?


Ссылка на программу:


[![ex6.cs](https://img.shields.io/badge/✨_Доп_задача-4285F4?style=for-the-badge&logo=csharp&logoColor=white)](https://github.com/wienwe/DyadyaRyuba/blob/main/HomeworkForRyubakov/Задачи%20с%20созданием%20классов(MathUtils%2C%20Counter%2C%20StringUtils%2C%20Circle)/ex6.cs)
## Ответ:
Программа выведет:

*Sam*
Почему (порядок инициализации свойства Name):

Инициализатор свойства: *Name = "Ben"* (но перезаписывается далее)

Конструктор *Person(string name): Name = "Tim"* (перезаписывает "Ben")

В конструкторе Employee передается *base("Bob"): Name = "Tim"* (аргумент "Bob" игнорируется из-за жесткого присвоения "Tim")

После конструктора выполняется инициализатор объекта: *{ Name = "Sam" }*, который перезаписывает предыдущие значения
