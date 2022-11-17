using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublisherConsole
{
    public class ImportAuthorDTO
    {
        public ImportAuthorDTO(string firstName, string lastName)
        {
            _firstName = firstName;
            _lastName = lastName;
        }

        private string _firstName;
        private string _lastName;

        public string FirstName => _firstName;
        public string LastName => _lastName;
    }
}
