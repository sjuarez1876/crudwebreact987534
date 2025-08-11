using Microsoft.Data.SqlClient;
using System.Data;

namespace WebEmpleados.Server.Data
{
    public class DbContext
    {
        private readonly string _connectionString;

        public DbContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("BDEmpleadosReact");

        }

        private async Task<SqlConnection> GetConnectionAsync()
        {

            var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync();

            return connection;

        }

        public async Task<int> ExecuteAsync(string storeProcedure, SqlParameter[] parameters)
        {

            using var connection = await GetConnectionAsync();

            using var command = new SqlCommand(storeProcedure, connection)

            {

                CommandType = CommandType.StoredProcedure

            };

            command.Parameters.AddRange(parameters);

            return await command.ExecuteNonQueryAsync();

        }

        public async Task<DataTable> QueryAsync(string storeProcedure, SqlParameter[] parameters = null)

        {

            using var connection = await GetConnectionAsync();

            using var command = new SqlCommand(storeProcedure, connection)

            {

                CommandType = CommandType.StoredProcedure

            };

            if (parameters != null) command.Parameters.AddRange(parameters);

            using var reader = await command.ExecuteReaderAsync();

            var table = new DataTable();

            table.Load(reader);

            return table;

        }



        public async Task<string> ExecuteScalarString(string storeProcedure, SqlParameter[] parameters)
        {
            using var connection = await GetConnectionAsync();

            using var command = new SqlCommand(storeProcedure, connection)

            {

                CommandType = CommandType.StoredProcedure

            };

            command.Parameters.AddRange(parameters);

            return (string)await command.ExecuteScalarAsync();

        }
    }
}
