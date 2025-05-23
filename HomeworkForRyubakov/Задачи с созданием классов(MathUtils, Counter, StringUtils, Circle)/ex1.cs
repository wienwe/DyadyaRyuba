using System;

namespace HelloApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Person tom = new Employee();
            Console.ReadKey();
        }
    }

    internal class Person
    {

    }
    public class Employee : Person
    {

    }
}
