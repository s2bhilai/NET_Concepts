﻿using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RawCoding_IntegrationTesting.Database
{
    public interface IDatabase
    {
        Task<NpgsqlDataReader> Query(string query, Dictionary<string, object> parameters = null);
        Task Insert(string command, Dictionary<string, string> parameters = null);
    }
}
