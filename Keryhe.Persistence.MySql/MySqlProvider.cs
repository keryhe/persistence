using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Keryhe.Persistence.MySql
{
    public class MySqlProvider : IPersistenceProvider
    {
        private readonly string _connectionString;

        public MySqlProvider(IOptions<MySqlProviderOptions> options)
        {
            _connectionString = options.Value.ConnectionString;
        }

        public IDataReader ExecuteQuery(string commandText, CommandType commandType, Dictionary<string, object> parameters)
        {
            MySqlConnection connection = new MySqlConnection(_connectionString);
            MySqlCommand command = new MySqlCommand(commandText, connection);
            command.CommandType = commandType;
            connection.Open();

            if (parameters != null)
            {
                foreach (string parameter in parameters.Keys)
                {
                    MySqlParameter sqlParameter = new MySqlParameter(parameter, parameters[parameter]);
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
            return ExecuteQuery(commandText, CommandType.Text);
        }

        public int ExecuteNonQuery(string commandText, CommandType commandType, Dictionary<string, object> parameters, ref Dictionary<string, object> outputParameters)
        {
            MySqlConnection connection = new MySqlConnection(_connectionString);
            try
            {
                MySqlCommand command = new MySqlCommand(commandText, connection);
                command.CommandType = commandType;
                connection.Open();

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
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                MySqlCommand command = new MySqlCommand(commandText, connection);
                command.CommandType = commandType;
                connection.Open();

                if (parameters != null)
                {
                    foreach (string parameter in parameters.Keys)
                    {
                        MySqlParameter sqlParameter = new MySqlParameter(parameter, parameters[parameter]);
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

