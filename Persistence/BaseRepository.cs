using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Persistence
{
    public abstract class BaseRepository : IBaseRepository
    {
        private readonly IConfigurationProvider _configurationProvider;
        private IDbConnection? _connection;

        protected BaseRepository(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }

        private string? ConnectionString => _configurationProvider.GetConnectionString();

        public IDbConnection DbConnection()
        {
            _connection ??= new SqlConnection(ConnectionString);

            if (_connection.State != ConnectionState.Open)
                ÅbenConnection();

            return _connection;
        }


        public IEnumerable<T> ExecuteStoredProcedure<T>(IDbConnection connection, string procedureNavn, object? parameters = null)
        {
            try
            {
                return connection.Query<T>(procedureNavn, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                Dispose();
            }
        }

        public IDbConnection? ÅbenConnection()
        {
            _connection?.Open();

            return _connection;
        }
        public void Dispose()
        {
            if (_connection != null)
            {
                if (_connection.State != ConnectionState.Closed)
                {
                    _connection.Close();
                }
                _connection?.Dispose();
            }
            GC.SuppressFinalize(this);
        }
    }
}
