using Npgsql;
using RawCoding_IntegrationTesting.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RawCoding_IntegrationTesting
{
    public class AnimalSetupFixture : IAsyncLifetime
    {
        public const string _connBase = "Server=127.0.0.1;Port=2022;User Id=postgres;Password=postgres;";
        public const string _db = "animal_setup_fixture";
        public static readonly string _conn = $"{_connBase};Database={_db}";

        private PostgresqlConnectionFactory _connectionFactory;
        public IAnimalStore Store { get; private set; }

        public async Task InitializeAsync()
        {
            await DatabaseSetup.CreateDatabase(_connBase, _db);
            _connectionFactory = new(_conn);
            NpgsqlConnection connection = await _connectionFactory.Create();
            var database = new Postgresql(connection);
            Store = new AnimalStore(database);
            await Seed();
        }

        public async Task Seed()
        {
            await Store.SaveAnimal(new(0, "Foo", "Bar"));
            await Store.SaveAnimal(new(0, "Bar", "Bar"));
            await Store.SaveAnimal(new(0, "Baz", "Bar"));
        }

        public async Task DisposeAsync()
        {
            await _connectionFactory.DisposeAsync();
            await DatabaseSetup.DeleteDatabase(_connBase, _db);
        }
      
    }
}
