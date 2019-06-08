using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Transactions;

namespace Keryhe.Persistence.MySql
{
    public class MySqlProvider : IPersistenceProvider
    {
        private readonly string _connectionString;

        public MySqlProvider(IOptions<MySqlProviderOptions> options)
        {
            _connectionString = options.Value.ConnectionString;
        }

        public IDbConnection CreateConnection()
        {
            IDbConnection connection = new MySqlConnection(_connectionString);
            return connection;
        }

        public IDataReader ExecuteQuery(IDbConnection connection, string commandText, CommandType commandType, Dictionary<string, object> parameters)
        {
            MySqlCommand command = new MySqlCommand(commandText, (MySqlConnection)connection);
            command.CommandType = commandType;

            if (parameters != null)
            {
                foreach (string parameter in parameters.Keys)
                {
                    MySqlParameter sqlParameter = new MySqlParameter(parameter, parameters[parameter]);
                    command.Parameters.Add(sqlParameter);
                }
            }

            return command.ExecuteReader();
        }

        public IDataReader ExecuteQuery(IDbConnection connection, string commandText, CommandType commandType)
        {
            return ExecuteQuery(connection, commandText, commandType, null);
        }

        public IDataReader ExecuteQuery(IDbConnection connection, string commandText)
        {
            return ExecuteQuery(connection, commandText, CommandType.Text);
        }

        public int ExecuteNonQuery(IDbConnection connection, string commandText, CommandType commandType, Dictionary<string, object> parameters, Dictionary<string, object> outputParameters)
        {
            MySqlCommand command = new MySqlCommand(commandText, (MySqlConnection)connection);
            command.CommandType = commandType;

            if (parameters != null)
            {
                foreach (string parameter in parameters.Keys)
                {
                    MySqlParameter sqlParameter = new MySqlParameter(parameter, parameters[parameter]);
                    command.Parameters.Add(parameter);
                }
            }

            if (outputParameters != null)
            {
                foreach (string outputParameter in outputParameters.Keys)
                {
                    MySqlParameter sqlOutputParameter = new MySqlParameter(outputParameter, outputParameters[outputParameter]);
                    command.Parameters.Add(sqlOutputParameter);
                }
            }

            int result = command.ExecuteNonQuery();

            if (outputParameters != null)
            {
                foreach (MySqlParameter commandParameter in command.Parameters)
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

