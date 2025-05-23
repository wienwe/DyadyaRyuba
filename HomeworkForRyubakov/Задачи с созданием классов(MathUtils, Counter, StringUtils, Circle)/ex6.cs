class Person
{
    public string Name { get; set; } = "Ben";

    public Person(string name)
    {
        Name = "Tim";
    }
}

class Employee : Person
{
    public string Company { get; set; }

    public Employee(string name, string company)
    : base("Bob")
    {
        Company = company;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Employee emp = new Employee("Tom", "Microsoft") { Name = "Sam" };

        Console.WriteLine(emp.Name); // Ben Tim Bob Tom Sam
        Console.ReadKey();
    }
}
