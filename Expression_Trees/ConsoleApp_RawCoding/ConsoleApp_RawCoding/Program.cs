using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ConsoleApp_RawCoding
{
    class Program
    {
        static void Main(string[] args)
        {

            //Func<int> five = () => 5;
            //Console.WriteLine(five());

            //Expression cannot contains statements
            //Expression<Func<int>> five_exp = () => 5;
            //Console.WriteLine(five_exp.Compile().Invoke());

            DynamicQuery query = new DynamicQuery();
            query.RunTimeState();

            Console.ReadLine();

            var user = new User();
            Expression<Func<User, object>> exp = user => user.Name;
            var body = exp.Body;

            if(body is MemberExpression me)
            {
                Console.WriteLine(me.Member.Name.ToLower());
            } else if(body is UnaryExpression ue)
            {
                Console.WriteLine(((MemberExpression)ue.Operand).Member.Name.ToLower());
            }

            Console.WriteLine(exp.Body);

            ///////////////////////////////////////////////////////////
            //1st Example - Constructing the querystring in url 
                       //   without hardcoding 
            string url = CreateUrl("http://asdfs.com", u => u.Name, u => u.Age);
            Console.WriteLine(url);
            /////////////////////////////////////////////////////////

            /////////////////////////////////////////////////////////
            //2nd Example - Read a value from querystring/body and perform filtering
            var selectProperty = "Number"; //"number - either of 2
            var someClass = new SomeClass
            {
                Word = "Hello World",
                Number = 1234
            };

            //parameter - 
            var parameter = Expression.Parameter(typeof(SomeClass));
            var accessor =  Expression.PropertyOrField(parameter, selectProperty);

            var lambda = Expression.Lambda(accessor, false, parameter);
            Console.WriteLine(lambda.Compile().DynamicInvoke(someClass));

            //////////////////////////////////////////////////////////

            Console.ReadKey();
        }



        public static string CreateUrl(string url, 
            params Expression<Func<User,object>>[] fieldSelectors)
        {
            var fields = new List<string>();

            foreach (var selector in fieldSelectors)
            {
                var body = selector.Body;

                if (body is MemberExpression me)
                {
                    fields.Add(me.Member.Name.ToLower());
                }
                else if (body is UnaryExpression ue)
                {
                    fields.Add(((MemberExpression)ue.Operand).Member.Name.ToLower());
                }
            }

            var selectedFields = string.Join(',', fields);

            return string.Concat(url, "?fields=", selectedFields);
        }
    }

    public class SomeClass
    {
        public string Word { get; set; }
        public int Number { get; set; }
    }

    public class User
    {
        public string  Name { get; set; }
        public int Age { get; set; }
    }
}



/* 
 1. Expression trees represent code in a tree-like data structure, 
    where each node is an expression.
 2. You can compile and run code represented by expression trees. 
    This enables dynamic modification of executable code, the execution of 
    LINQ queries in various databases, and the creation of dynamic queries.
 3. When a lambda expression is assigned to a variable of type Expression<TDelegate>,
    the compiler emits code to build an expression tree that represents 
    the lambda expression.
 4. The C# compiler can generate expression trees only from expression lambdas 
     (or single-line lambdas). 
    It cannot parse statement lambdas (or multi-line lambdas).
 5. Expression<Func<int, bool>> lambda = num => num < 5;
 6. To create expression trees by using the API, use the Expression class.
 7. The Expression<TDelegate> type provides the Compile method that 
    compiles the code represented by an expression tree into an executable delegate.
    Expression<Func<int, bool>> expr = num => num < 5;  
    // Compiling the expression tree into a delegate.  
    Func<int, bool> result = expr.Compile();        
    // Invoking the delegate and writing the result to the console.  
    Console.WriteLine(result(4)); //True  
  
 */