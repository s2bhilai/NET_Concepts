using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RawCoding_IntegrationTesting.Database
{
    public class PostgresqlConnectionFactory: IAsyncDisposable
    {
        private readonly string _connectionString;

        public PostgresqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        private NpgsqlConnection _connection;

        public async Task<NpgsqlConnection> Create()
        {
            // not async safe
            if (_connection != null)
                return _connection;

            _connection = new(_connectionString);
            await _connection.OpenAsync();
            return _connection;
        }

        public ValueTask DisposeAsync() => _connection.DisposeAsync();
    }
}
