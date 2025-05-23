class Auto // легковой автомобиль
{
    public int Seats { get; set; } // количество сидений
    public Auto()
    {
        Console.WriteLine("Auto has been created");
    }
    public Auto(int seats)
    {
        Seats = seats;
    }
}
class Truck : Auto // грузовой автомобиль
{
    public decimal Capacity { get; set; } // грузоподъемность
    public Truck(decimal capacity)
    {
        Seats = 2;
        Capacity = capacity;
        Console.WriteLine("Truck has been created");
    }
}
class Program
{
    static void Main(string[] args)
    {
        Truck truck = new Truck(1.1m);
        Console.WriteLine($"Truck with capacity {truck.Capacity}");
        Console.ReadKey();
    }
}
