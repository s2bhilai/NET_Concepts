using ConsoleApp_WBC_StorageApp.Data;
using ConsoleApp_WBC_StorageApp.Entities;
using ConsoleApp_WBC_StorageApp.Repositories;
using ConsoleApp_WBC_StorageApp.SpecialCases;
using System;

namespace ConsoleApp_WBC_StorageApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //var employeeRepo = new GenericRepositoryWithRemove<Entities.Employee,int>();
            //employeeRepo.Add(new Entities.Employee { FirstName = "Julia" });
            //employeeRepo.Add(new Entities.Employee { FirstName = "Anna" });
            //employeeRepo.Add(new Entities.Employee { FirstName = "Thomas" });

            //employeeRepo.Save();

            //var employee = employeeRepo.GetById(2);
            //Console.WriteLine($"Employee with Id 2: {employee.FirstName}");

            //var orgRepo = new GenericRepository<Entities.Organization,Guid>();
            //orgRepo.Add(new Entities.Organization { Name = "PLuralsight" });
            //orgRepo.Add(new Entities.Organization { Name = "EMC" });

            //orgRepo.Save();

            /////////////////////////////////////////
            ///Generic Interface/////////////
            ///
            //Generic Type Parameters of interfaces by default invariant
            //i.e they have to be the same type
            //IRepository<Organization> repo = new ListRepository<Organization>();
            //Same Organization type on interface as well as class
            //Also on the interface we cannot use less specific type than the class type
            //IRepository<IEntity> repo = new ListRepository<Organization>();
            //So in order to use co variant i.e allow a less specific type to be 
            // initialized with more specific type use out parameter

            //IRepository<IEntity> repo = new ListRepository<Organization>();
            //For methods that we read from repo, less specific type can be used,
            //but for add and remove we need to pass Organization Entity.
            // So co variant is applied on read methods where the type goes out
            // but cannot be used where the type goes in, 
            //works if T is used only for return values

            //IReadRepository<IEntity> repo = new ListRepository<Organization>();
            //In order to make the above statment work, make IReadRepository 
            //covaraint by using out param


            //var empRepo = new SqlRepository<Employee>(new StorageAppDbContext());
            //AddEmployees1(empRepo);
            //AddManagers(empRepo); // using contravaraint
            //GetEmployeebyId(empRepo);
            //WriteAllToConsole(empRepo);

            //IRepository<Employee> repo2 = new SqlRepository<Employee>(new StorageAppDbContext());
            //Above one works as Generic Interfaces are invariant so both the type
            //should be same i.e Employee
            //IRepository<Manager> repo3 = new SqlRepository<Employee>(new StorageAppDbContext());
            //This doesn't work as Manager is more specific type than Employee
            // so here we need to mention the repository
            //as ContraVariant i.e use in keyword, works if T is used only for 
            // input parameters.
            //IWriteRepository<Manager> repo4 = new SqlRepository<Employee>(new StorageAppDbContext());

            //var orgRepo1 = new ListRepository<Organization>();
            //AddOrganization(orgRepo1);
            //WriteAllToConsole(orgRepo1);


            /////////////////////////////////////////////////////////
            ///Generic Methods or Delegates
            /////////////////////////////////////////////////////
            ///private static void AddBatch<T>(IRepository<T> repository,
            //T[] items) where T: IEntity


            // ItemAdded<Employee> itemAdded = new ItemAdded<Employee>(EmployeeAdded);
            // var empRepo = new SqlRepository<Employee>(new StorageAppDbContext(),itemAdded);

            //More specific type so contravariant - in
            // ItemAdded<Manager> managerAdded = itemAdded;
            // AddEmployees1(empRepo);


            ////Event Handler
            //var empRepo3 = new SqlRepository_EventHandler<Employee>(new StorageAppDbContext());
            //empRepo3.ItemAdded += EmployeeAdded;
            //  empRepo3.ItemAdded += EmpRepo3_ItemAdded;

            //Special Cases
            // Different static property is created for each generic type
            // so string - 2 , int - 1, bool - 0
            //If there's no generic type, then for static property, for each instance same
            //property will get incremented, if we call container instance 3 times then static 
            //property value will become 3.

            _ = new Container<string>();
            _ = new Container<string>();
            var container = new Container<int>();

            //If using Generic methods inside generic classes, then use a different type 
            //parameter name for generic methods
            container.PrintItem<string>("string text");

            Console.WriteLine($"Container<string>: { Container<string>.InstanceCount }");
            Console.WriteLine($"Container<int>: { Container<int>.InstanceCount }");
            Console.WriteLine($"Container<bool>: { Container<bool>.InstanceCount }");

            var result = Add(2, 3);
            Console.WriteLine($"2+3 = {result}");

            var result1 = Add(2.7, 3.3);
            Console.WriteLine($"2.7+3.3= {result1}");

            Console.ReadLine();
        }

        private static T Add<T>(T x, T y) where T: notnull
        {
            dynamic a = x;
            dynamic b = y;
            return a + b;
        }

        private static void EmpRepo3_ItemAdded(object? sender, Employee e)
        {
            Console.WriteLine($"Employee added => {e.FirstName}");
        }

        private static void EmployeeAdded(Employee employee)
        {
            Console.WriteLine($"Employee added => {employee.FirstName}");
        }

        private static void AddManagers(IWriteRepository<Manager> managerRepo)
        {
            var saraManager = new Manager { FirstName = "Sara" };
            var saraManagerCopy = saraManager.Copy();
            managerRepo.Add(saraManager);

            if(saraManagerCopy is not null)
            {
                saraManagerCopy.FirstName += "_Copy";
                managerRepo.Add(saraManagerCopy);
            }

            managerRepo.Add(new Manager { FirstName = "Sara" });
            managerRepo.Add(new Manager { FirstName = "Henry" });

            managerRepo.Save();
        }

        private static void WriteAllToConsole(IReadRepository<IEntity> empRepo)
        {
            var items = empRepo.GetAll();
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }

        public static void AddEmployees(GenericRepository<Entities.Employee,int> employeeRepository)
        {
            employeeRepository.Add(new Entities.Employee { FirstName = "subin" });
            employeeRepository.Save();
        }

        public static void AddEmployees1(IRepository<Entities.Employee> employeeRepository)
        {
            var employees = new[]
            {
                new Entities.Employee { FirstName = "subin" },
                new Entities.Employee { FirstName = "shareef" },
                new Entities.Employee { FirstName = "raees" }
            };

            //RepositoryExtensions.AddBatch<Employee>(employeeRepository, employees);

            employeeRepository.AddBatch<Employee>(employees);

            employeeRepository.Save();
        }

        public static void AddOrganization(IRepository<Organization> orgRepository)
        {
            var organizations = new[]
            {
                new Organization {Name = "Pluralsight"},
                new Organization {Name = "Globomantics"}
            };

            RepositoryExtensions.AddBatch(orgRepository,organizations);

            //orgRepository.Add(new Organization {  Name = "subin" });
            
        }

        //private static void AddBatch<T>(IWriteRepository<T> repository,
        //    T[] items)
        //{
        //    foreach (var item in items)
        //    {
        //        repository.Add(item);
        //    }

        //    repository.Save();
        //}

        public static void GetEmployeebyId(IRepository<Entities.Employee> employeeRepository)
        {
            var employee = employeeRepository.GetById(2);
            Console.WriteLine($"Employee with Id 2: {employee.FirstName}");
        }
    }
}


//Dependency Inversion Principle
// Components must depend on abstractions and not on implementations.