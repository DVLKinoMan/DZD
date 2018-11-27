using System;
//using System.Collections.Generic
using System.Text;
using System.Threading.Tasks;
using DZD.Linq;
using System.Collections;
using System.Collections.Generic;
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
            //Person[] persons = new Person[4] {  new Person {  grade=2, subjects=new Subject[3] { new Subject { Name = "Math" }, new Subject { Name = "Calculus" }, new Subject { Name="English"} } },
            //                                    new Person {  grade=3, subjects=new Subject[2] { new Subject { Name = "Math" }, new Subject { Name = "Georgian" } } },
            //                                    new Person {  grade=4, subjects=new Subject[1] { new Subject { Name = "Russian" }} },
            //                                    new Person {  grade=5, subjects=new Subject[2] { new Subject { Name = "Programing" }, new Subject { Name="Web"} } }};


            //var subjectsWithNameMoreThan4 = persons.SelectMany(p => p.subjects.Where(f => f.Name.Length > 4).Select(f => f.Name.Length), (p, selector) => selector + ") " + p.grade);
            //4) Implementing Any and All
            //Person[] persons = new Person[4] {  new Person {  grade=2, subjects=new Subject[3] { new Subject { Name = "Math" }, new Subject { Name = "Calculus" }, new Subject { Name="English"} } },
            //                                    new Person {  grade=3, subjects=new Subject[2] { new Subject { Name = "Math" }, new Subject { Name = "Georgian" } } },
            //                                    new Person {  grade=4, subjects=new Subject[1] { new Subject { Name = "Russian" }} },
            //                                    new Person {  grade=5, subjects=new Subject[2] { new Subject { Name = "Programing" }, new Subject { Name="Web"} } }};

            //var anyoneIsInPerson = persons.Any();
            //var anyoneHas5grade = persons.Any(p => p.grade == 5);
            //var anyoneHas8Grade = persons.Any(p => p.grade == 8);
            //var allHaveNonNegativeGrades = persons.All(p => p.grade >= 0);
            //var allHaveMoreThan1Subjects = persons.All(p => p.subjects.Count() > 1);

            //5) Implementing First, FirstOrDefault, Last, LastOrDefault, Single, SingleOrDefault
            //Person[] persons = new Person[4] {  new Person {  grade=2, subjects=new Subject[3] { new Subject { Name = "Math" }, new Subject { Name = "Calculus" }, new Subject { Name="English"} } },
            //                                    new Person {  grade=3, subjects=new Subject[2] { new Subject { Name = "Math" }, new Subject { Name = "Georgian" } } },
            //                                    new Person {  grade=4, subjects=new Subject[1] { new Subject { Name = "Russian" }} },
            //                                    new Person {  grade=5, subjects=new Subject[2] { new Subject { Name = "Programing" }, new Subject { Name="Web"} } }};

            //Person[] emptyPersons = new Person[0];

            //var first = persons.First();
            //var firstOrDefault = persons.FirstOrDefault();

            //var firstWithPredicate = persons.First(p=>p.grade==4);
            //var firstOrDefaultWithPredicate = persons.FirstOrDefault(p=>p.grade==5);

            //var firstFromEmptyPersons = emptyPersons.First();
            //var firstOrDefaultFromEmptyPersons = emptyPersons.FirstOrDefault();

            //var firstWithPredicateFromEmptyPersons = emptyPersons.First(p=>p.grade==0);
            //var firstOrDefaultWithPredicateFromEmptyPersons = emptyPersons.FirstOrDefault(p=>p.grade==0);

            //6) Implementing DefaultIfEmtpy
            //Person[] persons = new Person[0];

            //var _persons = persons.DefaultIfEmpty();

            //7) Implementing Aggregate
            //Person[] persons = new Person[4] {  new Person {  grade=2, subjects=new Subject[3] { new Subject { Name = "Math" }, new Subject { Name = "Calculus" }, new Subject { Name="English"} } },
            //                                    new Person {  grade=3, subjects=new Subject[2] { new Subject { Name = "Math" }, new Subject { Name = "Georgian" } } },
            //                                    new Person {  grade=4, subjects=new Subject[1] { new Subject { Name = "Russian" }} },
            //                                    new Person {  grade=5, subjects=new Subject[2] { new Subject { Name = "Programing" }, new Subject { Name="Web"} } }};
            //var maxGradePerson = persons.Aggregate((x, y) =>
            // {
            //     if (x.grade > y.grade)
            //         return x;
            //     else return y;
            // });

            ////Why we need this 2 Overloads do not know
            //var gradesSum = persons.Aggregate(0, (accumulate, y) => y.grade + accumulate);
            //var gradesSumString = persons.Aggregate(0, (accumulate, y) => y.grade + accumulate, sumofGrades => "This is Some of Grades: " + sumofGrades.ToString());

            //8) Implementing Distinct
            //int[] ints = new int[5] { 1, 1, 3, 2, 4 };
            //Person[] persons = new Person[4] {  new Person {  grade=2, subjects=new Subject[3] { new Subject { Name = "Math" }, new Subject { Name = "Calculus" }, new Subject { Name="English"} } },
            //                                    new Person {  grade=2, subjects=new Subject[2] { new Subject { Name = "Math" }, new Subject { Name = "Georgian" } } },
            //                                    new Person {  grade=1, subjects=new Subject[1] { new Subject { Name = "Russian" }} },
            //                                    new Person {  grade=5, subjects=new Subject[2] { new Subject { Name = "Programing" }, new Subject { Name="Web"} } }};

            //var distinctInts = ints.Distinct();
            //PersonsComparer comp = new PersonsComparer();
            //var distinctPersonsWithGrades = persons.Distinct(comp);

            //9) Implementing Union
            //Person[] persons1 = new Person[2] {  new Person {  grade=2, subjects=new Subject[3] { new Subject { Name = "Math" }, new Subject { Name = "Calculus" }, new Subject { Name="English"} } },
            //                                    new Person {  grade=5, subjects=new Subject[2] { new Subject { Name = "Programing" }, new Subject { Name="Web"} } }};

            //Person[] persons2 = new Person[2] { new Person {  grade=1, subjects=new Subject[1] { new Subject { Name = "Russian" }} },
            //                                    new Person {  grade=5, subjects=new Subject[2] { new Subject { Name = "Programing" }, new Subject { Name="Web"} } }};

            //var personUnions = persons1.Union(persons2);

            //PersonsComparer personComparer = new PersonsComparer();
            //var personUnionsWithComparer = persons1.Union(persons2, personComparer);

            //10) Implementing Intersect
            //Person[] persons1 = new Person[2] {  new Person {  grade=2, subjects=new Subject[3] { new Subject { Name = "Math" }, new Subject { Name = "Calculus" }, new Subject { Name="English"} } },
            //                                    new Person {  grade=5, subjects=new Subject[2] { new Subject { Name = "Programing" }, new Subject { Name="ff"} } }};

            //Person[] persons2 = new Person[2] { new Person {  grade=1, subjects=new Subject[1] { new Subject { Name = "Russian" }} },
            //                                    new Person {  grade=5, subjects=new Subject[2] { new Subject { Name = "Programing" }, new Subject { Name="Web"} } }};

            //PersonsComparer perComparer = new PersonsComparer();
            //var personsIntersect = persons1.Intersect(persons2, perComparer);

            //11) Implementing Except
            //10) Implementing Intersect
            Person[] persons1 = new Person[3] {  new Person {  grade=2, subjects=new Subject[3] { new Subject { Name = "Math" }, new Subject { Name = "Calculus" }, new Subject { Name="English"} } },
                                                new Person {  grade=2, subjects=new Subject[3] { new Subject { Name = "Math" }, new Subject { Name = "Calculus" }, new Subject { Name="English"} } },
                                                new Person {  grade=5, subjects=new Subject[2] { new Subject { Name = "Programing" }, new Subject { Name="ff"} } }};

            Person[] persons2 = new Person[2] { new Person {  grade=1, subjects=new Subject[1] { new Subject { Name = "Russian" }} },
                                                new Person {  grade=5, subjects=new Subject[2] { new Subject { Name = "Programing" }, new Subject { Name="Web"} } }};

            PersonsComparer perComparer = new PersonsComparer();
            var personsExcept = persons1.Except(persons2, perComparer);

            Console.ReadLine();
        }

        class PersonsComparer : IEqualityComparer<Person>
        {
            public bool Equals(Person x, Person y)
            {
                if (x.grade == y.grade)
                    return true;
                return false;
            }

            public int GetHashCode(Person obj)
            {
                return obj.grade.GetHashCode();
            }
        }
    }
}
