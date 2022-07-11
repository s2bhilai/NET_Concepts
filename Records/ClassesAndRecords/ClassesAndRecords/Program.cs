using System;

namespace ClassesAndRecords
{
    //Positional Record - constructor and deconstructor only created 
    //for positional properties
    public record CourseRecord(string Name,string Author);

    public record CourseRecord1(string Name,string author)
    {
        public string GetTitle()
        {
            return $"{Name} - {author}";
        }
    }

    public record CourseRecord2(string Name)
    {
        public string Author { get; init; }
        public string Title
        {
            get
            {
                return $"{Name} - {Author}";
            }
        }
    }

    public record CourseRecord3(string name, string author)
    {
        
    }

    public record TimedCourseRecord(string name,string author,int duration) :
        CourseRecord3(name,author)
    {
          
    }



    class Program
    {
        static void Main(string[] args)
        {
            var plCourse = new Course();
            plCourse.Name = "Records";

            //Since Class is reference type, so assignment to another variable 
            //copies the reference so point to same location.
            var anotherCourse = plCourse;

            //Referencce type more efficient than value type as
            //copying is not necessary

            Console.WriteLine(anotherCourse.Name);


            var classroomCourse = new CourseRecord("Working with C#","Subin");

            //Read Values from records - destructuring
            var (name, author) = classroomCourse;
            Console.WriteLine(name);
            Console.WriteLine(author);

            //Destructuring doesnt work in class as the class doesnt have
            //deconstructor by default as compared to positional record.
            //If we explicitly write a deconstructor then destructuring will
            //work with classes too

            //A Record instance is immutable, as the properties are initonly
            //so cannot be updated, Once record instance created then
            //cannot be changed
            //classroomCourse.Name = "Nibin"; //error

            //A record is an reference type that can easily be cloned
            //Positional record gets a constructor and deconstructor free


            //Usually we use reference types to pass object from one class
            //to another, so suppose we have an object that's passed to
            //multiple classes and in one place by mistake the property
            //is changed, then this change will start showing in all the
            //places where this object is referenced, so use immutability.

            //Usecase - All the DTOs should be Record Type as they should not change

            //We can create a new instance and change one or more properties/ clone,
            //a seperate object in memory is created
            var anotherCourse1 = classroomCourse with { Name = "React" };

            var classroomCourse1 = new CourseRecord2("React js")
            {
                Author = "Subin"
            };

            //Inheritance
            // 1. Record can only derive from other records
            // 2. Derived positional records have to repeat all property declarations.

            //Virtual - All Deriving types can override this method
            //They can have their own implementation for it while having the possibility
            //to use the implementation of the parent class too

            //Virtual methods in Object
            //1 Equals - Compare an instance of the type to another instance
            //2 GetHashCode - Returns a number, When this code for one instance
            // is same for another instance, it is considered equal to it.
            //3 ToString - Converts the instance to a string. - Returns the full type name

            //in IL code, Record type is basically a Class.

            //Equality
            //Record instance is equal to another instance
            //1 All Property values are same
            //2 And they are exact same type
            //so if we derive a record from another type and compare the 2 instances
            //they will never be equal to each other

            Console.ReadKey();
        }
    }
}
