using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;

namespace Keryhe.Persistence.PostgreSQL
{
    public class PostgreSQLProvider : IPersistenceProvider
    {
        private readonly string _connectionString;

        public PostgreSQLProvider(IOptions<PostgreSQLProviderOptions> options)
        {
            _connectionString = options.Value.ConnectionString;
        }

        public IDataReader ExecuteQuery(string commandText, CommandType commandType, Dictionary<string, object> parameters)
        {
            NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            NpgsqlCommand command = new NpgsqlCommand(commandText, connection);
            command.CommandType = commandType;
            connection.Open();

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

        public IDataReader ExecuteQuery(string commandText, CommandType commandType)
        {
            return ExecuteQuery(commandText, commandType, null);
        }

        public IDataReader ExecuteQuery(string commandText)
        {
            return ExecuteQuery(commandText, System.Data.CommandType.Text);
        }

        public int ExecuteNonQuery(string commandText, CommandType commandType, Dictionary<string, object> parameters, ref Dictionary<string, object> outputParameters)
        {
            NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(commandText, connection);
                command.CommandType = commandType;
                connection.Open();

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
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }

        public int ExecuteNonQuery(string commandText, CommandType commandType, Dictionary<string, object> parameters)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                NpgsqlCommand command = new NpgsqlCommand(commandText, connection);
                command.CommandType = commandType;
                connection.Open();

                if (parameters != null)
                {
                    foreach (string parameter in parameters.Keys)
                    {
                        NpgsqlParameter sqlParameter = new NpgsqlParameter(parameter, parameters[parameter]);
                        command.Parameters.Add(sqlParameter);
                    }
                }

                int result = command.ExecuteNonQuery();
                return result;
            }
        }

        public int ExecuteNonQuery(string commandText, CommandType commandType)
        {
            return ExecuteNonQuery(commandText, commandType);
        }

        public int ExecuteNonQuery(string commandText)
        {
            return ExecuteNonQuery(commandText, CommandType.Text);
        }

        public IDbTransaction BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public void CommitTransaction(IDbTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public void RollbackTransaction(IDbTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public IDataReader ExecuteQuery(IDbTransaction transaction, string commandText, CommandType commandType, Dictionary<string, object> parameters)
        {
            throw new NotImplementedException();
        }

        public IDataReader ExecuteQuery(IDbTransaction transaction, string commandText, CommandType commandType)
        {
            throw new NotImplementedException();
        }

        public IDataReader ExecuteQuery(IDbTransaction transaction, string commandText)
        {
            throw new NotImplementedException();
        }

        public int ExecuteNonQuery(IDbTransaction transaction, string commandText, CommandType commandType, Dictionary<string, object> parameters)
        {
            throw new NotImplementedException();
        }

        public int ExecuteNonQuery(IDbTransaction transaction, string commandText, CommandType commandType)
        {
            throw new NotImplementedException();
        }
    }
}

