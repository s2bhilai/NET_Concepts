using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RawCoding_IntegrationTesting.Database
{
    public interface IAnimalStore
    {
        Task<IList<Animal>> GetAnimals();
        Task<Animal> GetAnimal(int id);
        Task SaveAnimal(Animal animal);
    }
}
