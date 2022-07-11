using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_RawCoding
{
    public class DynamicQuery
    {
        public void RunTimeState()
        {
            var companyNames = new[] {
                "Consolidated Messenger", "Alpine Ski House", "Southridge Video",
                "City Power & Light", "Coho Winery", "Wide World Importers",
                "Graphic Design Institute", "Adventure Works", "Humongous Insurance",
                "Woodgrove Bank", "Margie's Travel", "Northwind Traders",
                "Blue Yonder Airlines", "Trey Research", "The Phone Company",
                "Wingtip Toys", "Lucerne Publishing", "Fourth Coffee"
            };

            IQueryable<string> companyNamesSource = companyNames.AsQueryable();
            var fixedQry = companyNames.OrderBy(x => x);


            //Use runtime state from within expression tree
            //The internal expression tree—and thus the query—haven't been modified; the query returns different values 
            //only because the value of length has been changed.
            var length = 1;
            var qry = companyNamesSource
                .Select(x => x.Substring(0, length))
                .Distinct();

            Console.WriteLine(string.Join(",", qry));


            //Call additional LINQ methods
            //var qry = companyNamesSource;
            //if (sortByLength)
            //{
            //    qry = qry.OrderBy(x => x.Length);
            //}




        }
    }
}
