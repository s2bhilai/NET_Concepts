using RawCoding_IntegrationTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestProject1.Database
{
    [Collection(nameof(AnimalCollection))]
    public class AnimalDatabaseQueryTests
    {
        private AnimalSetupFixture _animalSetupFixture;

        public AnimalDatabaseQueryTests(AnimalSetupFixture animalSetupFixture)
        {
            _animalSetupFixture = animalSetupFixture;
        }

        [Fact]
        public async Task AnimalStore_ListsAnimalsFromDatabase()
        {
            var animals = await _animalSetupFixture.Store.GetAnimals();
            Assert.Equal(3, animals.Count);
            Assert.Contains(animals, x => x.Name.Equals("Foo"));
            Assert.Contains(animals, x => x.Name.Equals("Bar"));
            Assert.Contains(animals, x => x.Name.Equals("Baz"));
        }
    }
}
