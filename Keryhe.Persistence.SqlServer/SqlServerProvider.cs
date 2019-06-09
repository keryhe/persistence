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

        public IDbConnection CreateConnection()
        {
            IDbConnection connection = new SqlConnection(_connectionString);
            return connection;
        }

        public IDataReader ExecuteQuery(IDbConnection connection, string commandText, CommandType commandType, Dictionary<string, object> parameters)
        {
            SqlCommand command = new SqlCommand(commandText, (SqlConnection)connection);
            command.CommandType = commandType;
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
            SqlCommand command = new SqlCommand(commandText, (SqlConnection)connection);
            command.CommandType = commandType;

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

        public int ExecuteNonQuery(IDbConnection connection, string commandText, CommandType commandType, Dictionary<string, object> parameters)
        {
            return ExecuteNonQuery(connection, commandText, commandType, parameters, null);
        }

        public int ExecuteNonQuery(IDbConnection connection, string commandText, CommandType commandType)
        {
            return ExecuteNonQuery(connection, commandText, commandType, null);
        }

        public int ExecuteNonQuery(IDbConnection connection, string commandText)
        {
            return ExecuteNonQuery(connection, commandText, CommandType.Text);
        }
    }
}

