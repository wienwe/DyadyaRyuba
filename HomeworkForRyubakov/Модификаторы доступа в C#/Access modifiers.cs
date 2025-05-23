using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Models
{
    /// <summary>
    /// Базовый класс Car с различными модификаторами доступа
    /// </summary>
    public class Car
    {
        // Поля make и model приватные, чтобы защитить их от прямого доступа извне
        private string make;
        private string model;

        // Свойство Year публичное, так как год выпуска может быть доступен извне
        public int Year { get; set; }

        // Internal метод доступен только в пределах текущей сборки
        internal void SetMakeAndModel(string make, string model)
        {
            this.make = make;
            this.model = model;
        }

        // Protected метод доступен только внутри класса и производных классов
        protected virtual void DisplayInfo()
        {
            Console.WriteLine($"Make: {make}, Model: {model}, Year: {Year}");
        }
    }

    /// <summary>
    /// Класс ElectricCar наследует от Car и добавляет функциональность для электромобилей
    /// </summary>
    public class ElectricCar : Car
    {
        // Приватное поле, так как емкость батареи не должна изменяться напрямую
        private double batteryCapacity;

        // Публичный метод для установки емкости батареи
        public void SetBatteryCapacity(double capacity)
        {
            this.batteryCapacity = capacity;
        }

        // Переопределение protected метода с добавлением информации о батарее
        protected override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Battery Capacity: {batteryCapacity} Км/ч");
        }

        // Публичный метод для вывода информации (так как protected метод недоступен из Program)
        public void ShowInfo()
        {
            DisplayInfo();
        }
    }
}

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Работа с классом Car
            var car = new MyApp.Models.Car();

            // Попытка установить make и model напрямую (не получится - поля private)
            // car.make = "Toyota"; // Ошибка компиляции
            // car.model = "Camry"; // Ошибка компиляции

            // Установка значений через internal метод
            car.SetMakeAndModel("Toyota", "Camry");
            car.Year = 2020; // Публичное свойство доступно

            // Работа с классом ElectricCar
            var electricCar = new MyApp.Models.ElectricCar();
            electricCar.SetMakeAndModel("Tesla", "Model S");
            electricCar.Year = 2022;
            electricCar.SetBatteryCapacity(100.5); // Установка емкости батареи

            // Вывод информации
            // car.DisplayInfo(); // Ошибка - метод protected
            // electricCar.DisplayInfo(); // Ошибка - метод protected

            // Для демонстрации добавим публичный метод ShowInfo в ElectricCar
            Console.WriteLine("Информация о машине:");
            electricCar.ShowInfo();

            // Альтернативный вариант - создать метод в Program для вызова protected методов
            DisplayCarInfo(car);
            DisplayElectricCarInfo(electricCar);
        }

        // Вспомогательный метод для демонстрации protected метода Car
        static void DisplayCarInfo(MyApp.Models.Car car)
        {
            // Нельзя вызвать напрямую, но можно через reflection (не рекомендуется)
            Console.WriteLine("\nАльтернативный способ отображения информации об автомобиле:");
            var method = typeof(MyApp.Models.Car).GetMethod("DisplayInfo",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            method.Invoke(car, null);
        }

        // Вспомогательный метод для демонстрации protected метода ElectricCar
        static void DisplayElectricCarInfo(MyApp.Models.ElectricCar electricCar)
        {
            Console.WriteLine("\nАльтернативный способ отображения информации об электромобиле:");
            var method = typeof(MyApp.Models.ElectricCar).GetMethod("DisplayInfo",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            method.Invoke(electricCar, null);
        }
    }
}