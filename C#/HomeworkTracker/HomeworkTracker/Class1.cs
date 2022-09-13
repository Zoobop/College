using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Example
{
    //public static void Main(string[] args)
    //{
    //    var person = new Person("Booty", 0, 3.0);
    //    Console.WriteLine(person);
    //}

}

public class Person
{
    public string Name { get; set; }
    public int Id { get; set; }
    public double GPA { get; set; }
    public bool AP { get; set; }

    public Person(string name, int id, double gpa)
    {
        Name = name;
        Id = id;
        GPA = gpa;
        AP = GPA < 2.0;
    }

    public override string ToString()
    {
        return $"{Name}: {AP}";
    }
}