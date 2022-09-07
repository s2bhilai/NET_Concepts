using Npgsql;
using RawCoding_IntegrationTesting;
using RawCoding_IntegrationTesting.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestProject1.Database
{
    //IClassFixture<AnimalSetupFixture> - Scoped to a particular class
    //Collection Fixture - Spans across multiple test classes
    [Collection(nameof(AnimalCollection))]
    public class AnimalDatabaseTests //: IClassFixture<AnimalSetupFixture>
    {
        private AnimalSetupFixture _animalSetUpFixture;

        public AnimalDatabaseTests(AnimalSetupFixture animalSetupFixture)
        {
            _animalSetUpFixture = animalSetupFixture;
        }

        [Fact]
        public async Task AnimalStore_SaveAnimalToDatabase()
        {
            var name = Guid.NewGuid().ToString();
            await _animalSetUpFixture.Store.SaveAnimal(new(0, name, "Bar"));
            var animals = await _animalSetUpFixture.Store.GetAnimals(); 
            
            var animal = Assert.Single(animals,x => x.Name.Equals(name));
            Assert.Equal(1, animal.Id);
            Assert.Equal("Foo", animal.Name);
            Assert.Equal("Bar", animal.Type);            
        }


        [Fact]
        public async Task AnimalStore_GetSavedAnimalByIdFromDatabase()
        {
            var animal = await _animalSetUpFixture.Store.GetAnimal(1);

            Assert.NotNull(animal);
            Assert.Equal(1, animal.Id);
            Assert.Equal("Foo", animal.Name);
            Assert.Equal("Bar", animal.Type);
        }

    }
}
