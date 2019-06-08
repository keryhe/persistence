using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using System.Transactions;

namespace Keryhe.Persistence.PostgreSQL
{
    public class PostgreSQLProvider : IPersistenceProvider
    {
        private readonly string _connectionString;

        public PostgreSQLProvider(IOptions<PostgreSQLProviderOptions> options)
        {
            _connectionString = options.Value.ConnectionString;
        }

        public IDbConnection CreateConnection()
        {
            IDbConnection connection = new NpgsqlConnection(_connectionString);
            return connection;
        }

        public IDataReader ExecuteQuery(IDbConnection connection, string commandText, CommandType commandType, Dictionary<string, object> parameters)
        {
            NpgsqlCommand command = new NpgsqlCommand(commandText, (NpgsqlConnection)connection);
            command.CommandType = commandType;

            if (parameters != null)
            {
                foreach (string parameter in parameters.Keys)
                {
                    NpgsqlParameter sqlParameter = new NpgsqlParameter(parameter, parameters[parameter]);
                    command.Parameters.Add(sqlParameter);
                }
            }

            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public IDataReader ExecuteQuery(IDbConnection connection, string commandText, CommandType commandType)
        {
            return ExecuteQuery(connection, commandText, commandType, null);
        }

        public IDataReader ExecuteQuery(IDbConnection connection, string commandText)
        {
            return ExecuteQuery(connection, commandText, System.Data.CommandType.Text);
        }

        public int ExecuteNonQuery(IDbConnection connection, string commandText, CommandType commandType, Dictionary<string, object> parameters, Dictionary<string, object> outputParameters)
        {
            NpgsqlCommand command = new NpgsqlCommand(commandText, (NpgsqlConnection)connection);
            command.CommandType = commandType;

            if (parameters != null)
            {
                foreach (string parameter in parameters.Keys)
                {
                    NpgsqlParameter sqlParameter = new NpgsqlParameter(parameter, parameters[parameter]);
                    command.Parameters.Add(parameter);
                }
            }

            if (outputParameters != null)
            {
                foreach (string outputParameter in outputParameters.Keys)
                {
                    NpgsqlParameter sqlOutputParameter = new NpgsqlParameter(outputParameter, outputParameters[outputParameter]);
                    command.Parameters.Add(sqlOutputParameter);
                }
            }

            int result = command.ExecuteNonQuery();

            if (outputParameters != null)
            {
                foreach (NpgsqlParameter commandParameter in command.Parameters)
                {
                    outputParameters[commandParameter.ParameterName] = command.Parameters[commandParameter.ParameterName].Value.ToString();
                }
            }

            return result;
        }

        public int ExecuteNonQuery(IDbConnection connection, string commandText, CommandType commandType, Dictionary<string, object> parameters)
        {
            return ExecuteNonQuery(connection, commandText, commandType, parameters, null);
        }

        public int ExecuteNonQuery(IDbConnection connection, string commandText, CommandType commandType)
        {
            return ExecuteNonQuery(connection, commandText, commandType);
        }

        public int ExecuteNonQuery(IDbConnection connection, string commandText)
        {
            return ExecuteNonQuery(connection, commandText, CommandType.Text);
        }
    }
}

