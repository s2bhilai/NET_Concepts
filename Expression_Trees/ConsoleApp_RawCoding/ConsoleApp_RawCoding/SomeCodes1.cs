using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_RawCoding
{
    public class SomeCodes1
    {
        public void Start()
        {
            //Naive Approach
            Expression<Func<int, bool>> e1 = x => x < 5;
            Expression<Func<int, bool>> e2 = x => x / 2 == 5;

            //Expression combined = Expression.OrElse(e1, e2);
            //Expression.Lambda<Func<int, bool>>(combined);


        }

        //Expressions are a different format to describe code. They’re a data structure 
        //that represents code. They’re also “portable” in the sense that Expressions 
        //can be passed around and some other piece 
        //of code can investigate it to see what it’s suppose to do.

        // Expression Trees are fundamental to 
        //Entity Framework being able to turn C# code into SQL queries.

        //Expression tree is an in-memory representation of a lambda expression. It holds the actual
        //elements of the query, not the result of the query.

        // lambda expression assigned to Func<T> compiles into executable code and 
        //the lambda expression 
        //assigned to Expression<TDelegate> type compiles into Expression tree.
    }
}
