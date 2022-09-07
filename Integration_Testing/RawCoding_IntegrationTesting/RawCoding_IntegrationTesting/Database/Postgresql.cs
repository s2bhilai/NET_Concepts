﻿using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RawCoding_IntegrationTesting.Database
{
    public class Postgresql: IDatabase
    {
        private readonly NpgsqlConnection _connection;

        public Postgresql(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<NpgsqlDataReader> Query(string query, Dictionary<string, object> parameters = null)
        {
            await using var cmd = new NpgsqlCommand(query, _connection);

            if (parameters != null)
                foreach (var (k, v) in parameters)
                    cmd.Parameters.AddWithValue(k, v);

            return await cmd.ExecuteReaderAsync();
        }

        public async Task Insert(string command, Dictionary<string, string> parameters = null)
        {
            await using var cmd = new NpgsqlCommand(command, _connection);

            if (parameters != null)
                foreach (var (k, v) in parameters)
                    cmd.Parameters.AddWithValue(k, v);

            await cmd.ExecuteNonQueryAsync();
        }
    }
}
