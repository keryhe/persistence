using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Keryhe.Persistence.SqlServer
{
    public class SqlServerProvider : IPersistenceProvider
    {
        private readonly string _connectionString;

        public SqlServerProvider(IOptions<SqlServerProviderOptions> options)
        {
            _connectionString = options.Value.ConnectionString;
        }

        public IDataReader ExecuteQuery(string commandText, CommandType commandType, Dictionary<string, object> parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand(commandText, connection);
            command.CommandType = commandType;
            connection.Open();

            if (parameters != null)
            {
                foreach (string parameter in parameters.Keys)
                {
                    SqlParameter sqlParameter = new SqlParameter(parameter, parameters[parameter]);
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
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(commandText, connection);
                command.CommandType = commandType;
                connection.Open();

                if (parameters != null)
                {
                    foreach (string key in parameters.Keys)
                    {
                        SqlParameter sqlParameter;
                        if (parameters[key] == null)
                        {
                            sqlParameter = new SqlParameter(key, DBNull.Value);
                        }
                        else
                        {
                            sqlParameter = new SqlParameter(key, parameters[key]);
                        }
                        command.Parameters.Add(key);
                    }
                }

                if (outputParameters != null)
                {
                    foreach (string key in outputParameters.Keys)
                    {
                        SqlParameter sqlOutputParameter = new SqlParameter(key, outputParameters[key]);
                        command.Parameters.Add(sqlOutputParameter);
                    }
                }

                int result = command.ExecuteNonQuery();

                if (outputParameters != null)
                {
                    foreach (SqlParameter commandParameter in command.Parameters)
                    {
                        outputParameters[commandParameter.ParameterName] = command.Parameters[commandParameter.ParameterName].Value.ToString();
                    }
                }

                return result;
            }
        }

        public int ExecuteNonQuery(string commandText, CommandType commandType, Dictionary<string, object> parameters)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(commandText, connection);
                command.CommandType = commandType;
                connection.Open();

                if (parameters != null)
                {
                    foreach (string key in parameters.Keys)
                    {
                        SqlParameter sqlParameter;
                        if (parameters[key] == null)
                        {
                            sqlParameter = new SqlParameter(key, DBNull.Value);
                        }
                        else
                        {
                            sqlParameter = new SqlParameter(key, parameters[key]);
                        }

                        command.Parameters.Add(sqlParameter);
                    }
                }

                int result = command.ExecuteNonQuery();
                return result;
            }
        }

        public int ExecuteNonQuery(string commandText, CommandType commandType)
        {
            return ExecuteNonQuery(commandText, commandType, null);
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

