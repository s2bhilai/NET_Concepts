using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTesting_Moq.Unit
{
    public class CreateSomething
    {
        private IStore _store;

        public CreateSomething(IStore store)
        {
            _store = store;
        }

        public CreateSomethingResult Create(Something something)
        {
            if(something is {Name: { Length: > 0} })
            {
                return new(_store.Save(something));
            }   

            return new(false, "something not valid");
        }

        public record CreateSomethingResult(bool success, string Error = "");
    }

    public interface IStore
    {
        bool Save(Something something);
    }

    public class Something
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
