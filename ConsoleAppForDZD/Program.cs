using System;
//using System.Collections.Generic
using System.Text;
using System.Threading.Tasks;
using DZD.Linq;
//using System.Linq;

namespace ConsoleAppForDZD
{
    class Program
    {
        class Person
        {
            public int grade;
            public Subject[] subjects;
        }

        class Subject
        {
            public string Name;
        }
        static void Main(string[] args)
        {
            //int[] arr = new int[10] { 1, 2, 3, 4, 11, 34, 33, 1, 13, 5 };

            //var js = from ar in arr
            //         where ar > 5
            //         select ar * 2;

            //foreach (var k in js)
            //    Console.WriteLine(k);

            //Console.WriteLine(arr.Where((k, i) => k > 5));

            //1)Implementing Empty
            //var k = Enumarable.Empty<int>();
            //var l = k.ToList();

            //2)Implementing Count and LongCount
            //int[] arr = new int[10] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            //var k = arr.Count(f => f == 0);

            //3)Implementing SelectMany
            Person[] persons = new Person[4] {  new Person {  grade=2, subjects=new Subject[3] { new Subject { Name = "Math" }, new Subject { Name = "Calculus" }, new Subject { Name="English"} } },
                                                new Person {  grade=2, subjects=new Subject[2] { new Subject { Name = "Math" }, new Subject { Name = "Georgian" } } },
                                                new Person {  grade=2, subjects=new Subject[1] { new Subject { Name = "Russian" }} },
                                                new Person {  grade=2, subjects=new Subject[2] { new Subject { Name = "Programing" }, new Subject { Name="Web"} } }};


            var subjectsWithNameMoreThan4 = persons.SelectMany(p => p.subjects.Where(f => f.Name.Length > 4));

            Console.ReadLine();
        }
    }
}
